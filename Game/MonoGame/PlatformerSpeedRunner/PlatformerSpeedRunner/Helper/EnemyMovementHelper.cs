using Microsoft.Xna.Framework;
using PlatformerSpeedRunner.Objects.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerSpeedRunner.Helper
{
    public class EnemyMovementHelper
    {
        public int xVelocity { get; private set; } = 0;
        public int yVelocity { get; private set; } = 0;
        private int movementSpeed;

        private int minPosX;
        private int maxPosX;
        private int minPosY;
        private int maxPosY;
        public EnemyMovementHelper(int newMovementSpeed, int newMinPosX = 0, int newMaxPosX = 0, int newMinPosY = 0, int newMaxPosY = 0)
        {
            movementSpeed = newMovementSpeed;
            minPosX = newMinPosX;
            maxPosX = newMaxPosX;
            minPosY = newMinPosY;
            maxPosY = newMaxPosY;

            yVelocity = newMovementSpeed;
            xVelocity = newMovementSpeed;
        }

        public void MoveHorizontal(RenderAbleObject enemy)
        {
            enemy.BoundingBox.UpdateBoundingBoxes(enemy.Position.position);
            if (enemy.Position.position.X >= maxPosX)
            {
                xVelocity = -movementSpeed;
            }
            else if (enemy.Position.position.X <= minPosX)
            {
                xVelocity = movementSpeed;
            }
            enemy.Position.SetPosition(new Vector2(enemy.Position.position.X + xVelocity, enemy.Position.position.Y));
        }

        public void MoveVertical(RenderAbleObject enemy)
        {
            enemy.BoundingBox.UpdateBoundingBoxes(enemy.Position.position);
            if (enemy.Position.position.Y >= maxPosY)
            {
                yVelocity = -movementSpeed;
            }
            else if (enemy.Position.position.Y <= minPosY)
            {
                yVelocity = movementSpeed;
            }
            enemy.Position.SetPosition(new Vector2(enemy.Position.position.X, enemy.Position.position.Y + yVelocity));
        }
    }
}
