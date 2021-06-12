using Microsoft.Xna.Framework.Content;
using PlatformerSpeedRunner.Helper;

namespace PlatformerSpeedRunner.Objects.Base
{
    public class RenderAbleObject
    {
        public TextureHelper Texture;
        public PositionHelper Position;
        public BoundingBoxHelper BoundingBox;
        public RenderHelper Render;

        public RenderAbleObject()
        {
            Position = new PositionHelper();
            BoundingBox = new BoundingBoxHelper();
            Render = new RenderHelper();
        }

        public void SetTextureContentManager(ContentManager contentManager)
        {
            Texture = new TextureHelper(contentManager);
        }
    }
}
