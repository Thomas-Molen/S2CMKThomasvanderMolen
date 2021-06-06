using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using PlatformerSpeedRunner.Input.Base;

namespace PlatformerSpeedRunner.Input
{
    public class GameplayInputMapper : BaseInputMapper
    {
        public override IEnumerable<BaseInputCommand> GetKeyboardState(KeyboardState state)
        {
            var commands = new List<GameplayInputCommand>();

            //Extra
            if (state.IsKeyDown(Keys.Escape))
            {
                commands.Add(new GameplayInputCommand.ExitDown());
            }
            if (state.IsKeyUp(Keys.Escape))
            {
                commands.Add(new GameplayInputCommand.ExitUp());
            }
            if (state.IsKeyDown(Keys.R))
            {
                commands.Add(new GameplayInputCommand.RestartDown());
            }
            if (state.IsKeyUp(Keys.R))
            {
                commands.Add(new GameplayInputCommand.RestartUp());
            }
            //movement
            if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left))
            {
                commands.Add(new GameplayInputCommand.PlayerMoveLeft());
            }
            else if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
            {
                commands.Add(new GameplayInputCommand.PlayerMoveRight());
            }
            else
            {
                commands.Add(new GameplayInputCommand.PlayerMoveNone());
            }

            return commands;
        }

        public override IEnumerable<BaseInputCommand> GetMouseState(MouseState state)
        {
            var commands = new List<GameplayInputCommand>();

            if (state.LeftButton == ButtonState.Pressed)
            {
                commands.Add(new GameplayInputCommand.PlayerLMBHold());
            }

            if (state.LeftButton == ButtonState.Released)
            {
                commands.Add(new GameplayInputCommand.PlayerLMBRelease());
            }

            return commands;
        }
    }
}
