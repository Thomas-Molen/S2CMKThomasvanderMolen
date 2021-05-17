using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using PlatformerSpeedRunner.Objects.Base;
using PlatformerSpeedRunner.Enum;
using PlatformerSpeedRunner.States.Base;
using PlatformerSpeedRunner.Objects;
using PlatformerSpeedRunner.Input;
using PlatformerSpeedRunner.Input.Base;
using PlatformerSpeedRunner.Camera;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using PlatformerSpeedRunner.Helper;
using System.Collections;

namespace PlatformerSpeedRunner.States
{
    public class GameplayState : BaseGameState
    {
        private Player player;
        private CollisionHelper Collision;
        private AnimationHelper Animation;
        private DatabaseHelper Database;
        private BasicObject chargeCircle;
        private BasicObject endFlag;
        private TimeSpan startingTime;
        private TimeSpan elapsedTime;
        private TimeSpan checkPointTime = new TimeSpan(0);
        private GameTime localGameTime;
        private Text timerText;
        private Text debugText;

        private int TimeCharged;
        private Vector2 spawnPoint = new Vector2(200, 750);

        //textures
        private const string woodenBoxLarge = "Terrain\\WoodenBoxLarge";
        private const string grassLeft = "Terrain\\GrassLeft";
        private const string grassRight = "Terrain\\GrassRight";
        private const string grassMiddle = "Terrain\\GrassMiddle";
        private const string grassSoilLeft = "Terrain\\GrassSoilLeft";
        private const string grassSoilRight = "Terrain\\GrassSoilRight";
        private const string grassSoilMiddle = "Terrain\\GrassSoilMiddle";
        private const string spike = "Enemies\\Spike";
        private const string stoneCubeLarge = "Terrain\\StoneCubeLarge";
        private const string stoneSlabVertical = "Terrain\\StoneSlabVertical";
        private const string stoneSlabHorizontal = "Terrain\\StoneSlabHorizontal";
        private const string checkPointTexture = "Terrain\\CheckPoint";

        private readonly List<BasicObject> ObjectSpriteList = new List<BasicObject>();
        private readonly List<MovingRockHead> RockHeadSpriteList = new List<MovingRockHead>();
        private readonly List<MovingSpikeHead> SpikeHeadSpriteList = new List<MovingSpikeHead>();
        private readonly List<CheckPoint> CheckPointList = new List<CheckPoint>();

        private readonly List<BasicObject> TopsCollisionList = new List<BasicObject>();
        private readonly List<BasicObject> SidesCollisionList = new List<BasicObject>();
        private readonly List<BasicObject> FullCollisionList = new List<BasicObject>();
        private readonly List<BasicObject> DeathCollisionList = new List<BasicObject>();
        private readonly List<MovingRockHead> RockHeadCollisionList = new List<MovingRockHead>();
        private readonly List<MovingSpikeHead> SpikeHeadCollisionList = new List<MovingSpikeHead>();
        private readonly List<CheckPoint> CheckPointCollisionList = new List<CheckPoint>();
        private readonly List<BasicObject> EndFlagCollisionList = new List<BasicObject>();

        private int xGameBorderMin = 23;
        private int xGameBorderMax = 5980;
        private int yGameBorderMax = Program.height;
        private int yGameBorderMin = 25;

        public override void LoadContent()
        {
            currentState = GameState.Gameplay;

            Collision = new CollisionHelper();
            Animation = new AnimationHelper();
            Database = new DatabaseHelper();
            player = new Player(LoadTexture("Player\\Idle\\IdlePinkMan"));

            chargeCircle = new BasicObject(LoadTexture("Player\\Charging\\ChargingCircle1"), new Vector2(0, 0));

            endFlag = new BasicObject(LoadTexture("Terrain\\EndFlag"), new Vector2(5720, 950), true);
            EndFlagCollisionList.Add(endFlag);

            backgroundImage = new BasicObject(LoadTexture("Backgrounds\\PinkWallpaper"), new Vector2(0, 0));

            localGameTime = new GameTime();

            LoadObjects();
        }

        public override void HandleInput()
        {
            InputManager.GetCommands(cmd =>
            {
                if (cmd is GameplayInputCommand.Exit)
                {
                    Environment.Exit(1);
                }
                if (cmd is GameplayInputCommand.PlayerMoveLeft)
                {
                    player.Movement.MoveLeft();
                }
                if (cmd is GameplayInputCommand.PlayerMoveRight)
                {
                    player.Movement.MoveRight();
                }
                if (cmd is GameplayInputCommand.PlayerMoveNone)
                {
                    player.Movement.NoDirection();
                }
                if (cmd is GameplayInputCommand.PlayerLMBHold)
                {
                    AddGameObject(chargeCircle);
                    TimeCharged += 1;

                    chargeCircle.Position.SetPosition(new Vector2(player.Position.position.X, player.Position.position.Y - 10));

                    if (TimeCharged < 10)
                    {
                        chargeCircle.Texture.SetTexture(LoadTexture("Player\\Charging\\ChargingCircle1"));
                    }
                    else if (TimeCharged < 20)
                    {
                        chargeCircle.Texture.SetTexture(LoadTexture("Player\\Charging\\ChargingCircle2"));
                    }
                    else if (TimeCharged < 30)
                    {
                        chargeCircle.Texture.SetTexture(LoadTexture("Player\\Charging\\ChargingCircle3"));
                    }
                    else if (TimeCharged >= 30)
                    {
                        chargeCircle.Texture.SetTexture(LoadTexture("Player\\Charging\\ChargingCircle4"));
                    }
                }
                if (cmd is GameplayInputCommand.PlayerLMBRelease)
                {
                    chargeCircle.Position.SetPosition(new Vector2(chargeCircle.Position.position.X, chargeCircle.Position.position.X + 2000));
                    RemoveGameObject(chargeCircle);
                    if (TimeCharged >= 30)
                    {
                        player.Grapple(30, camera);
                    }
                    else if (TimeCharged < 10)
                    { }
                    else
                    {
                        player.Grapple(TimeCharged, camera);
                    }
                    TimeCharged = 0;
                }
                //DEBUG
                if (cmd is GameplayInputCommand.DebugOn)
                {
                    debug = true;
                }

                if (cmd is GameplayInputCommand.DebugOff)
                {
                    debug = false;
                }

                if (cmd is GameplayInputCommand.PlayerMoveUp)
                {
                    player.Movement.yVelocity = -10;
                }
            });
        }

        public override void UpdateGameState(GameTime gameTime)
        {
            if (localGameTime.ElapsedGameTime == new TimeSpan(0))
            {
                startingTime = gameTime.TotalGameTime;
                localGameTime = gameTime;
            }
            else
            {
                localGameTime = gameTime;
            }
            elapsedTime = gameTime.TotalGameTime - startingTime;

            if (player.Position.position.X >= 5000)
            {
                player.cameraState = CameraMode.None;
                xGameBorderMin = 4050;
            }

            player.PlayerUpdate();
            player.Texture.SetTexture(LoadTexture(Animation.GetAnimation(player.GetAnimationState())));

            foreach (MovingRockHead rockHead in RockHeadCollisionList)
            {
                rockHead.Movement();
            }
            foreach (MovingSpikeHead spikeHead in SpikeHeadCollisionList)
            {
                spikeHead.Movement();
            }

            MouseState mouseState = Mouse.GetState();

            KeepPlayerInBounds();
            HandleCollisions();
            camera.Follow(player);
            UpdateCameraBasedObjects(gameTime);
        }

        private void UpdateCameraBasedObjects(GameTime gameTime)
        {
            backgroundImage.Position.SetPosition(camera.GetCameraBasedPosition(new Vector2(0, 0)));
            timerText.Position.SetPosition(camera.GetCameraBasedPosition(timerText.originalPosition));
            timerText.content = elapsedTime.ToString();

            debugText.Position.SetPosition(camera.GetCameraBasedPosition(debugText.originalPosition));
            debugText.content = checkPointTime.ToString();
        }

        private void HandleCollisions()
        {
            Collision.PlayerFullDetector(player, FullCollisionList);
            Collision.PlayerTopDetector(player, TopsCollisionList);
            Collision.PlayerSideDetector(player, SidesCollisionList);
            var returnRockHead = Collision.PlayerRockHeadDetector(player, RockHeadCollisionList);
            if (returnRockHead != null)
            {
                returnRockHead.Texture.SetTexture(LoadTexture("Enemies\\RockHeadMad"));
            }
            if (Collision.PlayerSpikeHeadDetector(player, SpikeHeadCollisionList))
            {
                RespawnPlayer();
            }
            if (Collision.PlayerDeathDetector(player, DeathCollisionList))
            {
                RespawnPlayer();
            }
            var returnCheckPoint = Collision.PlayerCheckPointDetector(player, CheckPointCollisionList);
            if (returnCheckPoint != null)
            {
                CheckPointActivation(returnCheckPoint);
            }
            if (Collision.PlayerEndFlagDetector(player, EndFlagCollisionList))
            {
                var dbSubmitRun = Database.SendRun(Convert.ToInt32(elapsedTime.TotalMilliseconds));
                dbSubmitRun.Wait();

                SwitchState(new MenuState());
            }
        }

        //creating objects in world
        private void AddObject(string TextureName, int PosX, int PosY, List<BasicObject> CollisionList)
        {
            BasicObject Object = new BasicObject(LoadTexture(TextureName), new Vector2(PosX, PosY), true);
            CollisionList.Add(Object);
            ObjectSpriteList.Add(Object);
            AddGameObject(Object);
        }

        private void AddObject(string TextureName, int PosX, int PosY)
        {
            BasicObject Object = new BasicObject(LoadTexture(TextureName), new Vector2(PosX, PosY));
            ObjectSpriteList.Add(Object);
            AddGameObject(Object);
        }

        private void AddRockHead(int PosX, int PosY, int MinPos, int MaxPos)
        {
            MovingRockHead rockHead = new MovingRockHead(LoadTexture("Enemies\\RockHeadIdle"), new Vector2(PosX, PosY), MinPos, MaxPos);
            RockHeadCollisionList.Add(rockHead);
            RockHeadSpriteList.Add(rockHead);
            AddGameObject(rockHead);
        }

        private void AddSpikeHead(int PosX, int PosY, int MinPosY, int MaxPosY)
        {
            MovingSpikeHead spikeHead = new MovingSpikeHead(LoadTexture("Enemies\\SpikeHead"), new Vector2(PosX, PosY), MinPosY, MaxPosY);
            SpikeHeadCollisionList.Add(spikeHead);
            SpikeHeadSpriteList.Add(spikeHead);
            AddGameObject(spikeHead);
        }

        private void AddCheckPoint(int PosX, int PosY)
        {
            CheckPoint checkPoint = new CheckPoint(LoadTexture(checkPointTexture), new Vector2(PosX, PosY));
            CheckPointCollisionList.Add(checkPoint);
            CheckPointList.Add(checkPoint);
            AddGameObject(checkPoint);
        }

        private Text AddText(string content, int PosX, int PosY)
        {
            Text textObject = new Text(content, new Vector2(PosX, PosY));
            AddTextObject(textObject);
            return textObject;
        }

        private void KeepPlayerInBounds()
        {
            if (player.Position.position.X < xGameBorderMin)
            {
                player.Position.SetPosition(new Vector2(xGameBorderMin, player.Position.position.Y));
            }
            if (player.Position.position.X + player.Texture.Width > xGameBorderMax)
            {
                player.Position.SetPosition(new Vector2(xGameBorderMax - player.Texture.Width, player.Position.position.Y));
            }
            if (player.Position.position.Y < yGameBorderMin)
            {
                player.Position.SetPosition(new Vector2(player.Position.position.X, yGameBorderMin));
                player.Movement.yVelocity /= 2;
            }
            if (player.Position.position.Y > yGameBorderMax)
            {
                RespawnPlayer();
            }
        }

        private void RespawnPlayer()
        {
            player.Movement.ResetVelocity();
            player.Position.SetPosition(spawnPoint);
            startingTime += elapsedTime;
            startingTime += -checkPointTime;
        }

        private void CheckPointActivation(CheckPoint checkPoint)
        {
            checkPoint.Texture.SetTexture(LoadTexture("Terrain\\CheckPointActivated"));
            spawnPoint = new Vector2(checkPoint.Position.position.X, checkPoint.Position.position.Y + (checkPoint.Texture.Height - player.Texture.Height));
            checkPoint.activated = true;
            checkPointTime = elapsedTime;
        }

        protected override void SetInputManager()
        {
            InputManager = new InputManager(new GameplayInputMapper());
        }

        private void LoadObjects()
        {
            AddGameObject(backgroundImage);
            //GUI
            timerText = AddText("Timer", 0, 20);
            debugText = AddText("Debugging", 0, 40);


            //First ground grass
            AddObject(woodenBoxLarge, 475, 705, FullCollisionList);
            AddObject(grassLeft, 0, 803, TopsCollisionList);
            AddObject(grassMiddle, 164, 803, TopsCollisionList);
            AddObject(grassMiddle, 292, 803, TopsCollisionList);
            AddObject(grassRight, 420, 803, TopsCollisionList);
            AddObject(grassLeft, 572, 623, FullCollisionList);
            AddObject(grassRight, 736, 623, FullCollisionList);
            //spikes
            AddObject(spike, 889, 861, DeathCollisionList);
            AddObject(spike, 919, 861, DeathCollisionList);
            AddObject(spike, 949, 861, DeathCollisionList);
            AddObject(spike, 979, 861, DeathCollisionList);
            AddObject(spike, 1009, 861, DeathCollisionList);
            AddObject(spike, 1039, 861, DeathCollisionList);
            //Grass under spikes
            AddObject(grassLeft, 880, 892);
            AddObject(grassRight, 1044, 892);
            //First hill soil
            AddObject(grassSoilLeft, 572, 807);
            AddObject(grassSoilLeft, 572, 927);
            AddObject(grassSoilLeft, 572, 1047);
            AddObject(grassSoilRight, 736, 807, SidesCollisionList);
            AddObject(grassSoilRight, 736, 927);
            AddObject(grassSoilRight, 736, 1047);
            //First ground soil
            AddObject(grassSoilLeft, 0, 987);
            AddObject(grassSoilMiddle, 164, 987);
            AddObject(grassSoilMiddle, 292, 987);
            AddObject(grassSoilRight, 420, 987);
            //post spikes hill
            AddObject(grassLeft, 1071, 536, FullCollisionList);
            AddObject(grassSoilLeft, 1071, 720, SidesCollisionList);
            AddObject(grassSoilLeft, 1071, 840);
            AddObject(grassSoilLeft, 1071, 960);
            AddObject(grassRight, 1235, 536, FullCollisionList);
            AddObject(grassSoilRight, 1235, 720, SidesCollisionList);
            AddObject(grassSoilRight, 1235, 840, SidesCollisionList);
            AddObject(grassSoilRight, 1235, 960, SidesCollisionList);
            AddRockHead(1400, 536, 1400, 1800);
            //cave rock thing ceiling
            AddObject(stoneCubeLarge, 1973, 27, FullCollisionList);
            AddObject(stoneCubeLarge, 2101, 27);
            AddObject(stoneCubeLarge, 2037, 155, FullCollisionList);
            AddObject(stoneCubeLarge, 2165, 155);
            AddObject(stoneCubeLarge, 2292, 155);
            AddObject(stoneCubeLarge, 2229, 27);
            AddObject(stoneCubeLarge, 2420, 118);
            AddObject(stoneCubeLarge, 2420, -10);
            AddObject(stoneSlabVertical, 2357, -37);
            AddObject(stoneCubeLarge, 2037, 283, FullCollisionList);
            AddObject(stoneCubeLarge, 2100, 411, FullCollisionList);
            AddObject(stoneSlabHorizontal, 2228, 411, FullCollisionList);
            AddObject(stoneCubeLarge, 2165, 283);
            AddObject(stoneCubeLarge, 2292, 283, SidesCollisionList);
            AddObject(stoneCubeLarge, 2420, 246, FullCollisionList);
            AddObject(stoneSlabVertical, 2548, 132, FullCollisionList);
            AddObject(stoneSlabVertical, 2548, -60);
            AddObject(stoneSlabVertical, 2612, 55, FullCollisionList);
            AddObject(stoneSlabVertical, 2612, -137);
            AddObject(stoneCubeLarge, 2676, 27, TopsCollisionList);
            AddObject(stoneCubeLarge, 2804, 27, TopsCollisionList);
            AddObject(stoneCubeLarge, 2932, 27, TopsCollisionList);
            AddObject(stoneCubeLarge, 2676, 27, TopsCollisionList);
            AddObject(stoneCubeLarge, 3060, 27, TopsCollisionList);
            AddObject(stoneCubeLarge, 3188, 27, TopsCollisionList);
            AddObject(stoneCubeLarge, 3316, 27, TopsCollisionList);
            AddObject(stoneCubeLarge, 3444, 27, TopsCollisionList);
            AddObject(stoneCubeLarge, 3572, 27, TopsCollisionList);
            AddObject(stoneCubeLarge, 3700, 27, TopsCollisionList);
            //cave rock thing floor
            AddObject(stoneSlabHorizontal, 2236, 900, FullCollisionList);
            AddObject(stoneCubeLarge, 2300, 964, SidesCollisionList);
            AddObject(stoneCubeLarge, 2428, 900, FullCollisionList);
            AddObject(stoneCubeLarge, 2428, 1028);
            AddObject(stoneCubeLarge, 2556, 964, FullCollisionList);
            AddObject(stoneSlabHorizontal, 2684, 1012, FullCollisionList);
            //cave spikes
            AddObject(spike, 2876, 1030, DeathCollisionList);
            AddObject(spike, 2906, 1030, DeathCollisionList);
            AddObject(spike, 2936, 1030, DeathCollisionList);
            AddObject(spike, 2966, 1030, DeathCollisionList);
            AddObject(spike, 2996, 1030, DeathCollisionList);
            AddObject(spike, 3026, 1030, DeathCollisionList);
            AddObject(spike, 3056, 1030, DeathCollisionList);
            AddObject(spike, 3086, 1030, DeathCollisionList);
            AddObject(spike, 3116, 1030, DeathCollisionList);
            AddObject(spike, 3146, 1030, DeathCollisionList);
            AddObject(spike, 3176, 1030, DeathCollisionList);
            AddObject(spike, 3206, 1030, DeathCollisionList);
            AddObject(spike, 3236, 1030, DeathCollisionList);
            AddObject(spike, 3266, 1030, DeathCollisionList);
            AddObject(spike, 3296, 1030, DeathCollisionList);
            AddObject(spike, 3326, 1030, DeathCollisionList);
            AddObject(spike, 3356, 1030, DeathCollisionList);
            AddObject(spike, 3386, 1030, DeathCollisionList);
            AddObject(spike, 3416, 1030, DeathCollisionList);
            AddObject(stoneSlabHorizontal, 2875, 1060);
            AddObject(stoneSlabHorizontal, 3067, 1060);
            AddObject(stoneSlabHorizontal, 3259, 1060);
            AddSpikeHead(2950, 450, 400, 900);
            AddSpikeHead(3200, 700, 400, 900);
            //post spikes
            AddObject(stoneCubeLarge, 3450, 980, FullCollisionList);
            AddObject(stoneCubeLarge, 3578, 980, FullCollisionList);
            AddObject(stoneSlabVertical, 3706, 920, FullCollisionList);
            AddObject(stoneCubeLarge, 3770, 920, FullCollisionList);
            AddObject(grassLeft, 4020, 1000, TopsCollisionList);
            AddObject(stoneCubeLarge, 3898, 920, FullCollisionList);
            AddObject(stoneCubeLarge, 3770, 1048);
            AddObject(stoneCubeLarge, 3898, 1048);
            //boss arena
            AddObject(grassMiddle, 4184, 1000, TopsCollisionList);
            AddObject(grassMiddle, 4312, 1000, TopsCollisionList);
            AddObject(grassMiddle, 4440, 1000, TopsCollisionList);
            AddObject(grassMiddle, 4568, 1000, TopsCollisionList);
            AddObject(grassMiddle, 4696, 1000, TopsCollisionList);
            AddObject(grassMiddle, 4184, 1000, TopsCollisionList);
            AddObject(grassMiddle, 4312, 1000, TopsCollisionList);
            AddObject(grassMiddle, 4440, 1000, TopsCollisionList);
            AddObject(grassMiddle, 4568, 1000, TopsCollisionList);
            AddObject(grassMiddle, 4696, 1000, TopsCollisionList);
            AddObject(grassMiddle, 4824, 1000, TopsCollisionList);
            AddObject(grassMiddle, 4952, 1000, TopsCollisionList);
            AddObject(grassMiddle, 5080, 1000, TopsCollisionList);
            AddObject(grassMiddle, 5208, 1000, TopsCollisionList);
            AddObject(grassMiddle, 5336, 1000, TopsCollisionList);
            AddObject(grassMiddle, 5464, 1000, TopsCollisionList);
            AddObject(grassMiddle, 5592, 1000, TopsCollisionList);
            AddObject(grassMiddle, 5720, 1000, TopsCollisionList);
            AddObject(grassRight, 5848, 1000, TopsCollisionList);

            AddCheckPoint(3800, 848);
            AddGameObject(endFlag);
            AddGameObject(player);
            RespawnPlayer();
        }

        public override void RenderBoundingBoxes(SpriteBatch spriteBatch)
        {
            if (debug)
            {
                player.BoundingBox.RenderBoundingBoxes(spriteBatch);
                foreach (var item in FullCollisionList)
                {
                    item.BoundingBox.RenderBoundingBoxes(spriteBatch);
                }
                foreach (var item in SidesCollisionList)
                {
                    item.BoundingBox.RenderBoundingBoxes(spriteBatch);
                }
                foreach (var item in TopsCollisionList)
                {
                    item.BoundingBox.RenderBoundingBoxes(spriteBatch);
                }
                foreach (var item in SpikeHeadCollisionList)
                {
                    item.BoundingBox.RenderBoundingBoxes(spriteBatch);
                }
                foreach (var item in RockHeadCollisionList)
                {
                    item.BoundingBox.RenderBoundingBoxes(spriteBatch);
                }
                foreach (var item in DeathCollisionList)
                {
                    item.BoundingBox.RenderBoundingBoxes(spriteBatch);
                }
            }
        }
    }
}
