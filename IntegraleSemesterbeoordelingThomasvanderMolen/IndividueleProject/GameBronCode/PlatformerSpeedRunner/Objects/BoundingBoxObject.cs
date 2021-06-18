using Microsoft.Xna.Framework;
using PlatformerSpeedRunner.Helper;

namespace PlatformerSpeedRunner.Objects
{
    public class BoundingBoxObject
    {
        public PositionHelper Position;
        private float width;
        private float height;

        public BoundingBoxObject(Vector2 position, float Width, float Height)
        {
            Position = new PositionHelper();
            Position.SetPosition(position);
            width = Width;
            height = Height;
        }

        public bool CollidesWith(BoundingBoxObject otherBB)
        {
            if (Position.position.X < otherBB.Position.position.X + otherBB.width &&
                Position.position.X + width > otherBB.Position.position.X &&
                Position.position.Y < otherBB.Position.position.Y + otherBB.height &&
                Position.position.Y + height > otherBB.Position.position.Y)
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
