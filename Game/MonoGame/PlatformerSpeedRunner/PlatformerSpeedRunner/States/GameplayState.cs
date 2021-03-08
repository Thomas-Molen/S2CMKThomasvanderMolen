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

namespace PlatformerSpeedRunner.States
{
    public class GameplayState : BaseGameState
    {
        private int TimeCharged;

        //textures
        private const string player = "IdlePinkMan";
        private const string backgroundTexture = "PinkWallpaper";
        private const string stoneGroundTexture = "StoneGround";

        private List<StoneGroundSprite> stoneGroundList = new List<StoneGroundSprite>();

        public override void LoadContent()
        {
            playerSprite = new PlayerSprite(LoadTexture(player));
            splashImage = new SplashImage(LoadTexture(backgroundTexture));

            AddGameObject(splashImage);
            AddStoneGround(0, 653);
            AddStoneGround(200, 653);
            AddStoneGround(400, 653);
            AddStoneGround(600, 653);
            AddStoneGround(800, 653);
            AddStoneGround(1000, 950);
            AddStoneGround(1200, 950);
            AddGameObject(playerSprite);

            //spawnposition of player
            var playerX = baseViewportWidth / 2 - playerSprite.Width / 2;
            var playerY = baseViewportHeight - playerSprite.Height - 600;
            playerSprite.Position = new Vector2(playerX, playerY);
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
            });
        }

        public override void UpdateGameState(GameTime gameTime)
        {
            playerSprite.PlayerPhysics();

            debugText = playerSprite.Position.ToString();

            KeepPlayerInBounds();
            DetectCollisions();
        }

        private void DetectCollisions()
        {
            var playerGroundDetector = new CollisionDetector<StoneGroundSprite, PlayerSprite>(stoneGroundList);

            playerGroundDetector.DetectCollisions(playerSprite, (stoneGround, player) =>
            {
                playerSprite.Position = new Vector2(playerSprite.Position.X, stoneGround.GetHeight() - playerSprite.Height);
                playerSprite.yVelocity = 0;
            });
        }

        private void AddStoneGround(int posX, int posY)
        {
            StoneGroundSprite stoneGround = new StoneGroundSprite(LoadTexture(stoneGroundTexture));
            stoneGroundList.Add(stoneGround);
            AddGameObject(stoneGround);
            stoneGround.Position = new Vector2(posX, posY);
        }

        private void KeepPlayerInBounds()
        {
            if (playerSprite.Position.X - playerSprite.Width / 2 < 0)
            {
                playerSprite.Position = new Vector2(0 + playerSprite.Width / 2, playerSprite.Position.Y);
                playerSprite.yVelocity = 0;
                playerSprite.xVelocity = playerSprite.xVelocity/2;
            }

            if (playerSprite.Position.Y < 0)
            {
                playerSprite.Position = new Vector2(playerSprite.Position.X, 0);
                playerSprite.yVelocity = playerSprite.yVelocity/2;
            }
        }

        protected override void SetInputManager()
        {
            InputManager = new InputManager(new GameplayInputMapper());
        }
    }
}
