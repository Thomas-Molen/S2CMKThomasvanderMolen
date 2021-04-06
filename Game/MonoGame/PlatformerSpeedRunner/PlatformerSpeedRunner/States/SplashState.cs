using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using PlatformerSpeedRunner.Objects;
using PlatformerSpeedRunner.States.Base;
using PlatformerSpeedRunner.Input;
using PlatformerSpeedRunner.Input.Base;
using System;

namespace PlatformerSpeedRunner.States
{
    public class SplashState : BaseGameState
    {
        public override void LoadContent()
        {
            backgroundImage = new BasicObject(LoadTexture("Backgrounds\\PinkWallpaper"), new Vector2(0, 0));
            AddGameObject(backgroundImage);
        }

        public override void HandleInput()
        {
            InputManager.GetCommands(cmd =>
            {
                if (cmd is SplashInputCommand.GameSelect)
                {
                    SwitchState(new GameplayState());
                }
            });
        }

        public override void UpdateGameState(GameTime gameTime) 
        {
            camera.Follow();
        }

        protected override void SetInputManager()
        {
            InputManager = new InputManager(new SplashInputMapper());
        }

        private Text AddText(string content, int PosX, int PosY)
        {
            Text textObject = new Text(content, new Vector2(PosX, PosY));
            AddTextObject(textObject);
            return textObject;
        }
    }
}
