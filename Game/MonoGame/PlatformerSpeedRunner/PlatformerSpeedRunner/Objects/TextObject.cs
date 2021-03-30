using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerSpeedRunner.Objects
{
    public class TextObject
    {
        protected Vector2 basePosition = Vector2.One;
        public string content;
        public Vector2 originalPosition { private set; get; }
        public virtual Vector2 Position
        {
            get { return basePosition; }
            set
            {
                var deltaX = value.X - basePosition.X;
                var deltaY = value.Y - basePosition.Y;
                basePosition = value;
            }
        }

        public TextObject(string inputContent, Vector2 InputPosition)
        {
            content = inputContent;
            originalPosition = InputPosition;
            Position = InputPosition;
        }

        public void Render(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.DrawString(font, content, Position, Color.Black);
        }
    }
}
