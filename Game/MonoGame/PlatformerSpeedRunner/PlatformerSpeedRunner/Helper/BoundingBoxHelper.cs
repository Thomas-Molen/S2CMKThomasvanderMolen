using Microsoft.Xna.Framework;
using PlatformerSpeedRunner.Objects;
using System.Collections.Generic;

namespace PlatformerSpeedRunner.Helper
{
    public class BoundingBoxHelper
    {
        public List<BoundingBoxObject> boundingBoxes { get; private set; }
        private PositionHelper basePosition;

        public BoundingBoxHelper()
        {
            basePosition = new PositionHelper();
            boundingBoxes = new List<BoundingBoxObject>();
        }

        public void AddBoundingBox(BoundingBoxObject bb)
        {
            boundingBoxes.Add(bb);
        }

        public void UpdateBoundingBoxes(Vector2 newPosition)
        {
            var deltaX = newPosition.X - basePosition.position.X;
            var deltaY = newPosition.Y - basePosition.position.Y;
            basePosition.SetPosition(newPosition);

            foreach (var bb in boundingBoxes)
            {
                bb.Position.SetPosition(new Vector2(bb.Position.position.X + deltaX, bb.Position.position.Y + deltaY));
            }
        }
    }
}
