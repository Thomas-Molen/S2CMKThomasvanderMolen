using Microsoft.Xna.Framework.Input;
using System;

namespace PlatformerSpeedRunner.Input.Base
{
    public class InputManager
    {
        private readonly BaseInputMapper baseInputMapper;

        public InputManager(BaseInputMapper inputMapper)
        {
            baseInputMapper = inputMapper;
        }

        public void GetCommands(Action<BaseInputCommand> actOnState)
        {
            var keyboardState = Keyboard.GetState();
            foreach (var state in baseInputMapper.GetKeyboardState(keyboardState))
            {
                actOnState(state);
            }

            var mouseState = Mouse.GetState();
            foreach (var state in baseInputMapper.GetMouseState(mouseState))
            {
                actOnState(state);
            }
        }
    }
}
