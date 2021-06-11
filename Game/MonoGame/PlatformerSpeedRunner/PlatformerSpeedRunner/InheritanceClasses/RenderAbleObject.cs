using PlatformerSpeedRunner.Enum;
using PlatformerSpeedRunner.States.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using PlatformerSpeedRunner.Helper;
using Microsoft.Xna.Framework.Content;

namespace PlatformerSpeedRunner.Objects.Base
{
    public class RenderAbleObject
    {
        public TextureHelper Texture;
        public PositionHelper Position;
        public BoundingBoxHelper BoundingBox;
        public RenderHelper Render;

        public RenderAbleObject()
        {
            Position = new PositionHelper();
            BoundingBox = new BoundingBoxHelper();
            Render = new RenderHelper();
        }

        public void SetTextureContentManager(ContentManager contentManager)
        {
            Texture = new TextureHelper(contentManager);
        }
    }
}
