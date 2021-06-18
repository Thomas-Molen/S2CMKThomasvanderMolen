using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerSpeedRunner.Objects;
using PlatformerSpeedRunner.Objects.Base;

namespace PlatformerSpeedRunner.Helper
{
    public class RenderHelper
    {
        public void RenderText(SpriteBatch spriteBatch, SpriteFont font, Text ObjectToRender)
        {
            spriteBatch.DrawString(font, ObjectToRender.content, ObjectToRender.Position.position, Color.Black);
        }

        public void RenderObject(SpriteBatch spriteBatch, RenderAbleObject ObjectToRender)
        {
            spriteBatch.Draw(ObjectToRender.Texture.texture, ObjectToRender.Position.position, Color.White);
        }
    }
}
