using Microsoft.Xna.Framework;
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
        private bool gameEnd = false;
        private bool exitable = false;
        private bool restartable = false;
        private Player player;
        private CollisionHelper Collision;
        private DatabaseHelper Database;
        private ChargeCircleHelper Charge;
        private TimerHelper Timer;

        //global objects
        private BasicObject chargeCircle;
        private BasicObject endFlag;
        private Text timerText;
        private CheckPoint checkPoint;

        //level borders
        private int xGameBorderMin = 23;
        private int xGameBorderMax = 5980;
        private int yGameBorderMax = Program.height;
        private int yGameBorderMin = 25;

        public override void LoadContent()
        {
            Collision = new CollisionHelper();
            Database = new DatabaseHelper();
            Timer = new TimerHelper();
            player = new Player(baseContentManager);
            chargeCircle = new BasicObject(baseContentManager, Textures.chargingCircle1, new Vector2(-50, -50));
            endFlag = new BasicObject(baseContentManager, Textures.endFlag, new Vector2(5720, 960), true);
            backgroundImage = new BasicObject(baseContentManager, Textures.pinkBackground, new Vector2(0, 0));
            Charge = new ChargeCircleHelper(chargeCircle);

            camera.SetCameraMode(CameraMode.Horizontal);
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
                    Charge.AnimateCharge(player);
                }
                if (cmd is GameplayInputCommand.PlayerLMBRelease)
                {
                    chargeCircle.Position.SetPosition(new Vector2(-50, -50));
                    RemoveGameObject(chargeCircle);
                    player.Movement.Grapple(Charge, player, camera);
                }
            });
        }

        public override void UpdateGameState(GameTime gameTime)
        {
            if (gameEnd)
            {
                SubmitRun();
            }
            Timer.UpdateTimer(gameTime);
            CheckWorldEvents();
            player.PlayerUpdate();

            foreach (MovingRockHead rockHead in Collision.RockHeadCollisionList)
            {
                rockHead.EnemyUpdate();
            }
            foreach (MovingSpikeHead spikeHead in Collision.SpikeHeadCollisionList)
            {
                spikeHead.EnemyUpdate();
            }
            player.Movement.KeepPlayerInbound(player, xGameBorderMin, xGameBorderMax, yGameBorderMin);
            HandleCollisions();
            camera.Follow(player);
            UpdateCameraBasedObjects();
        }

        private void UpdateCameraBasedObjects()
        {
            backgroundImage.Position.SetPosition(camera.GetCameraBasedPosition(new Vector2(0, 0)));
            timerText.Position.SetPosition(camera.GetCameraBasedPosition(timerText.originalPosition));
            timerText.content = "Time: " + Timer.GetElapsedTimeString();
        }

        private void HandleCollisions()
        {
            Collision.PlayerFullDetector(player, Collision.FullCollisionList);
            Collision.PlayerTopDetector(player, Collision.TopsCollisionList);
            Collision.PlayerSideDetector(player, Collision.SidesCollisionList);
            Collision.PlayerRockHeadDetector(player, Collision.RockHeadCollisionList);
            if (Collision.PlayerBooleanDetector(player, Collision.SpikeHeadCollisionList) || Collision.PlayerBooleanDetector(player, Collision.DeathCollisionList))
            {
                RespawnPlayer();
            }
            if (Collision.PlayerBooleanDetector(player, checkPoint) && !checkPoint.activated)
            {
                CheckPointActivation(checkPoint);
            }
            if (Collision.PlayerBooleanDetector(player, Collision.EndFlagCollisionList))
            {
                Vector2 submitTextPosition = camera.GetCameraBasedPosition(new Vector2(800, 500));
                AddText("Submitting run...", (int)submitTextPosition.X, (int)submitTextPosition.Y);
                gameEnd = true;
            }
        }

        //creating objects in world
        private void AddObject(string TextureName, int PosX, int PosY, List<RenderAbleObject> CollisionList)
        {
            BasicObject Object = new BasicObject(baseContentManager, TextureName, new Vector2(PosX, PosY), true);
            CollisionList.Add(Object);
            AddGameObject(Object);
        }

        private void AddObject(string TextureName, int PosX, int PosY)
        {
            BasicObject Object = new BasicObject(baseContentManager, TextureName, new Vector2(PosX, PosY));
            AddGameObject(Object);
        }

        private void AddEnemyHead(int posX, int posY, int minPos, int maxPos, bool spiked = false)
        {
            RenderAbleObject enemyHead;
            if (spiked)
            {
                enemyHead = new MovingSpikeHead(baseContentManager, new Vector2(posX, posY), minPos, maxPos);
                Collision.SpikeHeadCollisionList.Add(enemyHead);
            }
            else
            {
                enemyHead = new MovingRockHead(baseContentManager, new Vector2(posX, posY), minPos, maxPos);
                Collision.RockHeadCollisionList.Add(enemyHead);
            }
            AddGameObject(enemyHead);
        }

        private void SetCheckPoint(int PosX, int PosY)
        {
            checkPoint = new CheckPoint(baseContentManager, new Vector2(PosX, PosY));
            AddGameObject(checkPoint);
        }

        private Text AddText(string content, int PosX, int PosY)
        {
            Text textObject = new Text(content, new Vector2(PosX, PosY), Fonts.CalibriBold25);
            AddTextObject(textObject);
            return textObject;
        }

        private void RespawnPlayer()
        {
            if (checkPoint.activated)
            {
                player.Respawn();
                Timer.ResetCheckpointTimer();
            }
            else
            {
                SwitchState(new GameplayState());
            }
        }

        private void CheckPointActivation(CheckPoint checkPoint)
        {
            player.SetSpawnPoint(new Vector2(checkPoint.Position.position.X, checkPoint.Position.position.Y + (checkPoint.Texture.height - player.Texture.height)));
            checkPoint.Activate();
            Timer.SetCheckpointTime();
        }

        private void SubmitRun()
        {
            try
            {
                Database.SendRun(Convert.ToInt32(Timer.elapsedTime.TotalMilliseconds)).Wait();
                SwitchState(new MenuState());
            }
            catch (Exception)
            {
                SwitchState(new MenuState());
            }
        }

        private void CheckWorldEvents()
        {
            if (player.Position.position.X >= 5000)
            {
                camera.SetCameraMode(CameraMode.None);
                xGameBorderMin = 4050;
            }
            if (player.Position.position.Y > yGameBorderMax)
            {
                RespawnPlayer();
            }
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
            AddObject(Textures.tutorialMovementIcon, 100, 400);
            AddObject(Textures.tutorialMouseIcon, 280, 400);
            //First ground grass
            AddObject(Textures.woodenBoxLarge, 475, 705, Collision.FullCollisionList);
            AddObject(Textures.grassLeft, 0, 803, Collision.TopsCollisionList);
            AddObject(Textures.grassMiddle, 164, 803, Collision.TopsCollisionList);
            AddObject(Textures.grassMiddle, 292, 803, Collision.TopsCollisionList);
            AddObject(Textures.grassRight, 420, 803, Collision.TopsCollisionList);
            AddObject(Textures.grassLeft, 572, 623, Collision.FullCollisionList);
            AddObject(Textures.grassRight, 736, 623, Collision.FullCollisionList);
            //spikes
            AddObject(Textures.spike, 889, 861, Collision.DeathCollisionList);
            AddObject(Textures.spike, 919, 861, Collision.DeathCollisionList);
            AddObject(Textures.spike, 949, 861, Collision.DeathCollisionList);
            AddObject(Textures.spike, 979, 861, Collision.DeathCollisionList);
            AddObject(Textures.spike, 1009, 861, Collision.DeathCollisionList);
            AddObject(Textures.spike, 1039, 861, Collision.DeathCollisionList);
            //Grass under spikes
            AddObject(Textures.grassLeft, 880, 892);
            AddObject(Textures.grassRight, 1044, 892);
            //First hill soil
            AddObject(Textures.grassSoilLeft, 572, 807);
            AddObject(Textures.grassSoilLeft, 572, 927);
            AddObject(Textures.grassSoilLeft, 572, 1047);
            AddObject(Textures.grassSoilRight, 736, 807, Collision.SidesCollisionList);
            AddObject(Textures.grassSoilRight, 736, 927);
            AddObject(Textures.grassSoilRight, 736, 1047);
            //First ground soil
            AddObject(Textures.grassSoilLeft, 0, 987);
            AddObject(Textures.grassSoilRight, 164, 987);
            AddObject(Textures.grassSoilMiddle, 292, 987);
            AddObject(Textures.grassSoilRight, 420, 987);
            //post spikes hill
            AddObject(Textures.grassLeft, 1071, 536, Collision.FullCollisionList);
            AddObject(Textures.grassSoilLeft, 1071, 720, Collision.SidesCollisionList);
            AddObject(Textures.grassSoilLeft, 1071, 840);
            AddObject(Textures.grassSoilLeft, 1071, 960);
            AddObject(Textures.grassRight, 1235, 536, Collision.FullCollisionList);
            AddObject(Textures.grassSoilRight, 1235, 720, Collision.SidesCollisionList);
            AddObject(Textures.grassSoilRight, 1235, 840, Collision.SidesCollisionList);
            AddObject(Textures.grassSoilRight, 1235, 960, Collision.SidesCollisionList);
            AddEnemyHead(1400, 536, 1400, 1800);
            //cave rock thing ceiling
            AddObject(Textures.stoneCubeLarge, 1973, 27, Collision.FullCollisionList);
            AddObject(Textures.stoneCubeLarge, 2101, 27);
            AddObject(Textures.stoneCubeLarge, 2037, 155, Collision.FullCollisionList);
            AddObject(Textures.stoneCubeLarge, 2165, 155);
            AddObject(Textures.stoneCubeLarge, 2292, 155);
            AddObject(Textures.stoneCubeLarge, 2229, 27);
            AddObject(Textures.stoneCubeLarge, 2420, 118);
            AddObject(Textures.stoneCubeLarge, 2420, -10);
            AddObject(Textures.stoneSlabVertical, 2357, -37);
            AddObject(Textures.stoneCubeLarge, 2037, 283, Collision.FullCollisionList);
            AddObject(Textures.stoneCubeLarge, 2100, 411, Collision.FullCollisionList);
            AddObject(Textures.stoneSlabHorizontal, 2228, 411, Collision.FullCollisionList);
            AddObject(Textures.stoneCubeLarge, 2165, 283);
            AddObject(Textures.stoneCubeLarge, 2292, 283, Collision.SidesCollisionList);
            AddObject(Textures.stoneCubeLarge, 2420, 246, Collision.FullCollisionList);
            AddObject(Textures.stoneSlabVertical, 2548, 132, Collision.FullCollisionList);
            AddObject(Textures.stoneSlabVertical, 2548, -60);
            AddObject(Textures.stoneSlabVertical, 2612, 55, Collision.FullCollisionList);
            AddObject(Textures.stoneSlabVertical, 2612, -137);
            AddObject(Textures.stoneCubeLarge, 2676, 27, Collision.TopsCollisionList);
            AddObject(Textures.stoneCubeLarge, 2804, 27, Collision.TopsCollisionList);
            AddObject(Textures.stoneCubeLarge, 2932, 27, Collision.TopsCollisionList);
            AddObject(Textures.stoneCubeLarge, 2676, 27, Collision.TopsCollisionList);
            AddObject(Textures.stoneCubeLarge, 3060, 27, Collision.TopsCollisionList);
            AddObject(Textures.stoneCubeLarge, 3188, 27, Collision.TopsCollisionList);
            AddObject(Textures.stoneCubeLarge, 3316, 27, Collision.TopsCollisionList);
            AddObject(Textures.stoneCubeLarge, 3444, 27, Collision.TopsCollisionList);
            AddObject(Textures.stoneCubeLarge, 3572, 27, Collision.TopsCollisionList);
            AddObject(Textures.stoneCubeLarge, 3700, 27, Collision.TopsCollisionList);
            //cave rock thing floor
            AddObject(Textures.stoneSlabHorizontal, 2236, 900, Collision.FullCollisionList);
            AddObject(Textures.stoneCubeLarge, 2300, 964, Collision.SidesCollisionList);
            AddObject(Textures.stoneCubeLarge, 2428, 900, Collision.FullCollisionList);
            AddObject(Textures.stoneCubeLarge, 2428, 1028);
            AddObject(Textures.stoneCubeLarge, 2556, 964, Collision.FullCollisionList);
            AddObject(Textures.stoneSlabHorizontal, 2684, 1012, Collision.FullCollisionList);
            //cave spikes
            AddObject(Textures.spike, 2876, 1030, Collision.DeathCollisionList);
            AddObject(Textures.spike, 2906, 1030, Collision.DeathCollisionList);
            AddObject(Textures.spike, 2936, 1030, Collision.DeathCollisionList);
            AddObject(Textures.spike, 2966, 1030, Collision.DeathCollisionList);
            AddObject(Textures.spike, 2996, 1030, Collision.DeathCollisionList);
            AddObject(Textures.spike, 3026, 1030, Collision.DeathCollisionList);
            AddObject(Textures.spike, 3056, 1030, Collision.DeathCollisionList);
            AddObject(Textures.spike, 3086, 1030, Collision.DeathCollisionList);
            AddObject(Textures.spike, 3116, 1030, Collision.DeathCollisionList);
            AddObject(Textures.spike, 3146, 1030, Collision.DeathCollisionList);
            AddObject(Textures.spike, 3176, 1030, Collision.DeathCollisionList);
            AddObject(Textures.spike, 3206, 1030, Collision.DeathCollisionList);
            AddObject(Textures.spike, 3236, 1030, Collision.DeathCollisionList);
            AddObject(Textures.spike, 3266, 1030, Collision.DeathCollisionList);
            AddObject(Textures.spike, 3296, 1030, Collision.DeathCollisionList);
            AddObject(Textures.spike, 3326, 1030, Collision.DeathCollisionList);
            AddObject(Textures.spike, 3356, 1030, Collision.DeathCollisionList);
            AddObject(Textures.spike, 3386, 1030, Collision.DeathCollisionList);
            AddObject(Textures.spike, 3416, 1030, Collision.DeathCollisionList);
            AddObject(Textures.stoneSlabHorizontal, 2875, 1060);
            AddObject(Textures.stoneSlabHorizontal, 3067, 1060);
            AddObject(Textures.stoneSlabHorizontal, 3259, 1060);
            AddEnemyHead(2900, 450, 400, 900, true);
            AddEnemyHead(3100, 800, 400, 900, true);
            AddEnemyHead(3300, 500, 400, 900, true);
            //post spikes
            AddObject(Textures.stoneCubeLarge, 3450, 980, Collision.FullCollisionList);
            AddObject(Textures.stoneCubeLarge, 3578, 980, Collision.FullCollisionList);
            AddObject(Textures.stoneSlabVertical, 3706, 920, Collision.FullCollisionList);
            AddObject(Textures.stoneCubeLarge, 3770, 920, Collision.FullCollisionList);
            AddObject(Textures.grassLeft, 4020, 1000, Collision.TopsCollisionList);
            AddObject(Textures.stoneCubeLarge, 3898, 920, Collision.FullCollisionList);
            AddObject(Textures.stoneCubeLarge, 3770, 1048);
            AddObject(Textures.stoneCubeLarge, 3898, 1048);
            //boss arena
            AddObject(Textures.grassMiddle, 4184, 1000, Collision.TopsCollisionList);
            AddObject(Textures.grassMiddle, 4312, 1000, Collision.TopsCollisionList);
            AddObject(Textures.grassMiddle, 4440, 1000, Collision.TopsCollisionList);
            AddObject(Textures.grassMiddle, 4568, 1000, Collision.TopsCollisionList);
            AddObject(Textures.grassMiddle, 4696, 1000, Collision.TopsCollisionList);
            AddObject(Textures.grassMiddle, 4184, 1000, Collision.TopsCollisionList);
            AddObject(Textures.grassMiddle, 4312, 1000, Collision.TopsCollisionList);
            AddObject(Textures.grassMiddle, 4440, 1000, Collision.TopsCollisionList);
            AddObject(Textures.grassMiddle, 4568, 1000, Collision.TopsCollisionList);
            AddObject(Textures.grassMiddle, 4696, 1000, Collision.TopsCollisionList);
            AddObject(Textures.grassMiddle, 4824, 1000, Collision.TopsCollisionList);
            AddObject(Textures.grassMiddle, 4952, 1000, Collision.TopsCollisionList);
            AddObject(Textures.grassMiddle, 5080, 1000, Collision.TopsCollisionList);
            AddObject(Textures.grassMiddle, 5208, 1000, Collision.TopsCollisionList);
            AddObject(Textures.grassMiddle, 5336, 1000, Collision.TopsCollisionList);
            AddObject(Textures.grassMiddle, 5464, 1000, Collision.TopsCollisionList);
            AddObject(Textures.grassMiddle, 5592, 1000, Collision.TopsCollisionList);
            AddObject(Textures.grassMiddle, 5720, 1000, Collision.TopsCollisionList);
            AddObject(Textures.grassRight, 5848, 1000, Collision.TopsCollisionList);

            SetCheckPoint(3800, 848);
            AddGameObject(endFlag);
            Collision.EndFlagCollisionList.Add(endFlag);
            AddGameObject(player);
        }
    }
}
