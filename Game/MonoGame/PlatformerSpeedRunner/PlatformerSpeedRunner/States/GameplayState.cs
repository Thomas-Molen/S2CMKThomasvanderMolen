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

namespace PlatformerSpeedRunner.States
{
    public class GameplayState : BaseGameState
    {
        private AnimationHelper animationHelper = new AnimationHelper();

        private int TimeCharged;
        //TODO THIS IS THE REAL SPAWNPOINT
        //private Vector2 spawnPoint = new Vector2(200, 745);
        private Vector2 spawnPoint = new Vector2(3900, 800);

        //textures
        private const string player = "Player\\Idle\\IdlePinkMan";
        private const string backgroundTexture = "Backgrounds\\PinkWallpaper";

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

        private List<ObjectSprite> TopsCollisionList = new List<ObjectSprite>();
        private List<ObjectSprite> SidesCollisionList = new List<ObjectSprite>();
        private List<ObjectSprite> FullCollisionList = new List<ObjectSprite>();
        private List<ObjectSprite> DeathCollisionList = new List<ObjectSprite>();
        private List<RockHeadSprite> RockHeadCollisionList = new List<RockHeadSprite>();
        private List<SpikeHeadSprite> SpikeHeadCollisionList = new List<SpikeHeadSprite>();
        private List<CheckPoint> CheckPointCollisionList = new List<CheckPoint>();

        public override void LoadContent()
        {
            playerSprite = new PlayerSprite(LoadTexture(player));
            playerSprite.Position = spawnPoint;
            chargeCircleSprite = new ChargeCircleSprite(LoadTexture("Player\\Charging\\ChargingCircle1"));

            splashImage = new SplashImage(LoadTexture(backgroundTexture));

            AddGameObject(splashImage);

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
            AddObject(stoneCubeLarge, 3898, 920, FullCollisionList);
            AddObject(stoneCubeLarge, 3770, 1048);
            AddObject(stoneCubeLarge, 3898, 1048, SidesCollisionList);

            AddCheckPoint(3800, 848);
            AddGameObject(playerSprite);
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

                    chargeCircleSprite.Position = new Vector2(playerSprite.Position.X, playerSprite.Position.Y - 10);

                    if (TimeCharged < 10)
                    {
                        chargeCircleSprite.ChangeTexture(LoadTexture("Player\\Charging\\ChargingCircle1"));
                    }
                    else if (TimeCharged < 20)
                    {
                        chargeCircleSprite.ChangeTexture(LoadTexture("Player\\Charging\\ChargingCircle2"));
                    }
                    else if (TimeCharged < 30)
                    {
                        chargeCircleSprite.ChangeTexture(LoadTexture("Player\\Charging\\ChargingCircle3"));
                    }
                    else if (TimeCharged >= 30)
                    {
                        chargeCircleSprite.ChangeTexture(LoadTexture("Player\\Charging\\ChargingCircle4"));
                    }
                }
                if (cmd is GameplayInputCommand.PlayerLMBRelease)
                {
                    chargeCircleSprite.Position = new Vector2(chargeCircleSprite.Position.X, chargeCircleSprite.Position.Y + 2000);
                    RemoveGameObject(chargeCircleSprite);
                    if (TimeCharged >= 30)
                    {
                        playerSprite.Grapple(30);
                    }
                    else if (TimeCharged < 10)
                    { }
                    else
                    {
                        playerSprite.Grapple(TimeCharged);
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
            playerSprite.PlayerPhysics();
            playerSprite.PlayerAnimation(LoadTexture(animationHelper.RunAnimation(playerSprite.GetAnimationState())));

            foreach (RockHeadSprite rockHead in RockHeadCollisionList)
            {
                rockHead.Movement();
            }
            foreach (SpikeHeadSprite spikeHead in SpikeHeadCollisionList)
            {
                spikeHead.Movement();
            }

            //MouseState mouseState = Mouse.GetState();
            //debugText = Convert.ToInt32(mouseState.X) + "," + Convert.ToInt32(mouseState.Y);
            debugText = playerSprite.animationState.ToString();

            KeepPlayerInBounds();
            DetectCollisions();
        }

        private void DetectCollisions()
        {
            var playerFullDetector = new CollisionDetector<ObjectSprite, PlayerSprite>(FullCollisionList);

            playerFullDetector.DetectCollisions(playerSprite, (Object, player) =>
            {
                if (Convert.ToInt32(player.Position.Y + player.Height) <= Object.Position.Y + 20 &&
                    Convert.ToInt32(player.Position.X + player.Width) > Object.Position.X &&
                    Convert.ToInt32(player.Position.X) < Object.Position.X + Object.Width)
                {
                    player.Position = new Vector2(player.Position.X, Object.Position.Y - player.Height);
                    player.yVelocity = 0;
                }
                else if (Convert.ToInt32(player.Position.Y) >= Object.Position.Y + Object.Height - 20 &&
                        Convert.ToInt32(player.Position.X) <= Object.Position.X + Object.Width &&
                        Convert.ToInt32(player.Position.X + player.Width) >= Object.Position.X)
                {
                    player.Position = new Vector2(player.Position.X, Object.Position.Y + Object.Height);
                    if (player.yVelocity < 0)
                    {
                        player.yVelocity = 0.1f;
                    }
                }
                else if (Convert.ToInt32(player.Position.X) >= Object.Position.X + 20 &&
                        Convert.ToInt32(player.Position.Y + player.Height) > Object.Position.Y &&
                        Convert.ToInt32(player.Position.Y) < Object.Position.Y + Object.Height)
                {
                    player.Position = new Vector2(Object.Position.X + Object.Width, player.Position.Y);
                    if (player.xVelocity < 0)
                    {
                        player.xVelocity = 0;
                    }
                }
                else if (Convert.ToInt32(player.Position.X + player.Width) < Object.Position.X + 20 &&
                        Convert.ToInt32(player.Position.Y + player.Height) > Object.Position.Y &&
                        Convert.ToInt32(player.Position.Y) < Object.Position.Y + Object.Height)
                {
                    player.Position = new Vector2(Object.Position.X - player.Width, player.Position.Y);
                    if (player.xVelocity > 0)
                    {
                        player.xVelocity = 0;
                    }
                }
            });

            var playerTopsDetector = new CollisionDetector<ObjectSprite, PlayerSprite>(TopsCollisionList);

            playerTopsDetector.DetectCollisions(playerSprite, (Object, player) =>
            {
                if (Convert.ToInt32(player.Position.Y + player.Height) <= Object.Position.Y + 20 &&
                    Convert.ToInt32(player.Position.X + player.Width) > Object.Position.X &&
                    Convert.ToInt32(player.Position.X) < Object.Position.X + Object.Width)
                {
                    player.Position = new Vector2(player.Position.X, Object.Position.Y - player.Height);
                    player.yVelocity = 0;
                }
                else if (Convert.ToInt32(player.Position.Y) >= Object.Position.Y + Object.Height - 20 &&
                        Convert.ToInt32(player.Position.X) <= Object.Position.X + Object.Width &&
                        Convert.ToInt32(player.Position.X + player.Width) >= Object.Position.X)
                {
                    player.Position = new Vector2(player.Position.X, Object.Position.Y + Object.Height);
                    if (player.yVelocity < 0)
                    {
                        player.yVelocity = 0.1f;
                    }
                }
            });

            var playerSidesDetector = new CollisionDetector<ObjectSprite, PlayerSprite>(SidesCollisionList);

            playerSidesDetector.DetectCollisions(playerSprite, (Object, player) =>
            {
                if (Convert.ToInt32(player.Position.X) >= Object.Position.X + 20 &&
                        Convert.ToInt32(player.Position.Y + player.Height) > Object.Position.Y &&
                        Convert.ToInt32(player.Position.Y) < Object.Position.Y + Object.Height)
                {
                    player.Position = new Vector2(Object.Position.X + Object.Width, player.Position.Y);
                    if (player.xVelocity < 0)
                    {
                        player.xVelocity = 0;
                    }
                }
                else if (Convert.ToInt32(player.Position.X + player.Width) < Object.Position.X + 20 &&
                        Convert.ToInt32(player.Position.Y + player.Height) > Object.Position.Y &&
                        Convert.ToInt32(player.Position.Y) < Object.Position.Y + Object.Height)
                {
                    player.Position = new Vector2(Object.Position.X - player.Width, player.Position.Y);
                    if (player.xVelocity > 0)
                    {
                        player.xVelocity = 0;
                    }
                }
            });

            var playerDeathDetector = new CollisionDetector<ObjectSprite, PlayerSprite>(DeathCollisionList);

            playerDeathDetector.DetectCollisions(playerSprite, (Object, player) =>
            {
                RespawnPlayer();
            });

            var playerSpikeHeadDetector = new CollisionDetector<SpikeHeadSprite, PlayerSprite>(SpikeHeadCollisionList);

            playerSpikeHeadDetector.DetectCollisions(playerSprite, (Object, player) =>
            {
                RespawnPlayer();
            });

            var playerRockHeadDetector = new CollisionDetector<RockHeadSprite, PlayerSprite>(RockHeadCollisionList);

            playerRockHeadDetector.DetectCollisions(playerSprite, (Object, player) =>
            {
                if (Convert.ToInt32(player.Position.Y + player.Height) <= Object.Position.Y + 20 &&
                    Convert.ToInt32(player.Position.X + player.Width) > Object.Position.X &&
                    Convert.ToInt32(player.Position.X) < Object.Position.X + Object.Width)
                {
                    Object.ChangeTexture(LoadTexture("Enemies\\RockHeadMad"));

                    player.Position = new Vector2(player.Position.X + Object.xVelocity, Object.Position.Y - player.Height);
                    player.yVelocity = 0;
                }
                else if (Convert.ToInt32(player.Position.Y) >= Object.Position.Y + Object.Height - 20 &&
                        Convert.ToInt32(player.Position.X) <= Object.Position.X + Object.Width &&
                        Convert.ToInt32(player.Position.X + player.Width) >= Object.Position.X)
                {
                    player.Position = new Vector2(player.Position.X, Object.Position.Y + Object.Height);
                    if (player.yVelocity < 0)
                    {
                        player.yVelocity = 0.1f;
                    }
                }
                else if (Convert.ToInt32(player.Position.X) >= Object.Position.X + 20 &&
                        Convert.ToInt32(player.Position.Y + player.Height) > Object.Position.Y &&
                        Convert.ToInt32(player.Position.Y) < Object.Position.Y + Object.Height)
                {
                    player.Position = new Vector2(Object.Position.X + Object.Width, player.Position.Y);
                    if (player.xVelocity < 0)
                    {
                        player.xVelocity = 0;
                    }
                }
                else if (Convert.ToInt32(player.Position.X + player.Width) < Object.Position.X + 20 &&
                        Convert.ToInt32(player.Position.Y + player.Height) > Object.Position.Y &&
                        Convert.ToInt32(player.Position.Y) < Object.Position.Y + Object.Height)
                {
                    player.Position = new Vector2(Object.Position.X - player.Width, player.Position.Y);
                    if (player.xVelocity > 0)
                    {
                        player.xVelocity = 0;
                    }
                }
            });

            var playerCheckPointDetector = new CollisionDetector<CheckPoint, PlayerSprite>(CheckPointCollisionList);

            playerCheckPointDetector.DetectCollisions(playerSprite, (Object, player) =>
            {
                if (Object.activated == false)
                {
                    Object.ChangeTexture(LoadTexture("Terrain\\CheckPointActivated"));
                    spawnPoint = new Vector2(Object.Position.X + 45, Object.Position.Y);
                    Object.activated = true;
                }
            });
        }

        //creating objects in world
        private void AddObject(string TextureName, int PosX, int PosY, List<ObjectSprite> CollisionList)
        {
            ObjectSprite Object = new ObjectSprite(LoadTexture(TextureName), true);
            CollisionList.Add(Object);
            AddGameObject(Object);
            Object.Position = new Vector2(PosX, PosY);
        }

        private void AddObject(string TextureName, int PosX, int PosY, int BoundingBoxWidth, int BoundingBoxHeight, int BoundingBoxOffSetX, int BoundingBoxOffSetY, List<ObjectSprite> CollisionList)
        {
            ObjectSprite Object = new ObjectSprite(LoadTexture(TextureName), BoundingBoxWidth, BoundingBoxHeight, BoundingBoxOffSetX, BoundingBoxOffSetY);
            CollisionList.Add(Object);
            AddGameObject(Object);
            Object.Position = new Vector2(PosX, PosY);
        }

        private void AddObject(string TextureName, int PosX, int PosY)
        {
            ObjectSprite Object = new ObjectSprite(LoadTexture(TextureName), false);
            AddGameObject(Object);
            Object.Position = new Vector2(PosX, PosY);
        }

        private void AddRockHead(int PosX, int PosY, int MinPosX, int MaxPosX)
        {
            RockHeadSprite rockHead = new RockHeadSprite(LoadTexture("Enemies\\RockHeadIdle"), MinPosX, MaxPosX);
            RockHeadCollisionList.Add(rockHead);
            AddGameObject(rockHead);
            rockHead.Position = new Vector2(PosX, PosY);
        }

        private void AddSpikeHead(int PosX, int PosY, int MinPosY, int MaxPosY)
        {
            SpikeHeadSprite spikeHead = new SpikeHeadSprite(LoadTexture("Enemies\\SpikeHead"), MinPosY, MaxPosY);
            SpikeHeadCollisionList.Add(spikeHead);
            AddGameObject(spikeHead);
            spikeHead.Position = new Vector2(PosX, PosY);
        }

        private void AddCheckPoint(int PosX, int PosY)
        {
            CheckPoint checkPoint = new CheckPoint(LoadTexture(checkPointTexture));
            CheckPointCollisionList.Add(checkPoint);
            AddGameObject(checkPoint);
            checkPoint.Position = new Vector2(PosX, PosY);
        }

        private void KeepPlayerInBounds()
        {
            if (playerSprite.Position.X - playerSprite.Width / 2 < 0)
            {
                playerSprite.Position = new Vector2(0 + playerSprite.Width / 2, playerSprite.Position.Y);
            }

            if (playerSprite.Position.Y < 0)
            {
                playerSprite.Position = new Vector2(playerSprite.Position.X, 0);
                playerSprite.yVelocity = playerSprite.yVelocity/2;
            }

            if (playerSprite.Position.Y > Program.height)
            {
                RespawnPlayer();
            }
        }

        private void RespawnPlayer()
        {
            playerSprite.xVelocity = 0;
            playerSprite.yVelocity = 0;
            playerSprite.Position = spawnPoint;
        }

        protected override void SetInputManager()
        {
            InputManager = new InputManager(new GameplayInputMapper());
        }
    }
}
