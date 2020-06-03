using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LifeInTheWild
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private int tileSize = 16;

        private Texture2D grass;
        private Texture2D flatrock;
        private Texture2D playerup;
        private SpriteFont font;
        Player player;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            grass = Content.Load<Texture2D>("grass");
            flatrock = Content.Load<Texture2D>("flatrock");
            playerup = Content.Load<Texture2D>("playerup");
            player = new Player(new Vector2(0,0), playerup, 10);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            for(int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    spriteBatch.Draw(grass, new Vector2(i*tileSize, j*tileSize), Color.White);
                }
            }
            spriteBatch.Draw(flatrock, new Vector2(2* tileSize, 2* tileSize), Color.White);
            player.Draw(spriteBatch);
            //spriteBatch.DrawString(font, "cul",new Vector2(10,10),Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
