﻿using Microsoft.Xna.Framework;

namespace PlatformerSpeedRunner.Objects
{
    public class BoundingBoxObject
    {
        public Vector2 position { get; set; }
        private float width { get; set; }
        private float height { get; set; }

        public Rectangle rectangle
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, (int)width, (int)height);
            }
        }

        public BoundingBoxObject(Vector2 position, float width, float height)
        {
            this.position = position;
            this.width = width;
            this.height = height;
        }

        public bool CollidesWith(BoundingBoxObject otherBB)
        {
            if (position.X < otherBB.position.X + otherBB.width &&
                position.X + width > otherBB.position.X &&
                position.Y < otherBB.position.Y + otherBB.height &&
                position.Y + height > otherBB.position.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}