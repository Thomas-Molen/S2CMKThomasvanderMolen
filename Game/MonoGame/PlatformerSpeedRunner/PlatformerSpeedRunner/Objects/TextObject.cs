using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerSpeedRunner.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerSpeedRunner.Objects
{
    public class TextObject
    {
        public RenderHelper renderHelper = new RenderHelper();
        public PositionHelper positionHelper = new PositionHelper();
        public string content;
        public Vector2 originalPosition { private set; get; }

        public TextObject(string inputContent, Vector2 Position)
        {
            positionHelper.SetPosition(Position);
            content = inputContent;
            originalPosition = Position;
        }
    }
}
