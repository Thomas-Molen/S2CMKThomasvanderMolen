using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerSpeedRunner.Objects;
using System.Collections.Generic;

namespace PlatformerSpeedRunner.Helper
{
    public class BoundingBoxHelper
    {
        protected List<BoundingBoxObject> boundingBoxes;
        protected Texture2D boundingBoxTexture;

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
    }
}
