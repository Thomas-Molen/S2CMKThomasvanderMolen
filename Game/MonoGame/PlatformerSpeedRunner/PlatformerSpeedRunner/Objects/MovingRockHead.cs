using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PlatformerSpeedRunner.Helper;
using PlatformerSpeedRunner.Objects.Base;

namespace PlatformerSpeedRunner.Objects
{
    public class MovingRockHead : RenderAbleObject
    {
        private const int BBWidth = 129;
        private const int BBHeight = 129;

        private readonly Texture2D idleTexture;
        private readonly Texture2D angryTexture;
        public EnemyMovementHelper Movement;

        public MovingRockHead(ContentManager contentManager, Vector2 newPosition, int inputMinPos, int inputMaxPos)
        {
            SetTextureContentManager(contentManager);
            Movement = new EnemyMovementHelper(2, inputMinPos, inputMaxPos);
            Position.SetPosition(newPosition);
            idleTexture = Texture.GetTexture2D("Enemies\\RockHeadIdle");
            angryTexture = Texture.GetTexture2D("Enemies\\RockHeadMad");
            Texture.SetTexture(idleTexture);

            BoundingBox.AddBoundingBox(new BoundingBoxObject(new Vector2(0, 0), BBWidth, BBHeight));
        }

        public void EnemyUpdate()
        {
            if (Texture.texture != idleTexture)
            {
                Texture.SetTexture(idleTexture);
            }
            Movement.MoveHorizontal(this);
        }

        public void MakeRockheadMad()
        {
            base.Texture.SetTexture(angryTexture);
        }
    }
}
