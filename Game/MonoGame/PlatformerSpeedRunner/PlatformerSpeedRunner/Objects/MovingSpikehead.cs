using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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

        public MovingSpikeHead(ContentManager contentManager, Vector2 newPosition, int inputMinPosY, int inputMaxPosY)
        {
            SetTextureContentManager(contentManager);
            Movement = new EnemyMovementHelper(3, newMinPosY: inputMinPosY, newMaxPosY: inputMaxPosY );
            Texture.SetTexture(Texture.GetTexture2D(Textures.spikeHead));
            Position.SetPosition(newPosition);

            BoundingBox.AddBoundingBox(new BoundingBoxObject(new Vector2(30, 0), BBWidth1, BBHeight1));
            BoundingBox.AddBoundingBox(new BoundingBoxObject(new Vector2(0, 30), BBWidth2, BBHeight2));
        }

        public void EnemyUpdate()
        {
            Movement.MoveVertical(this);
        }
    }
}
