using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using PlatformerSpeedRunner.Objects;
using PlatformerSpeedRunner.Enum;

namespace PlatformerSpeedRunner.Camera
{
    public class CameraHelper
    {
        public Matrix transform { get; private set; }

        private Matrix position;
        private Matrix offset;
        public void Follow (PlayerSprite playerSprite)
        {
            if (playerSprite != null)
            {
                if (playerSprite.Position.X < Program.width / 2)
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
                else if (playerSprite.cameraState == CameraMode.Horizontal)
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
                else if (playerSprite.cameraState == CameraMode.Vertical)
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
                else if (playerSprite.cameraState == CameraMode.Free)
                {
                    position = Matrix.CreateTranslation(
                        -playerSprite.Position.X - (playerSprite.Width / 2),
                        -playerSprite.Position.Y - (playerSprite.Height / 2),
                        0);

                    offset = Matrix.CreateTranslation(
                        Program.width / 2,
                        Program.height / 2,
                        0);
                }
                else
                { }
            }
            else
            { }


            //if (playerSprite.Position.X >= 5000 && playerSprite.Position.Y <= Program.height / 1.5)
            //{
            //    position = Matrix.CreateTranslation(
            //        -5000 + playerSprite.Width + (Program.width / 2),
            //        -playerSprite.Position.Y - (playerSprite.Height / 2),
            //        0);

            //    offset = Matrix.CreateTranslation(
            //        1,
            //        Program.height / 1.5f,
            //        0);
            //}
            //else if (playerSprite.Position.X > Program.width / 2 && playerSprite.Position.X < 5000)
            //{
            //    position = Matrix.CreateTranslation(
            //        -playerSprite.Position.X - (playerSprite.Width / 2),
            //        -playerSprite.Position.Y - (playerSprite.Height / 2),
            //        0);

            //    offset = Matrix.CreateTranslation(
            //        Program.width / 2,
            //        playerSprite.Position.Y,
            //        0);
            //}
            //else
            //{ }
            transform = position * offset;
        }
    }
}
