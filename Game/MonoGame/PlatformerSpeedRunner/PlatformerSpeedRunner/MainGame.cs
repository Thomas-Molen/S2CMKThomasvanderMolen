using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MySql.Data;


using PlatformerSpeedRunner.Enum;
using PlatformerSpeedRunner.Objects;
using PlatformerSpeedRunner.States;
using PlatformerSpeedRunner.States.Base;
using PlatformerSpeedRunner.Camera;
using System.Data;
using System;

namespace PlatformerSpeedRunner
{
    public class MainGame : Game
    {
       //components
        private BaseGameState currentGameState;

        private readonly GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private CameraHelper camera;


        private readonly int designedResolutionWidth;
        private readonly int designedResolutionHeight;

        public MainGame(int width, int height)
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            designedResolutionWidth = width;
            designedResolutionHeight = height;
        }

        protected override void Initialize()
        {
            //set rendertarget
            graphics.PreferredBackBufferWidth = designedResolutionWidth;
            graphics.PreferredBackBufferHeight = designedResolutionHeight;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

            camera = new CameraHelper();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //TODO CHANGE THIS TO THE BOOT SCREEN
            SwitchGameState(new MenuState());
        }

        //switch state logic
        private void CurrentGameState_OnStateSwitched(object sender, BaseGameState gameState)
        {
            SwitchGameState(gameState);
        }

        private void SwitchGameState(BaseGameState gameState)
        {
            if (currentGameState != null)
            {
                if (currentGameState.currentState == GameState.MainMenu)
                {

                }
                currentGameState.OnStateSwitched -= CurrentGameState_OnStateSwitched;
                currentGameState.UnloadContent();
            }

            currentGameState = gameState;

            currentGameState.Initialize(Content, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height, camera);

            currentGameState.LoadContent();

            currentGameState.OnStateSwitched += CurrentGameState_OnStateSwitched;
        }

        protected override void UnloadContent()
        {
            currentGameState?.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            currentGameState.HandleInput();

            currentGameState.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //renders to the target
            GraphicsDevice.SetRenderTarget(null);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(transformMatrix: camera.transform);

            currentGameState.Render(spriteBatch);
            currentGameState.RenderText(spriteBatch);
            currentGameState.RenderBoundingBoxes(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
