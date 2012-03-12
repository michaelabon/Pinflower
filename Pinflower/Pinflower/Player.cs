using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pinflower
{
    class Player
    {
        protected PinflowerGame game;
        public Vector2 Position;
        public Texture2D PlayerTexture;
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;
        float playerSpeed = 8.0f;

        public void Initialize(PinflowerGame game, Texture2D texture, Vector2 position)
        {
            this.game = game;
            PlayerTexture = texture;
            Position = position;
        }

        public void Update(GameTime gameTime)
        {
            // Save the previous state of the keyboard and game pad so we can determine single key/button presses
            previousKeyboardState = currentKeyboardState;

            // Read the current state of the keyboard and gamepad and store it
            currentKeyboardState = Keyboard.GetState();

            // Allows the game to exit
            if (currentKeyboardState.IsKeyDown(Keys.Escape)) { game.Exit(); }

            // GAME LOGICf!~
            if (currentKeyboardState.IsKeyDown(Keys.D) && currentKeyboardState.IsKeyDown(Keys.A))
            {
                // do nothing
            }
            else if (currentKeyboardState.IsKeyDown(Keys.D))
            {
                this.Position.X += this.playerSpeed;
            }
            else if (currentKeyboardState.IsKeyDown(Keys.A))
            {
                this.Position.X -= this.playerSpeed;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(PlayerTexture, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public int Width
        {
            get { return PlayerTexture.Width; }
        }

        public int Height
        {
            get { return PlayerTexture.Height; }
        }
    }
}
