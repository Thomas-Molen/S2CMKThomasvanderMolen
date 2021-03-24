using PlatformerSpeedRunner.Objects.Base;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformerSpeedRunner.Objects
{
    public class BackgroundSprite : BaseGameObject
    {
        public BackgroundSprite(Texture2D texture)
        {
            baseTexture = texture;
        }
    }
}
