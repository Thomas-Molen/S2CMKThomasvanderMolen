using Microsoft.Xna.Framework;
using PlatformerSpeedRunner.Objects;
using System.Collections.Generic;

namespace PlatformerSpeedRunner.Helper
{
    public class BoundingBoxHelper
    {
        public List<BoundingBoxObject> boundingBoxes { get; private set; }
        private Vector2 basePosition = Vector2.Zero;

        public BoundingBoxHelper()
        {
            boundingBoxes = new List<BoundingBoxObject>();
        }

        public void AddBoundingBox(BoundingBoxObject bb)
        {
            boundingBoxes.Add(bb);
        }

        public void UpdateBoundingBoxes(Vector2 Position)
        {
            var deltaX = Position.X - basePosition.X;
            var deltaY = Position.Y - basePosition.Y;
            basePosition = Position;

            foreach (var bb in boundingBoxes)
            {
                bb.position = new Vector2(bb.position.X + deltaX, bb.position.Y + deltaY);
            }
        }
    }
}
