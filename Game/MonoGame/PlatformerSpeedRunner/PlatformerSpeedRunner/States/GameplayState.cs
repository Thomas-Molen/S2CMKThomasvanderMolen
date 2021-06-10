using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PlatformerSpeedRunner.Enum;
using PlatformerSpeedRunner.Helper;
using PlatformerSpeedRunner.Input;
using PlatformerSpeedRunner.Input.Base;
using PlatformerSpeedRunner.Objects;
using PlatformerSpeedRunner.Objects.Base;
using PlatformerSpeedRunner.States.Base;
using System;
using System.Collections.Generic;

namespace PlatformerSpeedRunner.States
{
    public class GameplayState : BaseGameState
    {
        bool gameEnd = false;
        bool exitable = false;
        bool restartable = false;
        private Player player;
        private CollisionHelper Collision;
        private AnimationHelper Animation;
        private DatabaseHelper Database;
        private ChargeCircleHelper Charge;
        private BasicObject chargeCircle;
        private BasicObject endFlag;
        private TimeSpan startingTime;
        private TimeSpan elapsedTime;
        private TimeSpan checkPointTime = new TimeSpan(0);
        private GameTime localGameTime;
        private Text timerText;
        private CheckPoint checkPoint;

        private int timeCharged;
        //private Vector2 spawnPoint = new Vector2(200, 750);
        private Vector2 spawnPoint = new Vector2(4800, 750);

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
        private readonly List<RenderAbleObject> TopsCollisionList = new List<RenderAbleObject>();
        private readonly List<RenderAbleObject> SidesCollisionList = new List<RenderAbleObject>();
        private readonly List<RenderAbleObject> FullCollisionList = new List<RenderAbleObject>();
        private readonly List<RenderAbleObject> DeathCollisionList = new List<RenderAbleObject>();
        private readonly List<RenderAbleObject> RockHeadCollisionList = new List<RenderAbleObject>();
        private readonly List<RenderAbleObject> SpikeHeadCollisionList = new List<RenderAbleObject>();
        private readonly List<CheckPoint> CheckPointCollisionList = new List<CheckPoint>();
        private readonly List<RenderAbleObject> EndFlagCollisionList = new List<RenderAbleObject>();

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
            chargeCircle = new BasicObject(LoadTexture("Player\\Charging\\ChargingCircle1"), new Vector2(-50, -50));
            endFlag = new BasicObject(LoadTexture("Terrain\\EndFlag"), new Vector2(5720, 960), true);

            Charge = new ChargeCircleHelper(chargeCircle);

            backgroundImage = new BasicObject(LoadTexture("Backgrounds\\PinkWallpaper"), new Vector2(0, 0));

            localGameTime = new GameTime();

            LoadWorld();
        }

        public override void HandleInput()
        {
            InputManager.GetCommands(cmd =>
            {
                if (cmd is GameplayInputCommand.ExitDown)
                {
                    exitable = true;
                }
                if (cmd is GameplayInputCommand.ExitUp && exitable)
                {
                    SwitchState(new MenuState());
                }
                if (cmd is GameplayInputCommand.RestartDown)
                {
                    restartable = true;
                }
                if (cmd is GameplayInputCommand.RestartUp && restartable)
                {
                    SwitchState(new GameplayState());
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
                    timeCharged++;
                    Charge.AnimateCharge(timeCharged, player, this);
                }
                if (cmd is GameplayInputCommand.PlayerLMBRelease)
                {
                    chargeCircle.Position.SetPosition(new Vector2(-50, -50));
                    RemoveGameObject(chargeCircle);
                    player.Grapple(timeCharged, camera);
                    timeCharged = 0;
                }
            });
        }

        public override void UpdateGameState(GameTime gameTime)
        {
            if (gameEnd)
            {
                try
                {
                    Database.SendRun(Convert.ToInt32(elapsedTime.TotalMilliseconds)).Wait();
                    SwitchState(new MenuState());
                }
                catch (Exception)
                {
                    SwitchState(new MenuState());
                }
            }

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

            KeepPlayerInBounds();
            HandleCollisions();
            camera.Follow(player);
            UpdateCameraBasedObjects();
        }

        private void UpdateCameraBasedObjects()
        {
            backgroundImage.Position.SetPosition(camera.GetCameraBasedPosition(new Vector2(0, 0)));
            timerText.Position.SetPosition(camera.GetCameraBasedPosition(timerText.originalPosition));
            string currentTime = elapsedTime.ToString().Substring(0, elapsedTime.ToString().Length - 4);
            timerText.content = "Time: " + currentTime;
        }

        private void HandleCollisions()
        {
            Collision.PlayerFullDetector(player, FullCollisionList);
            Collision.PlayerTopDetector(player, TopsCollisionList);
            Collision.PlayerSideDetector(player, SidesCollisionList);
            Collision.PlayerRockHeadDetector(player, RockHeadCollisionList);
            if (Collision.PlayerSpikeHeadDetector(player, SpikeHeadCollisionList) || Collision.PlayerSpikeHeadDetector(player, DeathCollisionList))
            {
                RespawnPlayer();
            }
            if (Collision.PlayerCheckPointDetector(player, checkPoint))
            {
                CheckPointActivation(checkPoint);
            }
            if (Collision.PlayerEndFlagDetector(player, EndFlagCollisionList))
            {
                Vector2 submitTextPosition = camera.GetCameraBasedPosition(new Vector2(800, 500));
                AddText("Submitting run...", (int)submitTextPosition.X, (int)submitTextPosition.Y);
                gameEnd = true;
            }
        }

        //creating objects in world
        private void AddObject(string TextureName, int PosX, int PosY, List<RenderAbleObject> CollisionList)
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
            MovingRockHead rockHead = new MovingRockHead(LoadTexture("Enemies\\RockHeadIdle"), LoadTexture("Enemies\\RockHeadMad"), new Vector2(PosX, PosY), MinPos, MaxPos);
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

        private void SetCheckPoint(int PosX, int PosY)
        {
            checkPoint = new CheckPoint(LoadTexture(checkPointTexture), new Vector2(PosX, PosY));
            AddGameObject(checkPoint);
        }

        private Text AddText(string content, int PosX, int PosY)
        {
            Text textObject = new Text(content, new Vector2(PosX, PosY), Fonts.CalibriBold25);
            AddTextObject(textObject);
            return textObject;
        }

        private void KeepPlayerInBounds()
        {
            player.Movement.KeepPlayerInbound(player, xGameBorderMin, xGameBorderMax, yGameBorderMin);
            if (player.Position.position.Y > yGameBorderMax)
            {
                RespawnPlayer();
            }
        }

        private void RespawnPlayer()
        {
            if (checkPoint.activated)
            {
                player.Movement.ResetVelocity();
                startingTime += elapsedTime;
                startingTime += -checkPointTime;
                player.Position.SetPosition(spawnPoint);
            }
            else
            {
                SwitchState(new GameplayState());
            }
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

        private void LoadWorld()
        {
            AddGameObject(backgroundImage);
            //GUI
            timerText = AddText("Timer", 0, 10);

            //TutorialIcons
            AddObject("Menu\\TutorialMovementIcon", 100, 400);
            AddObject("Menu\\TutorialMouseIcon", 280, 400);
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
            AddSpikeHead(2900, 450, 400, 900);
            AddSpikeHead(3100, 800, 400, 900);
            AddSpikeHead(3300, 500, 400, 900);
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

            SetCheckPoint(3800, 848);
            AddGameObject(endFlag);
            EndFlagCollisionList.Add(endFlag);
            AddGameObject(player);
            player.Position.SetPosition(spawnPoint);
        }
    }
}
