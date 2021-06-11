using Microsoft.Xna.Framework;
using PlatformerSpeedRunner.Enum;
using PlatformerSpeedRunner.Helper;
using PlatformerSpeedRunner.Input;
using PlatformerSpeedRunner.Input.Base;
using PlatformerSpeedRunner.Objects;
using PlatformerSpeedRunner.States.Base;
using System;
using System.Diagnostics;

namespace PlatformerSpeedRunner.States
{
    public class MenuState : BaseGameState
    {
        private ButtonHelper buttonHelper;
        private SaveDataHelper dataHelper;
        private DatabaseHelper dataBaseHelper;

        private BasicObject playButton;
        private BasicObject playIcon;
        private BasicObject leaderboardButton;
        private BasicObject leaderboardIcon;
        private BasicObject createButton;
        private BasicObject createIcon;
        private BasicObject accountButton;
        private BasicObject accountIcon;

        private bool LMBPressed = false;
        private string username;

        public override void LoadContent()
        {
            currentState = GameState.MainMenu;
            buttonHelper = new ButtonHelper();
            dataHelper = new SaveDataHelper();
            dataBaseHelper = new DatabaseHelper();
            dataBaseHelper.GetUsername();
            username = dataBaseHelper.GetUsername();

            backgroundImage = new BasicObject(baseContentManager, "Backgrounds\\PinkWallpaper", new Vector2(0, 0));
            AddGameObject(backgroundImage);
            LoadWorld();
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
                if (cmd is SplashInputCommand.PlayerLMBPress)
                {
                    LMBPressed = true;
                    
                }
                if (cmd is SplashInputCommand.PlayerLMBRelease && LMBPressed)
                {
                    CheckButtonPress();
                    LMBPressed = false;
                }
            });
        }

        public override void UpdateGameState(GameTime gameTime) 
        {
            camera.Follow();
        }

        private void CheckButtonPress()
        {
            if (buttonHelper.IsMouseOnButton(playButton, camera) || buttonHelper.IsMouseOnButton(playIcon, camera))
            {
                SwitchState(new GameplayState());
            }
            if (username != "Could not connect to server")
            {
                if (buttonHelper.IsMouseOnButton(leaderboardButton, camera) || buttonHelper.IsMouseOnButton(leaderboardIcon, camera))
                {
                    OpenUrl("http://platformerspeedrunner/leaderboard");
                }
                if (username == "no user found with such key")
                {
                    if (buttonHelper.IsMouseOnButton(createButton, camera) || buttonHelper.IsMouseOnButton(createIcon, camera))
                    {
                        OpenUrl("http://platformerspeedrunner/register/" + dataHelper.GetSaveData());
                    }
                }
                else
                {
                    if (buttonHelper.IsMouseOnButton(accountButton, camera) || buttonHelper.IsMouseOnButton(accountIcon, camera))
                    {
                        OpenUrl("http://platformerspeedrunner/personal_runs");
                    }
                }
            }
        }

        private void OpenUrl(string url)
        {
            Process myProcess = new Process();
            try
            {
                myProcess.StartInfo.UseShellExecute = true;
                myProcess.StartInfo.FileName = url;
                myProcess.Start();
            }
            catch (Exception)
            { }
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

        private Text AddSmallText(string content, int PosX, int PosY)
        {
            Text textObject = new Text(content, new Vector2(PosX, PosY), Fonts.CalibriBold25);
            AddTextObject(textObject);
            return textObject;
        }

        private BasicObject AddButton(int PosX, int PosY, string TextureName)
        {
            BasicObject buttonObject = new BasicObject(baseContentManager, TextureName, new Vector2(PosX, PosY), true);
            AddGameObject(buttonObject);
            return buttonObject;
        }

        private void LoadWorld()
        {
            AddText("MAIN MENU", 795, 100);
            AddText("Start Game", 848, 315);
            
            playButton = AddButton(745, 300, "Menu\\EmptyButton");
            playIcon = AddButton(645, 300, "Menu\\PlayButton");

            if (username != "Could not connect to server")
            {
                AddText("Leaderboard", 825, 515);
                leaderboardButton = AddButton(745, 500, "Menu\\EmptyButton");
                leaderboardIcon = AddButton(645, 500, "Menu\\LeaderboardButton");

                if (username == "no user found with such key")
                {
                    AddText("Register Account", 760, 715);
                    createButton = AddButton(745, 700, "Menu\\EmptyButton");
                    createIcon = AddButton(645, 700, "Menu\\CreateButton");
                }
                else
                {
                    AddText("Account", 870, 715);
                    accountButton = AddButton(745, 700, "Menu\\EmptyButton");
                    accountIcon = AddButton(645, 700, "Menu\\AccountButton");
                    AddSmallText("Username: " + username, 10, 10);

                    string bestTime = dataBaseHelper.GetBestTime();
                    if (bestTime == "no runs found from user" || bestTime == "Could not connect to server")
                    {
                        AddSmallText("Best Time: " + bestTime, 10, 50);
                    }
                    else
                    {
                        string time = TimeSpan.FromMilliseconds(Convert.ToInt32(bestTime)).ToString();
                        time = time.Substring(0, time.Length - 4);
                        AddSmallText("Best Time: " + time, 10, 50);
                    }
                }
            }
            else
            {
                AddSmallText("Could not connect to servers", 10, 10);
                AddSmallText("Runs will not be saved", 10, 50);
            }
        }
    }
}
