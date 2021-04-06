﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerSpeedRunner.Objects;
using System.Collections.Generic;

namespace PlatformerSpeedRunner.Helper
{
    public class BoundingBoxHelper
    {
        public List<BoundingBoxObject> boundingBoxes { get; private set; }
        private Texture2D boundingBoxTexture;
        private Vector2 basePosition = Vector2.One;

        public BoundingBoxHelper()
        {
            boundingBoxes = new List<BoundingBoxObject>();
        }

        public void AddBoundingBox(BoundingBoxObject bb)
        {
            boundingBoxes.Add(bb);
        }

        private void CreateBoundingBoxTexture(GraphicsDevice graphicsDevice)
        {
            boundingBoxTexture = new Texture2D(graphicsDevice, 1, 1);
            boundingBoxTexture.SetData<Color>(new Color[] { Color.White });
        }

        public void RenderBoundingBoxes(SpriteBatch spriteBatch)
        {
            if (boundingBoxTexture == null)
            {
                CreateBoundingBoxTexture(spriteBatch.GraphicsDevice);
            }

            foreach (var bb in boundingBoxes)
            {
                spriteBatch.Draw(boundingBoxTexture, bb.rectangle, Color.Red);
            }
        }

        public void UpdateBoundingBoxes(Vector2 Position)
        {
            var deltaX = Position.X - basePosition.X;
            var deltaY = Position.Y - basePosition.Y;
            basePosition = Position;

            foreach (var bb in boundingBoxes)
            {
                bb.position = new Vector2(bb.position.X + deltaX, bb.position.Y + deltaY);
            }
        }
    }
}
