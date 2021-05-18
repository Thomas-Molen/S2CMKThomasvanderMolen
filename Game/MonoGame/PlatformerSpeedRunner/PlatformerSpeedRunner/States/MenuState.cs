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
                if (cmd is SplashInputCommand.PlayerLMB)
                {

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
            Text textObject = new Text(content, new Vector2(PosX, PosY), Fonts.CalibriBold50);
            AddTextObject(textObject);
            return textObject;
        }

        private void AddButton(int PosX, int PosY, string TextureName)
        {
            BasicObject buttonObject = new BasicObject(LoadTexture(TextureName), new Vector2(PosX, PosY), true);
            AddGameObject(buttonObject);
        }

        private void LoadObjects()
        {
            AddText("MAIN MENU", 795, 100);
            AddText("Start Game", 848, 315);
            AddText("Leaderboard", 825, 515);
            AddText("Register Account", 760, 715);

            AddButton(745, 300, "Menu\\EmptyButton");
            AddButton(745, 500, "Menu\\EmptyButton");
            AddButton(745, 700, "Menu\\EmptyButton");

            AddButton(645, 300, "Menu\\PlayButton");
            AddButton(645, 500, "Menu\\LeaderboardButton");
            AddButton(645, 700, "Menu\\CreateButton");
        }
    }
}
