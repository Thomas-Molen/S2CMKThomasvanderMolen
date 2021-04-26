using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using PlatformerSpeedRunner.Objects;
using PlatformerSpeedRunner.States.Base;
using PlatformerSpeedRunner.Input;
using PlatformerSpeedRunner.Input.Base;
using System;
using PlatformerSpeedRunner.Enum;

namespace PlatformerSpeedRunner.States
{
    public class MenuState : BaseGameState
    {
        public override void LoadContent()
        {
            currentState = GameState.MainMenu;

            backgroundImage = new BasicObject(LoadTexture("Backgrounds\\PinkWallpaper"), new Vector2(0, 0));
            AddGameObject(backgroundImage);
            LoadObjects();
        }

        public override void HandleInput()
        {
            InputManager.GetCommands(cmd =>
            {
                if (cmd is SplashInputCommand.GameSelect)
                {
                    SwitchState(new GameplayState());
                }
                if (cmd is SplashInputCommand.GameExit)
                {
                    Environment.Exit(1);
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

        private void LoadObjects()
        {
            AddText("MAIN MENU", 900, 100);
        }
    }
}
