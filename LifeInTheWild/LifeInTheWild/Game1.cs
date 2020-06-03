using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace LifeInTheWild
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private int tileSize = 16;
        private int mapSize = 50;

        Random rnd = new Random();
        private Texture2D[] floorTiles;
        private Texture2D flatrock;
        private Texture2D playerup;
        private SpriteFont font;
        private int[,] map = {
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {1,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {1,0,0,0,0,0,0,0}
        };
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

            floorTiles = new Texture2D[5];
            floorTiles[0] = Content.Load<Texture2D>("grass");
            floorTiles[1] = Content.Load<Texture2D>("grass2");
            floorTiles[2] = Content.Load<Texture2D>("flowers");

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
            GraphicsDevice.Clear(Color.Black);//Couleur d'arrière plan

            spriteBatch.Begin(SpriteSortMode.Immediate, null, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(2f));

            //Affichage du terrain------------------------------------------------------------------------------------------------

            for (int ligne = 0; ligne <= 7; ligne++)
            {
                for (int colonne = 0; colonne <= 7; colonne++)
                {
                    int id = map[ligne, colonne];
                    if (id == 0)//si on trouve un 0 dans le tableau on affiche de l'herbe
                    {
                        spriteBatch.Draw(floorTiles[rnd.Next(0, 3)], new Vector2(colonne * tileSize, ligne * tileSize), Color.White);
                    }
                    if (id == 1)//si on trouve un 1 dans le tableau on affiche de la pierre
                    {
                        spriteBatch.Draw(flatrock, new Vector2(colonne * tileSize, ligne * tileSize), Color.White);
                    }
                }
            }
            player.Draw(spriteBatch);
            //spriteBatch.DrawString(font, "cul",new Vector2(10,10),Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
