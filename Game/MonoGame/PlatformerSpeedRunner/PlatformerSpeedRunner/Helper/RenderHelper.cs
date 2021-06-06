using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerSpeedRunner.Objects;

namespace PlatformerSpeedRunner.Helper
{
    public class RenderHelper
    {
        public void RenderText(SpriteBatch spriteBatch, SpriteFont font, Text ObjectToRender)
        {
            spriteBatch.DrawString(font, ObjectToRender.content, ObjectToRender.Position.position, Color.Black);
        }
    }
}
