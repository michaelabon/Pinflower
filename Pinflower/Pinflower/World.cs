using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pinflower
{
    public class World
    {
        protected PinflowerGame game;
        protected Player player;
        protected byte[] levelData;
        protected int height;
        protected int width;
        private const int redMask = 0x00ff0000;
        private const int blueMask = 0x0000ff00;
        private const int greenMask = 0x000000ff;
        protected Rectangle viewport;
        protected Texture2D worldTexture;

        public void Initialize(PinflowerGame _game, Player _player, Texture2D _texture)
        {
            game = _game;
            worldTexture = _texture;
            player = _player;
            viewport = new Rectangle(0, 0, PinflowerGame.viewportWidth, PinflowerGame.viewportHeight);
        }

        public void Update()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(worldTexture, new Vector2(-player.Position.X, -player.Position.Y), Color.White);
        }

        public bool isCollision(Point point)
        {
            byte levelDatum = levelData[point.Y * width + point.X];
            int red = (levelDatum & redMask);
            int green = (levelDatum & greenMask);
            int blue = (levelDatum & blueMask);
            return (red > 0) || (green > 0) || (blue > 0);
        }
    }
}
