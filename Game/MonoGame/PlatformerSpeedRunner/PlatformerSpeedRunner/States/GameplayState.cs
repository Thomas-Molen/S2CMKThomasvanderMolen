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
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace PlatformerSpeedRunner.States
{
    public class GameplayState : BaseGameState
    {
        //textures
        private const string player = "IdlePinkMan";
        private const string backgroundTexture = "PinkWallpaper";
        private const string stoneGroundTexture = "StoneGround";

        private PlayerSprite playerSprite;

        private bool gravityState = true;

        private List<StoneGroundSprite> stoneGroundList = new List<StoneGroundSprite>();

        public override void LoadContent()
        {
            playerSprite = new PlayerSprite(LoadTexture(player));

            AddGameObject(new SplashImage(LoadTexture(backgroundTexture)));
            AddGameObject(playerSprite);
            AddStoneGround(0, 653);
            AddStoneGround(200, 653);
            AddStoneGround(400, 653);
            AddStoneGround(600, 653);
            AddStoneGround(800, 653);
            AddStoneGround(1000, 653);

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
                    playerSprite.MoveNone();
                }

                if (cmd is GameplayInputCommand.PlayerLMB)
                {
                    playerSprite.Grapple();
                    gravityState = false;
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
            playerSprite.PlayerPhysics(gravityState);
            gravityState = true;

            KeepPlayerInBounds();
            DetectCollisions();
        }

        private void DetectCollisions()
        {
            var playerGroundDetector = new CollisionDetector<StoneGroundSprite, PlayerSprite>(stoneGroundList);

            playerGroundDetector.DetectCollisions(playerSprite, (stoneGround, player) =>
            {
                gravityState = false;
                playerSprite.YVelocityNone();
            });
        }

        private void AddStoneGround(int posX, int posY)
        {
            StoneGroundSprite stoneGround = new StoneGroundSprite(LoadTexture(stoneGroundTexture));
            stoneGroundList.Add(stoneGround);
            AddGameObject(stoneGround);
            stoneGround.Position = new Vector2(posX, posY);
        }

        //TODO BROKEN
        private void KeepPlayerInBounds()
        {
            if (playerSprite.Position.X < 0)
            {
                playerSprite.Position = new Vector2(0, playerSprite.Position.Y);
                playerSprite.YVelocityNone();
                playerSprite.XVelocityNone();
            }

            if (playerSprite.Position.X > baseViewportWidth - playerSprite.Width)
            {
                playerSprite.Position = new Vector2(baseViewportWidth - playerSprite.Width, playerSprite.Position.Y);
                playerSprite.YVelocityNone();
                playerSprite.XVelocityNone();
            }

            if (playerSprite.Position.Y < 0)
            {
                playerSprite.Position = new Vector2(playerSprite.Position.X, 0);
                playerSprite.YVelocityNone();
                playerSprite.XVelocityNone();
            }
        }

        protected override void SetInputManager()
        {
            InputManager = new InputManager(new GameplayInputMapper());
        }
    }
}
