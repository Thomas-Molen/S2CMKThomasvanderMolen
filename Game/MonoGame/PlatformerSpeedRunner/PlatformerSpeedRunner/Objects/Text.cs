using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerSpeedRunner.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerSpeedRunner.Objects
{
    public class Text
    {
        public RenderHelper Render = new RenderHelper();
        public PositionHelper Position = new PositionHelper();
        public string content;
        public Vector2 originalPosition { private set; get; }

        public Text(string inputContent, Vector2 Position)
        {
            this.Position.SetPosition(Position);
            content = inputContent;
            originalPosition = Position;
        }
    }
}
