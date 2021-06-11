using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerSpeedRunner.Helper;
using PlatformerSpeedRunner.Objects.Base;

namespace PlatformerSpeedRunner.Objects
{
    public class MovingSpikeHead : RenderAbleObject
    {
        private const int BBWidth1 = 75;
        private const int BBHeight1 = 129;
        private const int BBWidth2 = 129;
        private const int BBHeight2 = 75;
        public EnemyMovementHelper Movement;

        public MovingSpikeHead(Texture2D Texture, Vector2 Position, int inputMinPosY, int inputMaxPosY)
        {
            Movement = new EnemyMovementHelper(3, newMinPosY: inputMinPosY, newMaxPosY: inputMaxPosY );
            base.Position.SetPosition(Position);
            base.Texture.SetTexture(Texture);

            BoundingBox.AddBoundingBox(new BoundingBoxObject(new Vector2(30, 0), BBWidth1, BBHeight1));
            BoundingBox.AddBoundingBox(new BoundingBoxObject(new Vector2(0, 30), BBWidth2, BBHeight2));
        }

        public void EnemyUpdate()
        {
            Movement.MoveVertical(this);
        }
    }
}
