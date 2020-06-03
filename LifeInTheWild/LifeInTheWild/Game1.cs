using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace LifeInTheWild
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Random rnd = new Random();
        private SpriteFont font;

        private int tileSize = 16;
        private static int mapSize = 50;
        private Texture2D[] floorTiles;
        private Texture2D playerup;
        private Player player;

        private Texture2D arbreTex;
        private Arbre arbre;
        private Arbre arbre2;
        private Arbre arbre3;

        List<Entity> objets = new List<Entity>();

        private int[,] map = new int[mapSize, mapSize];//array de mapSize rangs et mapSize colonnes

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
            font = Content.Load<SpriteFont>("basic");

            floorTiles = new Texture2D[5];
            floorTiles[0] = Content.Load<Texture2D>("grass");
            floorTiles[1] = Content.Load<Texture2D>("grass2");
            floorTiles[2] = Content.Load<Texture2D>("grass3");
            floorTiles[3] = Content.Load<Texture2D>("flowers");

            playerup = Content.Load<Texture2D>("playerup");
            player = new Player(new Vector2(0,0), playerup, 10);

            arbreTex = Content.Load<Texture2D>("tree");
            arbre = new Arbre(new Vector2(rnd.Next(10)*tileSize, rnd.Next(10) * tileSize), arbreTex,10);
            objets.Add(arbre);
            arbre2 = new Arbre(new Vector2(rnd.Next(10) * tileSize, rnd.Next(10) * tileSize), arbreTex, 10);
            objets.Add(arbre2);
            arbre3 = new Arbre(new Vector2(rnd.Next(10) * tileSize, rnd.Next(10) * tileSize), arbreTex, 10);
            objets.Add(arbre3);

            //Charge un tableau 2D et le remplis de valeurs aléatoires
            for (int i = 0; i <= mapSize-1; i++)
            {
                for (int j = 0; j <= mapSize-1; j++)
                {
                    map[i,j] = rnd.Next(0, 4);
                }
            }
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(objets);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);//Couleur d'arrière plan

            spriteBatch.Begin(SpriteSortMode.Immediate, null, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(2f));

            //Affichage du terrain------------------------------------------------------------------------------------------------

            for (int ligne = 0; ligne <= mapSize-1; ligne++)
            {
                for (int colonne = 0; colonne <= mapSize-1; colonne++)
                {
                    int id = map[ligne, colonne];
                    spriteBatch.Draw(floorTiles[id], new Vector2(colonne * tileSize, ligne * tileSize), Color.White);
                }
            }
            foreach (Entity el in objets)//pour tous les objets de la map
            {
                el.Draw(spriteBatch);
            }
            player.Draw(spriteBatch);
            //spriteBatch.DrawString(font, player.getHP().ToString(),player.getPosition(),Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
