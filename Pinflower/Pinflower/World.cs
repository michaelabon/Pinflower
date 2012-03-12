using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pinflower
{
    public class World
    {
        protected PinflowerGame game;
        protected byte[] levelData;
        protected int height;
        protected int width;
        private const int redMask = 0x00ff0000;
        private const int blueMask = 0x0000ff00;
        private const int greenMask = 0x000000ff;
        protected Rectangle viewport;
        protected Texture2D worldTexture;

        public void Initialize(PinflowerGame _game, Texture2D _texture)
        {
            game = _game;
            worldTexture = _texture;
            //var bmd = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height),
            //System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            //int bufferSize = bmd.Height * bmd.Stride;
            ////create data buffer 
            //levelData = new byte[bufferSize];
            //// copy bitmap data into buffer
            //Marshal.Copy(bmd.Scan0, levelData, 0, levelData.Length);

            //// unlock the bitmap data
            //bmp.UnlockBits(bmd);

            //viewport = new Rectangle(0, 0, PinflowerGame.viewportWidth, PinflowerGame.viewportHeight);
            //worldTexture = GetTexture2DFromBitmap(game.GraphicsDevice, bmp);
        }

        public void Update()
        {
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(worldTexture, new Vector2(0,0), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public bool isCollision(Point point)
        {
            byte levelDatum = levelData[point.Y * width + point.X];
            int red = (levelDatum & redMask);
            int green = (levelDatum & greenMask);
            int blue = (levelDatum & blueMask);
            return (red > 0) || (green > 0) || (blue > 0);
        }

        //public static Texture2D GetTexture2DFromBitmap(GraphicsDevice device, System.Drawing.Bitmap bitmap)
        //{
        //    Texture2D tex = new Texture2D(device, bitmap.Width, bitmap.Height);

        //    System.Drawing.Imaging.BitmapData data = bitmap.LockBits(
        //        new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
        //        System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap.PixelFormat);

        //    int bufferSize = data.Height * data.Stride;

        //    //create data buffer 
        //    byte[] bytes = new byte[bufferSize];

        //    // copy bitmap data into buffer
        //    Marshal.Copy(data.Scan0, bytes, 0, bytes.Length);

        //    // copy our buffer to the texture
        //    tex.SetData(bytes);

        //    // unlock the bitmap data
        //    bitmap.UnlockBits(data);

        //    return tex;
        //}
    }
}
