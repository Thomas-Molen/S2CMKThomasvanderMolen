using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PlatformerSpeedRunner.Camera;
using PlatformerSpeedRunner.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerSpeedRunner.Helper
{
    public class ButtonHelper
    {
        public bool IsMouseOnButton(BasicObject button, CameraHelper camera)
        {
            MouseState mouseState = Mouse.GetState();
            Vector2 mousePosition = camera.GetCameraBasedPosition(new Vector2(mouseState.X, mouseState.Y));
            Vector2 buttonTopLeft = new Vector2(button.Position.position.X, button.Position.position.Y);
            Vector2 buttonBottomRight = new Vector2(buttonTopLeft.X + button.Texture.Width, buttonTopLeft.Y + button.Texture.Height);
            if (IsMouseInPos(mousePosition.X, buttonTopLeft.X, buttonBottomRight.X) && IsMouseInPos(mousePosition.Y, buttonTopLeft.Y, buttonBottomRight.Y))
            {
                return true;
            }
            return false;
        }

        private bool IsMouseInPos(float mousePos, float PosMin, float PosMax)
        {
            if (mousePos > PosMin && mousePos < PosMax)
            {
                return true;
            }
            return false;
        }
    }
}
