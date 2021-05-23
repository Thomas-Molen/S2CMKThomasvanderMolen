using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using PlatformerSpeedRunner.Input.Base;

namespace PlatformerSpeedRunner.Input
{
    class SplashInputMapper : BaseInputMapper
    {
        public override IEnumerable<BaseInputCommand> GetKeyboardState(KeyboardState state)
        {
            var commands = new List<SplashInputCommand>();

            if (state.IsKeyDown(Keys.Enter))
            {
                commands.Add(new SplashInputCommand.GameSelect());
            }
            if (state.IsKeyDown(Keys.Escape))
            {
                commands.Add(new SplashInputCommand.GameExit());
            }

            return commands;
        }

        public override IEnumerable<BaseInputCommand> GetMouseState(MouseState state)
        {
            var commands = new List<SplashInputCommand>();

            if (state.LeftButton == ButtonState.Pressed)
            {
                commands.Add(new SplashInputCommand.PlayerLMBPress());
            }
            if (state.LeftButton == ButtonState.Released)
            {
                commands.Add(new SplashInputCommand.PlayerLMBRelease());
            }

            return commands;
        }
    }
}
