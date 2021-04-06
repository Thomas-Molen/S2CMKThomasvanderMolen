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
                if (playerSprite.GetPosition().X < Program.width / 2)
                {
                    position = Matrix.CreateTranslation(
                        -playerSprite.GetPosition().X - (playerSprite.textureHelper.Width / 2),
                        -playerSprite.GetPosition().Y - (playerSprite.textureHelper.Height / 2),
                        0);

                    offset = Matrix.CreateTranslation(
                        playerSprite.GetPosition().X,
                        playerSprite.GetPosition().Y,
                        0);
                }
                else if (playerSprite.cameraState == CameraMode.Horizontal)
                {
                    position = Matrix.CreateTranslation(
                        -playerSprite.GetPosition().X - (playerSprite.textureHelper.Width / 2),
                        -playerSprite.GetPosition().Y - (playerSprite.textureHelper.Height / 2),
                        0);

                    offset = Matrix.CreateTranslation(
                        Program.width / 2,
                        playerSprite.GetPosition().Y,
                        0);
                }
                else if (playerSprite.cameraState == CameraMode.Vertical)
                {
                    position = Matrix.CreateTranslation(
                        -playerSprite.GetPosition().X - (playerSprite.textureHelper.Width / 2),
                        -playerSprite.GetPosition().Y - (playerSprite.textureHelper.Height / 2),
                        0);

                    offset = Matrix.CreateTranslation(
                        Program.width / 2,
                        playerSprite.GetPosition().Y,
                        0);
                }
                else if (playerSprite.cameraState == CameraMode.Free)
                {
                    position = Matrix.CreateTranslation(
                        -playerSprite.GetPosition().X - (playerSprite.textureHelper.Width / 2),
                        -playerSprite.GetPosition().Y - (playerSprite.textureHelper.Height / 2),
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

            transform = position * offset;
        }

        public void Follow()
        {
            transform = Matrix.CreateTranslation(0, 0, 0);
        }

        public Vector2 GetCameraBasedPosition(Vector2 BasePosition)
        {
            return new Vector2(-transform.Translation.X + BasePosition.X, -transform.Translation.Y + BasePosition.Y);
        }
    }
}
