using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PlatformerSpeedRunner.Helper;
using PlatformerSpeedRunner.Objects.Base;

namespace PlatformerSpeedRunner.Objects
{
    public class MovingRockHead : RenderAbleObject
    {
        private const int BBWidth = 129;
        private const int BBHeight = 129;

        public EnemyMovementHelper Movement;

        public MovingRockHead(ContentManager contentManager, Vector2 newPosition, int inputMinPos, int inputMaxPos)
        {
            SetTextureContentManager(contentManager);
            Movement = new EnemyMovementHelper(2, inputMinPos, inputMaxPos);
            Position.SetPosition(newPosition);
            Texture.SetTexture(Texture.GetTexture2D(Textures.rockHead));

            BoundingBox.AddBoundingBox(new BoundingBoxObject(new Vector2(0, 0), BBWidth, BBHeight));
        }

        public void EnemyUpdate()
        {
            if (Texture.texture.ToString() != Textures.rockHead)
            {
                Texture.SetTexture(Texture.GetTexture2D(Textures.rockHead));
            }
            Movement.MoveHorizontal(this);
        }

        public void MakeRockheadMad()
        {
            base.Texture.SetTexture(Texture.GetTexture2D(Textures.rockHeadAngry));
        }
    }
}
