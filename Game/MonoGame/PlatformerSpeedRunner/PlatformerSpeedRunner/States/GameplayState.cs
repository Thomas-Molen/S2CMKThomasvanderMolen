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
        private Vector2 spawnPoint = new Vector2(200, 745);

        //textures
        private const string player = "IdlePinkMan";
        private const string backgroundTexture = "PinkWallpaper";

        private const string woodenBoxLarge = "WoodenBoxLarge";
        private const string grassLeft = "GrassLeft";
        private const string grassRight = "GrassRight";
        private const string grassMiddle = "GrassMiddle";
        private const string grassSoilLeft = "GrassSoilLeft";
        private const string grassSoilRight = "GrassSoilRight";
        private const string grassSoilMiddle = "GrassSoilMiddle";
        private const string spike = "Spike";
        private const string stoneCubeLarge = "StoneCubeLarge";

        private List<ObjectSprite> TopsCollisionList = new List<ObjectSprite>();
        private List<ObjectSprite> SidesCollisionList = new List<ObjectSprite>();
        private List<ObjectSprite> FullCollisionList = new List<ObjectSprite>();
        private List<ObjectSprite> DeathCollisionList = new List<ObjectSprite>();
        private List<RockHeadSprite> RockheadCollisionList = new List<RockHeadSprite>();

        public override void LoadContent()
        {
            playerSprite = new PlayerSprite(LoadTexture(player));
            playerSprite.Position = spawnPoint;

            splashImage = new SplashImage(LoadTexture(backgroundTexture));

            AddGameObject(splashImage);

            //First ground grass
            AddObject(woodenBoxLarge, 475, 705, 100, 100, FullCollisionList);
            AddObject(grassLeft, 0, 803, 164, 184, TopsCollisionList);
            AddObject(grassMiddle, 164, 803, 128, 184, TopsCollisionList);
            AddObject(grassMiddle, 292, 803, 128, 184, TopsCollisionList);
            AddObject(grassRight, 420, 803, 140, 184, TopsCollisionList);
            AddObject(grassLeft, 572, 623, 164, 184, FullCollisionList);
            AddObject(grassRight, 736, 623, 156, 184, FullCollisionList);
            //spikes
            AddObject(spike, 889, 861, 35, 35, DeathCollisionList);
            AddObject(spike, 919, 861, 35, 35, DeathCollisionList);
            AddObject(spike, 949, 861, 35, 35, DeathCollisionList);
            AddObject(spike, 979, 861, 35, 35, DeathCollisionList);
            AddObject(spike, 1009, 861, 35, 35, DeathCollisionList);
            AddObject(spike, 1039, 861, 35, 35, DeathCollisionList);
            //Grass under spikes
            AddObject(grassLeft, 880, 892);
            AddObject(grassRight, 1044, 892);
            //First hill soil
            AddObject(grassSoilLeft, 572, 807);
            AddObject(grassSoilLeft, 572, 927);
            AddObject(grassSoilLeft, 572, 1047);
            AddObject(grassSoilRight, 736, 807, 156, 120, SidesCollisionList);
            AddObject(grassSoilRight, 736, 927);
            AddObject(grassSoilRight, 736, 1047);
            //First ground soil
            AddObject(grassSoilLeft, 0, 987);
            AddObject(grassSoilMiddle, 164, 987);
            AddObject(grassSoilMiddle, 292, 987);
            AddObject(grassSoilRight, 420, 987);
            //post spikes hill
            AddObject(grassLeft, 1071, 536, 164, 184, FullCollisionList);
            AddObject(grassSoilLeft, 1071, 720, 164, 120, SidesCollisionList);
            AddObject(grassSoilLeft, 1071, 840);
            AddObject(grassSoilLeft, 1071, 960);
            AddObject(grassRight, 1235, 536, 156, 120, FullCollisionList);
            AddObject(grassSoilRight, 1235, 720, 156, 120, SidesCollisionList);
            AddObject(grassSoilRight, 1235, 840, 156, 120, SidesCollisionList);
            AddObject(grassSoilRight, 1235, 960, 156, 120, SidesCollisionList);
            AddRockHead(1400, 536, 1400, 1800);
            //cave rock thing
            AddObject(stoneCubeLarge, 2100, 27, 128, 128, SidesCollisionList);
            AddObject(stoneCubeLarge, 2100, 155, 128, 128, SidesCollisionList);
            AddObject(stoneCubeLarge, 2100, 283, 128, 128, SidesCollisionList);
            AddObject(stoneCubeLarge, 2100, 411, 128, 128, FullCollisionList);

            AddObject(stoneCubeLarge, 2300, 900, 128, 128, FullCollisionList);
            AddObject(stoneCubeLarge, 2428, 900, 128, 128, TopsCollisionList);
            AddObject(stoneCubeLarge, 2556, 900, 128, 128, TopsCollisionList);
            AddObject(stoneCubeLarge, 2684, 900, 128, 128, FullCollisionList);

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
                    TimeCharged += 1;
                }
                if (cmd is GameplayInputCommand.PlayerLMBRelease)
                {
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

            foreach (RockHeadSprite rockhead in RockheadCollisionList)
            {
                rockhead.Movement();
            }

            MouseState mouseState = Mouse.GetState();
            debugText = Convert.ToInt32(mouseState.X) + "," + Convert.ToInt32(mouseState.Y);

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
                    player.yVelocity = 0.1f;
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
                player.Position = spawnPoint;
            });

            var playerRockHeadDetector = new CollisionDetector<RockHeadSprite, PlayerSprite>(RockheadCollisionList);

            playerRockHeadDetector.DetectCollisions(playerSprite, (Object, player) =>
            {
                if (Convert.ToInt32(player.Position.Y + player.Height) <= Object.Position.Y + 20 &&
                    Convert.ToInt32(player.Position.X + player.Width) > Object.Position.X &&
                    Convert.ToInt32(player.Position.X) < Object.Position.X + Object.Width)
                {
                    Object.ChangeTexture(LoadTexture("RockHeadMad"));

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
        }

        //creating objects in world
        private void AddObject(string TextureName, int PosX, int PosY, int BoundingBoxWidth, int BoundingBoxHeight, List<ObjectSprite> CollisionList)
        {
            ObjectSprite Object = new ObjectSprite(LoadTexture(TextureName), BoundingBoxWidth, BoundingBoxHeight);
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
            ObjectSprite Object = new ObjectSprite(LoadTexture(TextureName));
            AddGameObject(Object);
            Object.Position = new Vector2(PosX, PosY);
        }

        private void AddRockHead(int PosX, int PosY, int MinPosX, int MaxPosX)
        {
            RockHeadSprite rockHead = new RockHeadSprite(LoadTexture("RockHeadIdle"), MinPosX, MaxPosX);
            RockheadCollisionList.Add(rockHead);
            AddGameObject(rockHead);
            rockHead.Position = new Vector2(PosX, PosY);
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
                playerSprite.Position = spawnPoint;
            }
        }

        protected override void SetInputManager()
        {
            InputManager = new InputManager(new GameplayInputMapper());
        }
    }
}
