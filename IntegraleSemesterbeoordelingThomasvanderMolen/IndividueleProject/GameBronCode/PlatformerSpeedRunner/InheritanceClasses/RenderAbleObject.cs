using Microsoft.Xna.Framework.Content;
using PlatformerSpeedRunner.Helper;

namespace PlatformerSpeedRunner.Objects.Base
{
    public class RenderAbleObject
    {
        public TextureList Textures { get; private set; }
        public TextureHelper Texture { get; private set; }
        public PositionHelper Position { get; private set; }
        public BoundingBoxHelper BoundingBox { get; private set; }
        public RenderHelper Render { get; private set; }

        public RenderAbleObject()
        {
            Textures = new TextureList();
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
