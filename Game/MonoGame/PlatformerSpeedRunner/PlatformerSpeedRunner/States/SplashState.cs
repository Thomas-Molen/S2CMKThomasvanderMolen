using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using PlatformerSpeedRunner.Objects;
using PlatformerSpeedRunner.States.Base;
using PlatformerSpeedRunner.Input;
using PlatformerSpeedRunner.Input.Base;

namespace PlatformerSpeedRunner.States
{
    public class SplashState : BaseGameState
    {
        public override void LoadContent()
        {
            backgroundImage = new ObjectSprite(LoadTexture("Backgrounds\\splash"));

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

        public override void UpdateGameState(GameTime gameTime) { }

        protected override void SetInputManager()
        {
            InputManager = new InputManager(new SplashInputMapper());
        }
    }
}
