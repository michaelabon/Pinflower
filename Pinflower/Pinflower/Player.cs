using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pinflower
{
    public class Player
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

        // Constants for controling horizontal movement
        private const float MoveAcceleration = 13000.0f;
        private const float MaxMoveSpeed = 1750.0f;
        private const float GroundDragFactor = 0.48f;
        private const float AirDragFactor = 0.58f;

        // Constants for controlling vertical movement
        private const float MaxJumpTime = 0.10f;
        private const float JumpLaunchVelocity = -3500.0f;
        private const float GravityAcceleration = 3400.0f;
        private const float MaxFallSpeed = 550.0f;
        private const float JumpControlPower = 0.14f;

        public Vector2 velocity;
        public bool isOnGround;
        // Jumping state
        public bool isJumping;
        private bool wasJumping;
        private float jumpTime;

        /// <summary>
        /// Current user movement input.
        /// </summary>
        private float movement;

        public void Initialize(PinflowerGame game, Vector2 position)
        {
            this.game = game;

            Position = position;
            velocity = Vector2.Zero;
            isJumping = false;
            isOnGround = true;
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
            
            runLeftAnim = new Animation(game.Content.Load<Texture2D>(spriteSet + "RunLeft"), 0.08f, true);
            idleAnim = new Animation(game.Content.Load<Texture2D>(spriteSet + "Idle"), 0.2f, true);
            sprite.PlayAnimation(idleAnim);
        }

        /// <summary>
        /// Updates the player's velocity and position based on input, gravity, etc.
        /// </summary>
        public void ApplyPhysics(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 previousPosition = Position;

            // Base velocity is a combination of horizontal movement control and
            // acceleration downward due to gravity.
            velocity.X += movement * MoveAcceleration * elapsed;
            velocity.Y = MathHelper.Clamp(velocity.Y + GravityAcceleration * elapsed, -MaxFallSpeed, MaxFallSpeed);

            velocity.Y = DoJump(velocity.Y, gameTime);

            // Apply pseudo-drag horizontally.
            if (!isJumping)
                velocity.X *= GroundDragFactor;
            else
                velocity.X *= AirDragFactor;

            // Prevent the player from running faster than his top speed.            
            velocity.X = MathHelper.Clamp(velocity.X, -MaxMoveSpeed, MaxMoveSpeed);

            // Apply velocity.
            Position += velocity * elapsed;
            Position = new Vector2((float)Math.Round(Position.X), (float)Math.Round(Position.Y));

            // If the player is now colliding with the level, separate them.
            //HandleCollisions();

            // If the collision stopped us from moving, reset the velocity to zero.
            if (Position.X == previousPosition.X)
                velocity.X = 0;

            if (Position.Y == previousPosition.Y)
                velocity.Y = 0;
        }

        /// <summary>
        /// Calculates the Y velocity accounting for jumping and
        /// animates accordingly.
        /// </summary>
        /// <remarks>
        /// During the accent of a jump, the Y velocity is completely
        /// overridden by a power curve. During the decent, gravity takes
        /// over. The jump velocity is controlled by the jumpTime field
        /// which measures time into the accent of the current jump.
        /// </remarks>
        /// <param name="velocityY">
        /// The player's current velocity along the Y axis.
        /// </param>
        /// <returns>
        /// A new Y velocity if beginning or continuing a jump.
        /// Otherwise, the existing Y velocity.
        /// </returns>
        private float DoJump(float velocityY, GameTime gameTime)
        {
            // If the player wants to jump
            if (isJumping)
            {
                // Begin or continue a jump
                if ((!wasJumping && isOnGround) || jumpTime > 0.0f)
                {
                    if (jumpTime == 0.0f)
                        //jumpSound.Play();

                    jumpTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    //sprite.PlayAnimation(jumpAnimation);
                }

                // If we are in the ascent of the jump
                if (0.0f < jumpTime && jumpTime <= MaxJumpTime)
                {
                    // Fully override the vertical velocity with a power curve that gives players more control over the top of the jump
                    velocityY = JumpLaunchVelocity * (1.0f - (float)Math.Pow(jumpTime / MaxJumpTime, JumpControlPower));
                }
                else
                {
                    // Reached the apex of the jump
                    jumpTime = 0.0f;
                }
            }
            else
            {
                // Continues not jumping or cancels a jump in progress
                jumpTime = 0.0f;
            }
            wasJumping = isJumping;


            return velocityY;
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
            if ((currentKeyboardState.IsKeyDown(Keys.D) || currentKeyboardState.IsKeyDown(Keys.Right)) 
                && (currentKeyboardState.IsKeyDown(Keys.A) || currentKeyboardState.IsKeyDown(Keys.Left)))
            {
                this.movement = 0;
                this.direction = -1;
            }
            else if (currentKeyboardState.IsKeyDown(Keys.D) || currentKeyboardState.IsKeyDown(Keys.Right))
            {
                this.movement = 1;
                this.direction = 1;
            }
            else if (currentKeyboardState.IsKeyDown(Keys.A) || currentKeyboardState.IsKeyDown(Keys.Left))
            {
                this.movement = -1;
                this.direction = 0;
            }
            else
            {
                this.movement = 0;
                this.direction = -1;
            }

            if (currentKeyboardState.IsKeyDown(Keys.W) || currentKeyboardState.IsKeyDown(Keys.Space))
            {
                this.isJumping = true;
            }
            else
                this.isJumping = false;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (direction == -1) { sprite.PlayAnimation(idleAnim); }
            else {
                sprite.PlayAnimation(runLeftAnim);
            }

            SpriteEffects flip = direction > 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            sprite.Draw(gameTime, spriteBatch, new Vector2(PinflowerGame.viewportWidth / 2, PinflowerGame.viewportHeight / 2), flip);
        }
    
        public int Width
        {
            get { return sprite.Animation.FrameWidth; }
        }

        public int Height
        {
            get { return sprite.Animation.FrameWidth; }
        }
    }
}
