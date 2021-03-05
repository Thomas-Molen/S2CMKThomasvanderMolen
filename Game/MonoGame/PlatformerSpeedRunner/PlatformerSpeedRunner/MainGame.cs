using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MySql.Data;
using MySql.Data.MySqlClient;

using PlatformerSpeedRunner.Enum;
using PlatformerSpeedRunner.Objects;
using PlatformerSpeedRunner.States;
using PlatformerSpeedRunner.States.Base;
using System.Data;

namespace PlatformerSpeedRunner
{
    public class MainGame : Game
    {
        //debug
        private Vector2 debugTextPosition = new Vector2(20, 60);
        private Vector2 debugMousePosition = new Vector2(20, 80);

        private BaseGameState currentGameState;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private SpriteFont font;

        //Database Temporary
        MySqlConnection sqlConnection;
        MySqlDataAdapter sqlDataAdapter;
        DataSet dataSet;

        private string playerName;
        private Vector2 playerNamePosition = new Vector2(20, 20);
        private string playerScore;
        private Vector2 playerScorePosition = new Vector2(20, 40);

        //set the proper window scaling
        private RenderTarget2D renderTarget;
        private Rectangle renderScaleRectangle;

        private const int designedResolutionWidth = 1080;
        private const int designedResolutionHeight = 720;

        private const float designedResolutionAspectRatio = designedResolutionWidth / (float)designedResolutionHeight;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //set rendertarget
            graphics.PreferredBackBufferWidth = designedResolutionWidth;
            graphics.PreferredBackBufferHeight = designedResolutionHeight;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

            renderTarget = new RenderTarget2D(graphics.GraphicsDevice, designedResolutionWidth, designedResolutionHeight, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);
            renderScaleRectangle = GetScaleRectangle();

            font = Content.Load<SpriteFont>("GuiFont");

            playerName = GetData("SELECT user_name FROM `users` WHERE user_number = 1", "user_name");
            playerScore = GetData("SELECT score FROM `users` WHERE user_number = 1", "score");

            base.Initialize();
        }

        //add black bars where neccesary
        private Rectangle GetScaleRectangle()
        {
            var variance = 0.5;
            var actualAspectRatio = Window.ClientBounds.Width / (float)Window.ClientBounds.Height;
            Rectangle scaleRectangle;
            
            if (actualAspectRatio <= designedResolutionAspectRatio)
            {
                var presentHeight = (int)(Window.ClientBounds.Width / designedResolutionAspectRatio + variance);
                var barHeight = (Window.ClientBounds.Height - presentHeight) / 2;

                scaleRectangle = new Rectangle(0, barHeight, Window.ClientBounds.Width, presentHeight);
            }
            else
            {
                var presentWidth = (int)(Window.ClientBounds.Height * designedResolutionAspectRatio + variance);
                var barWidth = (Window.ClientBounds.Width - presentWidth) / 2;

                scaleRectangle = new Rectangle(barWidth, 0, presentWidth, Window.ClientBounds.Height);
            }
            return scaleRectangle;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //TODO CHANGE THIS TO THE BOOT SCREEN
            SwitchGameState(new GameplayState());
            // TODO: use this.Content to load your game content here
        }

        //switch state logic
        private void CurrentGameState_OnStateSwitched(object sender, BaseGameState e)
        {
            SwitchGameState(e);
        }

        private void currentGameState_OnEventNotification(object sender, Enum.Events e)
        {
            switch (e)
            {
                case Events.GAME_QUIT:
                    Exit();
                    break;
            }
        }

        private void SwitchGameState(BaseGameState gameState)
        {
            if (currentGameState != null)
            {
                currentGameState.OnStateSwitched -= CurrentGameState_OnStateSwitched;
                currentGameState.OnEventNotification -= currentGameState_OnEventNotification;
                currentGameState.UnloadContent();
            }

            currentGameState = gameState;

            currentGameState.Initialize(Content, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);

            currentGameState.LoadContent();

            currentGameState.OnStateSwitched += CurrentGameState_OnStateSwitched;
            currentGameState.OnEventNotification += currentGameState_OnEventNotification;
        }

        protected override void UnloadContent()
        {
            currentGameState?.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            currentGameState.HandleInput();

            // TODO: Add your update logic here


            currentGameState.Update(gameTime);
            base.Update(gameTime);
        }

        //Get database name value
        private string GetData(string sqlString, string selectName)
        {
            sqlConnection = new MySqlConnection("server=studmysql01.fhict.local;Uid=dbi456905;Database=dbi456905;Pwd=7pjWWwn2M");
            sqlDataAdapter = new MySqlDataAdapter(sqlString, sqlConnection);
            dataSet = new DataSet();

            sqlDataAdapter.Fill(dataSet, "depth");

            return dataSet.Tables[0].Rows[0][selectName].ToString();
        }

        protected override void Draw(GameTime gameTime)
        {
            //renders to the target
            GraphicsDevice.SetRenderTarget(renderTarget);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            currentGameState.Render(spriteBatch);

            spriteBatch.DrawString(font, "player: " + playerName, playerNamePosition, Color.Black);
            spriteBatch.DrawString(font, "score: " + playerScore, playerScorePosition, Color.Black);
            MouseState mouseState = Mouse.GetState();
            spriteBatch.DrawString(font, "mouse: " + mouseState.X + " , " + mouseState.Y, debugMousePosition, Color.Black);
            spriteBatch.DrawString(font, "debug: " + currentGameState.debugText, debugTextPosition, Color.Black);

            spriteBatch.End();

            //render scaled content
            graphics.GraphicsDevice.SetRenderTarget(null);

            graphics.GraphicsDevice.Clear(ClearOptions.Target, Color.Black, 1.0f, 0);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);

            spriteBatch.Draw(renderTarget, renderScaleRectangle, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
