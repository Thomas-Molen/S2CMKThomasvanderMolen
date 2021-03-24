using Microsoft.Xna.Framework;

namespace PlatformerSpeedRunner.Objects
{
    public class BoundingBox
    {
        public Vector2 position { get; set; }
        public float width { get; set; }
        public float height { get; set; }

        public Rectangle rectangle
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, (int)width, (int)height);
            }
        }

        public BoundingBox(Vector2 position, float width, float height)
        {
            this.position = position;
            this.width = width;
            this.height = height;
        }

        public bool CollidesWith(BoundingBox otherBB)
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
