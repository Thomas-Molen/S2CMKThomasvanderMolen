using Microsoft.Xna.Framework;
using PlatformerSpeedRunner.Enum;
using PlatformerSpeedRunner.Helper;

namespace PlatformerSpeedRunner.Objects
{
    public class Text
    {
        public RenderHelper Render = new RenderHelper();
        public PositionHelper Position = new PositionHelper();
        public string content;
        public Fonts font;
        public Vector2 originalPosition { private set; get; }

        public Text(string inputContent, Vector2 Position, Fonts Font)
        {
            this.Position.SetPosition(Position);
            content = inputContent;
            originalPosition = Position;
            font = Font;
        }
    }
}
