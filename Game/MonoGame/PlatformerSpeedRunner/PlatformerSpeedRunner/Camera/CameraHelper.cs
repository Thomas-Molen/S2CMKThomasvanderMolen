using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using PlatformerSpeedRunner.Objects;

namespace PlatformerSpeedRunner.Camera
{
    public class CameraHelper
    {
        public Matrix transform { get; private set; }

        private Matrix position;
        private Matrix offset;
        public void Follow (PlayerSprite playerSprite)
        {
            if (playerSprite.Position.X > Program.width / 2)
            {
                position = Matrix.CreateTranslation(
                    -playerSprite.Position.X - (playerSprite.Width / 2),
                    -playerSprite.Position.Y - (playerSprite.Height / 2),
                    0);

                offset = Matrix.CreateTranslation(
                    Program.width / 2,
                    playerSprite.Position.Y,
                    0);  
            }
            else
            {
                position = Matrix.CreateTranslation(
                    -playerSprite.Position.X - (playerSprite.Width / 2),
                    -playerSprite.Position.Y - (playerSprite.Height / 2),
                    0);

                offset = Matrix.CreateTranslation(
                    playerSprite.Position.X,
                    playerSprite.Position.Y,
                    0);
            }
            transform = position * offset;
        }
    }
}
