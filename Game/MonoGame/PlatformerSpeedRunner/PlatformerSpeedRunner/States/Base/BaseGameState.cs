using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using PlatformerSpeedRunner.Enum;
using PlatformerSpeedRunner.Objects.Base;
using PlatformerSpeedRunner.Input.Base;
using Microsoft.Xna.Framework;
using PlatformerSpeedRunner.Objects;
using PlatformerSpeedRunner.Camera;

namespace PlatformerSpeedRunner.States.Base
{
    public abstract class BaseGameState
    {
        public string debugText;

        protected bool debug = false;

        public PlayerSprite playerSprite;

        public ChargeCircleSprite chargeCircleSprite;

        public ObjectSprite endFlag;

        public SplashImage splashImage;

        public CameraHelper camera;

        private ContentManager baseContentManager;
        protected int baseViewportHeight;
        protected int baseViewportWidth;

        private readonly List<BaseGameObject> gameObjects = new List<BaseGameObject>();

        protected InputManager InputManager { get; set; }

        protected abstract void SetInputManager();

        private const string fallBackTexture = "ErrorSprite";

        public void Initialize(ContentManager contentManager, int viewportWidth, int viewportHeight, CameraHelper inputCamera)
        {
            camera = inputCamera;
            baseViewportHeight = viewportHeight;
            baseViewportWidth = viewportWidth;

            baseContentManager = contentManager;

            contentManager.Load<SpriteFont>("Fonts\\GuiFont");

            SetInputManager();
        }

        public abstract void LoadContent();
        public void UnloadContent()
        {
            baseContentManager.Unload();
        }

        public void Update(GameTime gameTime)
        {
            UpdateGameState(gameTime);
            debugText = camera.transform.Translation.X.ToString();
        }

        public abstract void HandleInput();

        public abstract void UpdateGameState(GameTime gameTime);

        public event EventHandler<BaseGameState> OnStateSwitched;

        public event EventHandler<Events> OnEventNotification;


        protected Texture2D LoadTexture(string textureName)
        {
            try
            {
                var texture = baseContentManager.Load<Texture2D>(textureName);
                return texture;
            }
            catch (Microsoft.Xna.Framework.Content.ContentLoadException)
            {
                var texture = baseContentManager.Load<Texture2D>(fallBackTexture);
                return texture;
            }
        }

        protected void NotifyEvent(Events eventType)
        {
            OnEventNotification?.Invoke(this, eventType);

            foreach (var gameObject in gameObjects)
            {
                gameObject.OnNotify(eventType);
            }
        }

        protected void SwitchState(BaseGameState gameState)
        {
            OnStateSwitched?.Invoke(this, gameState);
        }

        protected void AddGameObject(BaseGameObject gameObject)
        {
            gameObjects.Add(gameObject);
        }

        protected void RemoveGameObject(BaseGameObject gameObject)
        {
            gameObjects.Remove(gameObject);
        }

        public void Render(SpriteBatch spriteBatch)
        {
            foreach (var gameObject in gameObjects.OrderBy(a => a.zIndex))
            {
                //gameObject.Render(spriteBatch);
                if (debug)
                {
                    gameObject.RenderBoundingBoxes(spriteBatch);
                }
                gameObject.Render(spriteBatch);
            }
        }
    }
}
