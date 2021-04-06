using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerSpeedRunner.Objects;
using PlatformerSpeedRunner.Objects.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

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
