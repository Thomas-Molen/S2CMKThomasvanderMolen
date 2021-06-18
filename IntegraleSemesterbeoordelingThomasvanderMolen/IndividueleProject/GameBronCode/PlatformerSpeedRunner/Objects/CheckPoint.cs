using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PlatformerSpeedRunner.Objects.Base;

namespace PlatformerSpeedRunner.Objects
{
    public class CheckPoint : RenderAbleObject
    {
        public bool activated { get; private set; } = false;

        public CheckPoint(ContentManager contentManager, Vector2 newPosition)
        {
            SetTextureContentManager(contentManager);
            Texture.SetTexture(Textures.checkpoint);
            Position.SetPosition(newPosition);

            BoundingBox.AddBoundingBox(new BoundingBoxObject(new Vector2(Position.position.X, Position.position.Y), 38, 72));
        }

        public void Activate()
        {
            activated = true;
            Texture.SetTexture(Textures.checkpointActivated);
        }
    }
}
