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
                commands.Add(new GameplayInputCommand.Exit());
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

            if (state.IsKeyDown(Keys.W))
            {
                commands.Add(new GameplayInputCommand.PlayerMoveUp());
            }

            //DEBUG BOUNDINGBOXES
            if (state.IsKeyDown(Keys.OemTilde))
            {
                commands.Add(new GameplayInputCommand.DebugOn());
            }

            if (state.IsKeyDown(Keys.Tab))
            {
                commands.Add(new GameplayInputCommand.DebugOff());
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
