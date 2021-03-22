using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MySql.Data;
using MySql.Data.MySqlClient;

using PlatformerSpeedRunner.Enum;
using PlatformerSpeedRunner.Objects;
using PlatformerSpeedRunner.States;
using PlatformerSpeedRunner.States.Base;
using PlatformerSpeedRunner.Camera;
using System.Data;

namespace PlatformerSpeedRunner
{
    public class MainGame : Game
    {
        //debug
        private Vector2 debugTextPosition;
        private Vector2 debugPlayerPosition;

        //components
        private BaseGameState currentGameState;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private SpriteFont font;

        private CameraHelper camera;

        //Database
        MySqlConnection sqlConnection;
        MySqlDataAdapter sqlDataAdapter;
        DataSet dataSet;

        private string playerName;
        private Vector2 playerNamePosition;
        private string playerScore;
        private Vector2 playerScorePosition;

        //set the proper window scaling
        private RenderTarget2D renderTarget;
        private Rectangle renderScaleRectangle;

        private int designedResolutionWidth;
        private int designedResolutionHeight;
        private float designedResolutionAspectRatio;

        public MainGame(int width, int height)
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            designedResolutionWidth = width;
            designedResolutionHeight = height;
            designedResolutionAspectRatio = width / (float)height;
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

            font = Content.Load<SpriteFont>("Fonts\\GuiFont");

            playerName = GetData("SELECT user_name FROM `users` WHERE user_number = 1", "user_name");
            playerScore = GetData("SELECT score FROM `users` WHERE user_number = 1", "score");

            camera = new CameraHelper();

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
            SwitchGameState(new SplashState());
        }

        //switch state logic
        private void CurrentGameState_OnStateSwitched(object sender, BaseGameState gameState)
        {
            SwitchGameState(gameState);
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

            currentGameState.Update(gameTime);

            camera.Follow(currentGameState.playerSprite);
            UpdateCameraBasedPositions();

            base.Update(gameTime);
        }

        //Get database name value
        private string GetData(string sqlString, string selectName)
        {
            sqlConnection = new MySqlConnection("server=studmysql01.fhict.local;Uid=dbi456905;Database=dbi456905;Pwd=7pjWWwn2M");
            sqlDataAdapter = new MySqlDataAdapter(sqlString, sqlConnection);
            dataSet = new DataSet();

            try
            {
                sqlDataAdapter.Fill(dataSet, "depth");
            }
            catch (System.Exception)
            {
                return "no database information could be recovered"; //when you probably don't have a connection to database
            }
            return dataSet.Tables[0].Rows[0][selectName].ToString();
        }

        private void UpdateCameraBasedPositions()
        {
            currentGameState.splashImage.Position = SetCameraBasedVector(0, 0);

            playerNamePosition = SetCameraBasedVector(0, 0);
            playerScorePosition = SetCameraBasedVector(0, 20);
            debugPlayerPosition = SetCameraBasedVector(0, 40);
            debugTextPosition = SetCameraBasedVector(0, 60);
        }

        private Vector2 SetCameraBasedVector(int xOffset, int yOffset)
        {
            return new Vector2(
                -camera.transform.Translation.X + xOffset,
                -camera.transform.Translation.Y + yOffset);

            //return new Vector2(
            //    currentGameState.playerSprite.Position.X - Program.width/2 + currentGameState.playerSprite.Width/2 + xOffset,
            //    currentGameState.playerSprite.Height/2 + yOffset);
        }

        protected override void Draw(GameTime gameTime)
        {
            //renders to the target
            GraphicsDevice.SetRenderTarget(renderTarget);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(transformMatrix: camera.transform);

            currentGameState.Render(spriteBatch);

            if (currentGameState.playerSprite != null)
            {
                spriteBatch.DrawString(font, "player: " + playerName, playerNamePosition, Color.Black);
                spriteBatch.DrawString(font, "score: " + playerScore, playerScorePosition, Color.Black);
                spriteBatch.DrawString(font, "player: " + currentGameState.playerSprite.Position.ToString(), debugPlayerPosition, Color.Black);
                spriteBatch.DrawString(font, "debug: " + currentGameState.debugText, debugTextPosition, Color.Black);
            }

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
