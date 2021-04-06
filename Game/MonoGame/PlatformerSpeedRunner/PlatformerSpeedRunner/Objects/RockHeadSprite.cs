using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerSpeedRunner.Helper;
using PlatformerSpeedRunner.Objects.Base;

namespace PlatformerSpeedRunner.Objects
{
    public class RockHeadSprite : RenderAbleObject
    {
        public BoundingBoxHelper boundingBoxHelper = new BoundingBoxHelper();
        public RenderHelper renderHelper = new RenderHelper();
        private const int BBWidth = 129;
        private const int BBHeight = 129;
        private readonly int minPos;
        private readonly int maxPos;

        private readonly int movementSpeed = 2;
        public int velocity;
        private readonly Texture2D idleTexture;

        public RockHeadSprite(Texture2D Texture, Vector2 Position, int inputMinPos, int inputMaxPos)
        {
            positionHelper.SetPosition(Position);
            textureHelper.SetTexture(Texture);
            idleTexture = Texture;
            minPos = inputMinPos;
            maxPos = inputMaxPos;

            velocity = movementSpeed;

            boundingBoxHelper.AddBoundingBox(new BoundingBoxObject(new Vector2(0, 0), BBWidth, BBHeight));
        }

        public void Movement()
        {
            boundingBoxHelper.UpdateBoundingBoxes(positionHelper.position);
            if (textureHelper.texture != idleTexture)
            {
                textureHelper.SetTexture(idleTexture);
            }

            if (positionHelper.position.X >= maxPos)
            {
                velocity = -movementSpeed;
            }
            else if (positionHelper.position.X <= minPos)
            {
                velocity = movementSpeed;
            }
            positionHelper.SetPosition(new Vector2(positionHelper.position.X + velocity, positionHelper.position.Y));
        }
    }
}
