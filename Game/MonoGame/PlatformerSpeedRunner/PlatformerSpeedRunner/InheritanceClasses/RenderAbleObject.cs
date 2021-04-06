using PlatformerSpeedRunner.Enum;
using PlatformerSpeedRunner.States.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using PlatformerSpeedRunner.Helper;

namespace PlatformerSpeedRunner.Objects.Base
{
    public class RenderAbleObject
    {
        public TextureHelper Texture = new TextureHelper();
        public PositionHelper Position = new PositionHelper();

        public void RenderSprite(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture.texture, Position.position, Color.White);
        }
    }
}
