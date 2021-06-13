using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PlatformerSpeedRunner.Objects.Base;

namespace PlatformerSpeedRunner.Objects
{
    public class BasicObject : RenderAbleObject
    {
        public BasicObject(ContentManager contentManager, string textureString, Vector2 newPosition, bool BoundingBox = false)
        {
            SetTextureContentManager(contentManager);
            Position.SetPosition(newPosition);
            Texture.SetTexture(textureString);

            if (BoundingBox)
            {
                this.BoundingBox.AddBoundingBox(new BoundingBoxObject(new Vector2(Position.position.X, Position.position.Y), base.Texture.width + 1, base.Texture.height + 1));
            }
        }
    }
}
