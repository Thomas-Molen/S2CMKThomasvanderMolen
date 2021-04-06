using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerSpeedRunner.Helper
{
    public class PositionHelper
    {
        public Vector2 position { get; private set; }

        public void SetPosition(Vector2 Position)
        {
            position = Position;
        }
    }
}
