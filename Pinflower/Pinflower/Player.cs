using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pinflower
{
    class Player
    {
        protected PinflowerGame game;
        public Vector2 Position;
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;
        float playerSpeed = 6.0f;

        Animation runLeftAnim; // only need the left as we're going to flip it for the right
        Animation idleAnim;
        AnimationPlayer sprite;
        int direction;
        string state = "idle";

        public void Initialize(PinflowerGame game, Vector2 position)
        {
            this.game = game;

            Position = position;
            LoadContent("Players");
        }
        
        public void Initialize(PinflowerGame game, Vector2 position, string spriteSet)
        {
            this.game = game;

            Position = position;
            LoadContent(spriteSet);
        }

        public void LoadContent(string spriteSet)
        {
            spriteSet = "Textures/" + spriteSet + "/";
            
            runLeftAnim = new Animation(game.Content.Load<Texture2D>(spriteSet + "RunLeft"), 0.07f, true);
            idleAnim = new Animation(game.Content.Load<Texture2D>(spriteSet + "Idle"), 0.8f, true);
            sprite.PlayAnimation(idleAnim);
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
                this.direction = -1;
            }
            else if (currentKeyboardState.IsKeyDown(Keys.D))
            {
                this.Position.X += this.playerSpeed;
                this.direction = 1;
            }
            else if (currentKeyboardState.IsKeyDown(Keys.A))
            {
                this.Position.X -= this.playerSpeed;
                this.direction = 0;
            }
            else
            {
                this.direction = -1;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (direction == -1) { sprite.PlayAnimation(idleAnim); }
            else {
                sprite.PlayAnimation(runLeftAnim);
            }

            SpriteEffects flip = direction > 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            sprite.Draw(gameTime, spriteBatch, Position, flip);
        }

        /*public int Width
        {
            get { return PlayerTexture.Width; }
        }

        public int Height
        {
            get { return PlayerTexture.Height; }
        }*/
    }
}
