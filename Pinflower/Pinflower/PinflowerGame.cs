using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pinflower
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class PinflowerGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        World world;
        public const int viewportWidth = 64 * 4 * 3;
        public const int viewportHeight = 64 * 4 * 3;

        public PinflowerGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = viewportHeight;
            graphics.PreferredBackBufferWidth = viewportWidth;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic 
            player = new Player();
            world = new World();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D texture;
            // Load world
            //System.Drawing.Bitmap bitmap = (System.Drawing.Bitmap)System.Drawing.Bitmap.FromFile(@"Content\Textures\Worlds\descend.bmp");
            texture = Content.Load<Texture2D>(@"Textures\Worlds\descend");
            world.Initialize(this, player, texture);

            // Load player
            Vector2 playerPosition = new Vector2(-texture.Width/2, -texture.Height/2);
            player.Initialize(this, playerPosition);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.DarkSlateGray);

            base.Draw(gameTime);


            spriteBatch.Begin();
            world.Draw(spriteBatch);
            player.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }
    }
}
