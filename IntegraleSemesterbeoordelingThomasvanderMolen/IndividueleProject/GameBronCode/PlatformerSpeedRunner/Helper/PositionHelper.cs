using Microsoft.Xna.Framework;

namespace PlatformerSpeedRunner.Helper
{
    public class PositionHelper
    {
        public Vector2 position { get; private set; } = Vector2.Zero;

        public void SetPosition(Vector2 Position)
        {
            position = Position;
        }
    }
}
