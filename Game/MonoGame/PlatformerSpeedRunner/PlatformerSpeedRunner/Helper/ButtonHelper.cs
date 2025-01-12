﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PlatformerSpeedRunner.Camera;
using PlatformerSpeedRunner.Objects;

namespace PlatformerSpeedRunner.Helper
{
    public class ButtonHelper
    {
        public bool IsMouseOnButton(BasicObject button, CameraHelper camera)
        {
            MouseState mouseState = Mouse.GetState();
            Vector2 mousePosition = camera.GetCameraBasedPosition(new Vector2(mouseState.X, mouseState.Y));
            Vector2 buttonTopLeftPos = new Vector2(button.Position.position.X, button.Position.position.Y);
            Vector2 buttonBottomRightPos = new Vector2(buttonTopLeftPos.X + button.Texture.width, buttonTopLeftPos.Y + button.Texture.height);
            if (IsMouseInPos(mousePosition.X, buttonTopLeftPos.X, buttonBottomRightPos.X) && IsMouseInPos(mousePosition.Y, buttonTopLeftPos.Y, buttonBottomRightPos.Y))
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
