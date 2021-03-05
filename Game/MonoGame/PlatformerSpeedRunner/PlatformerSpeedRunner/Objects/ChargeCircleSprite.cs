using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerSpeedRunner.Objects.Base;

namespace PlatformerSpeedRunner.Objects
{
    public class ChargeCircleSprite : BaseGameObject
    {
        public ChargeCircleSprite(Texture2D texture)
        {
            baseTexture = texture;
        }
    }
}
