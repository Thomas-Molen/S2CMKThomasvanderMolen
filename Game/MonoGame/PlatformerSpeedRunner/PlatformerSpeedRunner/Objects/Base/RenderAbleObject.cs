using PlatformerSpeedRunner.Enum;
using PlatformerSpeedRunner.States.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using PlatformerSpeedRunner.Helper;

namespace PlatformerSpeedRunner.Objects.Base
{
    public class RenderAbleObject
    {
        public TextureHelper textureHelper = new TextureHelper();
        public PositionHelper positionHelper = new PositionHelper();

        //public int Width { get { return baseTexture.Width; } }
        //public int Height { get { return baseTexture.Height; } }

        //public Animations AnimationState { get; set; }

        //public virtual Vector2 Position
        //{
        //    get { return basePosition; }
        //    set
        //    {
        //        var deltaX = value.X - basePosition.X;
        //        var deltaY = value.Y - basePosition.Y;
        //        basePosition = value;

        //        foreach (var bb in boundingBoxes)
        //        {
        //            bb.position = new Vector2(bb.position.X + deltaX, bb.position.Y + deltaY);
        //        }
        //    }
        //}

        //public List<BoundingBoxObject> BoundingBoxes
        //{
        //    get
        //    {
        //        return boundingBoxes;
        //    }
        //}

        public virtual void OnNotify(Events eventType) { }

        public void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureHelper.texture, positionHelper.position, Color.White);
        }

        //public void RenderBoundingBoxes(SpriteBatch spriteBatch)
        //{
        //    if (boundingBoxTexture == null)
        //    {
        //        CreateBoundingBoxTexture(spriteBatch.GraphicsDevice);
        //    }

        //    foreach (var bb in boundingBoxes)
        //    {
        //        spriteBatch.Draw(boundingBoxTexture, bb.rectangle, Color.Red);
        //    }
        //}

        //public void AddBoundingBox(BoundingBoxObject bb)
        //{
        //    boundingBoxes.Add(bb);
        //}

        //private void CreateBoundingBoxTexture(GraphicsDevice graphicsDevice)
        //{
        //    boundingBoxTexture = new Texture2D(graphicsDevice, 1, 1);
        //    boundingBoxTexture.SetData<Color>(new Color[] { Color.White });
        //}

        //public void ChangeTexture(Texture2D Texture)
        //{
        //    baseTexture = Texture;
        //}
    }
}
