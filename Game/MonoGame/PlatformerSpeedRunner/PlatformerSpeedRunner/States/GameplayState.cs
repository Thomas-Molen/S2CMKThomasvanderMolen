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
        private CollisionHelper collisionHelper;
        private PlayerSprite playerSprite;
        private AnimationHelper animationHelper;
        private ObjectSprite chargeCircleSprite;
        private ObjectSprite endFlag;
        private TimeSpan startingTime;
        private TimeSpan elapsedTime;
        private TimeSpan checkPointTime = new TimeSpan(0);
        private GameTime localGameTime;
        private TextObject timerText;
        private TextObject debugText;

        private int TimeCharged;
        //TODO THIS IS THE REAL SPAWNPOINT
        private Vector2 spawnPoint = new Vector2(200, 745);
        //private Vector2 spawnPoint = new Vector2(3900, 800);

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

        private readonly List<ObjectSprite> ObjectSpriteList = new List<ObjectSprite>();
        private readonly List<RockHeadSprite> RockHeadSpriteList = new List<RockHeadSprite>();
        private readonly List<SpikeHeadSprite> SpikeHeadSpriteList = new List<SpikeHeadSprite>();
        private readonly List<CheckPoint> CheckPointList = new List<CheckPoint>();

        private readonly List<ObjectSprite> TopsCollisionList = new List<ObjectSprite>();
        private readonly List<ObjectSprite> SidesCollisionList = new List<ObjectSprite>();
        private readonly List<ObjectSprite> FullCollisionList = new List<ObjectSprite>();
        private readonly List<ObjectSprite> DeathCollisionList = new List<ObjectSprite>();
        private readonly List<RockHeadSprite> RockHeadCollisionList = new List<RockHeadSprite>();
        private readonly List<SpikeHeadSprite> SpikeHeadCollisionList = new List<SpikeHeadSprite>();
        private readonly List<CheckPoint> CheckPointCollisionList = new List<CheckPoint>();
        private readonly List<ObjectSprite> EndFlagCollisionList = new List<ObjectSprite>();

        private int xGameBorderMin = 23;
        private int xGameBorderMax = 5980;
        private int yGameBorderMax = Program.height;
        private int yGameBorderMin = 25;

        public override void LoadContent()
        {
            collisionHelper = new CollisionHelper();
            animationHelper = new AnimationHelper();
            playerSprite = new PlayerSprite(LoadTexture("Player\\Idle\\IdlePinkMan"));

            chargeCircleSprite = new ObjectSprite(LoadTexture("Player\\Charging\\ChargingCircle1"), new Vector2(0, 0));

            endFlag = new ObjectSprite(LoadTexture("Terrain\\EndFlag"), new Vector2(5720, 950), true);
            EndFlagCollisionList.Add(endFlag);

            backgroundImage = new ObjectSprite(LoadTexture("Backgrounds\\PinkWallpaper"), new Vector2(0, 0));

            localGameTime = new GameTime();

            LoadObjects();
        }

        public override void HandleInput()
        {
            InputManager.GetCommands(cmd =>
            {
                if (cmd is GameplayInputCommand.GameExit)
                {
                    NotifyEvent(Events.GAME_QUIT);
                }

                if (cmd is GameplayInputCommand.PlayerMoveLeft)
                {
                    playerSprite.MoveLeft();
                }

                if (cmd is GameplayInputCommand.PlayerMoveRight)
                {
                    playerSprite.MoveRight();
                }

                if (cmd is GameplayInputCommand.PlayerMoveNone)
                {
                    playerSprite.NoDirection();
                }

                if (cmd is GameplayInputCommand.PlayerLMBHold)
                {
                    AddGameObject(chargeCircleSprite);
                    TimeCharged += 1;

                    chargeCircleSprite.positionHelper.SetPosition(new Vector2(playerSprite.GetPosition().X, playerSprite.GetPosition().Y - 10));

                    if (TimeCharged < 10)
                    {
                        chargeCircleSprite.textureHelper.SetTexture(LoadTexture("Player\\Charging\\ChargingCircle1"));
                    }
                    else if (TimeCharged < 20)
                    {
                        chargeCircleSprite.textureHelper.SetTexture(LoadTexture("Player\\Charging\\ChargingCircle2"));
                    }
                    else if (TimeCharged < 30)
                    {
                        chargeCircleSprite.textureHelper.SetTexture(LoadTexture("Player\\Charging\\ChargingCircle3"));
                    }
                    else if (TimeCharged >= 30)
                    {
                        chargeCircleSprite.textureHelper.SetTexture(LoadTexture("Player\\Charging\\ChargingCircle4"));
                    }
                }
                if (cmd is GameplayInputCommand.PlayerLMBRelease)
                {
                    chargeCircleSprite.positionHelper.SetPosition(new Vector2(chargeCircleSprite.positionHelper.position.X, chargeCircleSprite.positionHelper.position.X + 2000));
                    RemoveGameObject(chargeCircleSprite);
                    if (TimeCharged >= 30)
                    {
                        playerSprite.Grapple(30, camera);
                    }
                    else if (TimeCharged < 10)
                    { }
                    else
                    {
                        playerSprite.Grapple(TimeCharged, camera);
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
                    playerSprite.yVelocity = -10;
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

            if (playerSprite.GetPosition().X >= 5000)
            {
                playerSprite.cameraState = CameraMode.None;
                xGameBorderMin = 4050;
            }

            playerSprite.PlayerPhysics();
            playerSprite.ChangeTexture(LoadTexture(animationHelper.GetAnimation(playerSprite.GetAnimationState())));

            foreach (RockHeadSprite rockHead in RockHeadCollisionList)
            {
                rockHead.Movement();
            }
            foreach (SpikeHeadSprite spikeHead in SpikeHeadCollisionList)
            {
                spikeHead.Movement();
            }

            MouseState mouseState = Mouse.GetState();

            KeepPlayerInBounds();
            HandleCollisions();
            camera.Follow(playerSprite);
            UpdateCameraBasedObjects(gameTime);
        }

        private void UpdateCameraBasedObjects(GameTime gameTime)
        {
            backgroundImage.positionHelper.SetPosition(camera.GetCameraBasedPosition(new Vector2(0, 0)));
            timerText.positionHelper.SetPosition(camera.GetCameraBasedPosition(timerText.originalPosition));
            timerText.content = elapsedTime.ToString();

            debugText.positionHelper.SetPosition(camera.GetCameraBasedPosition(debugText.originalPosition));
            debugText.content = checkPointTime.ToString();
        }

        private void HandleCollisions()
        {
            collisionHelper.PlayerFullDetector(playerSprite, FullCollisionList);
            collisionHelper.PlayerTopDetector(playerSprite, TopsCollisionList);
            collisionHelper.PlayerSideDetector(playerSprite, SidesCollisionList);
            var returnRockHead = collisionHelper.PlayerRockHeadDetector(playerSprite, RockHeadCollisionList);
            if (returnRockHead != null)
            {
                returnRockHead.textureHelper.SetTexture(LoadTexture("Enemies\\RockHeadMad"));
            }
            if (collisionHelper.PlayerSpikeHeadDetector(playerSprite, SpikeHeadCollisionList))
            {
                RespawnPlayer();
            }
            if (collisionHelper.PlayerDeathDetector(playerSprite, DeathCollisionList))
            {
                RespawnPlayer();
            }
            var returnCheckPoint = collisionHelper.PlayerCheckPointDetector(playerSprite, CheckPointCollisionList);
            if (returnCheckPoint != null)
            {
                CheckPointActivation(returnCheckPoint);
            }
            if (collisionHelper.PlayerEndFlagDetector(playerSprite, EndFlagCollisionList))
            {
                SwitchState(new SplashState());
            }
        }

        //creating objects in world
        private void AddObject(string TextureName, int PosX, int PosY, List<ObjectSprite> CollisionList)
        {
            ObjectSprite Object = new ObjectSprite(LoadTexture(TextureName), new Vector2(PosX, PosY), true);
            CollisionList.Add(Object);
            ObjectSpriteList.Add(Object);
            AddGameObject(Object);
        }

        private void AddObject(string TextureName, int PosX, int PosY)
        {
            ObjectSprite Object = new ObjectSprite(LoadTexture(TextureName), new Vector2(PosX, PosY));
            ObjectSpriteList.Add(Object);
            AddGameObject(Object);
        }

        private void AddRockHead(int PosX, int PosY, int MinPos, int MaxPos)
        {
            RockHeadSprite rockHead = new RockHeadSprite(LoadTexture("Enemies\\RockHeadIdle"), new Vector2(PosX, PosY), MinPos, MaxPos);
            RockHeadCollisionList.Add(rockHead);
            RockHeadSpriteList.Add(rockHead);
            AddGameObject(rockHead);
        }

        private void AddSpikeHead(int PosX, int PosY, int MinPosY, int MaxPosY)
        {
            SpikeHeadSprite spikeHead = new SpikeHeadSprite(LoadTexture("Enemies\\SpikeHead"), new Vector2(PosX, PosY), MinPosY, MaxPosY);
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

        private TextObject AddText(string content, int PosX, int PosY)
        {
            TextObject textObject = new TextObject(content, new Vector2(PosX, PosY));
            AddTextObject(textObject);
            return textObject;
        }

        private void KeepPlayerInBounds()
        {
            if (playerSprite.GetPosition().X < xGameBorderMin)
            {
                playerSprite.SetPosition(new Vector2(xGameBorderMin, playerSprite.GetPosition().Y));
            }
            if (playerSprite.GetPosition().X + playerSprite.textureHelper.Width > xGameBorderMax)
            {
                playerSprite.SetPosition(new Vector2(xGameBorderMax - playerSprite.textureHelper.Width, playerSprite.GetPosition().Y));
            }
            if (playerSprite.GetPosition().Y < yGameBorderMin)
            {
                playerSprite.SetPosition(new Vector2(playerSprite.GetPosition().X, yGameBorderMin));
                playerSprite.yVelocity /= 2;
            }
            if (playerSprite.GetPosition().Y > yGameBorderMax)
            {
                RespawnPlayer();
            }
        }

        private void RespawnPlayer()
        {
            playerSprite.xVelocity = 0;
            playerSprite.yVelocity = 0;
            playerSprite.SetPosition(spawnPoint);
            startingTime += elapsedTime;
            startingTime += -checkPointTime;
        }

        private void CheckPointActivation(CheckPoint checkPoint)
        {
            checkPoint.textureHelper.SetTexture(LoadTexture("Terrain\\CheckPointActivated"));
            spawnPoint = new Vector2(checkPoint.positionHelper.position.X, checkPoint.positionHelper.position.Y + (checkPoint.textureHelper.Height - playerSprite.textureHelper.Height));
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
            AddGameObject(playerSprite);
        }
    }
}
