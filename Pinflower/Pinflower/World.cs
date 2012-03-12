using System;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace Pinflower
{
    public class World
    {
        protected PinflowerGame game;
        protected Player player;
        protected int height;
        protected int width;
        private const int redMask = 0x00ff0000;
        private const int blueMask = 0x0000ff00;
        private const int greenMask = 0x000000ff;
        protected Rectangle viewport;
        protected Vector2 halfViewport;
        protected Texture2D worldTexture;
        protected System.Drawing.Bitmap bitmap;

        public void Initialize(PinflowerGame _game, Player _player, Texture2D _texture)
        {
            game = _game;
            worldTexture = _texture;
            player = _player;
            viewport = new Rectangle(0, 0, PinflowerGame.viewportWidth, PinflowerGame.viewportHeight);
            halfViewport = new Vector2(viewport.Width / 2, viewport.Height / 2);
            MemoryStream stream = new MemoryStream();
            _texture.SaveAsPng(stream, _texture.Width, _texture.Height);
            bitmap = (System.Drawing.Bitmap)System.Drawing.Bitmap.FromStream(stream);
        }

        public void Update()
        {
            Rectangle bb = player.BoundingBox;
            bb.Offset((int)halfViewport.X, (int)halfViewport.Y);
            if (bb.Left <= 0 || bb.Right >= viewport.Width || bb.Bottom >= viewport.Height || bb.Top <= 0)
            {
                player.Position -= player.Velocity;
                player.Velocity.X = 0;
                return;
            }
            System.Drawing.Color color;
            for (int j = bb.Top; j < bb.Bottom; j++)
            {
                for (int i = bb.Left; i < bb.Right; i++)
                {
                    color = bitmap.GetPixel(i, j);
                    if (color.R == 0)
                    {
                        ResolveCollisions(bb, i, j);
                    }
                }
            }
        }

        public void ResolveCollisions(Rectangle boundingBox, int x, int y)
        {
            Vector2 depth = boundingBox.GetIntersectionDepth(new Rectangle(x, y, 1, 1));
            if (depth != Vector2.Zero)
            {
                player.Position -= player.Velocity;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(worldTexture, new Vector2(-player.Position.X, -player.Position.Y), Color.White);
        }
    }
}
