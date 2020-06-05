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
        SpriteBatch spriteBatch;//permet d'afficher les assets du jeu
        public static int screenWidth, screenHeight;//la taille de l'écran
        Random rnd = new Random();//le générateur de nombres aléatoire
        private SpriteFont font;//la police d'écriture du jeu

        private int tileSize = 16;//la taille des images du jeu (en pixels)
        private static int mapSize = 50;//la taille de la map
        private Texture2D[] floorTiles;//tableau contenant toutes les tiles de sol
        private Camera camera;//la caméra du jeu

        private Texture2D playerup;
        private Texture2D playerdown;
        private Texture2D playerleft;
        private Texture2D playerright;
        private Player player;

        //Les images des objets du jeu
        private Texture2D arbreTex;
        private Texture2D sapinTex;
        private Texture2D bushTex;
        private Texture2D potTex;
        private Texture2D doorTex;
        private Texture2D chestTex;
        private Texture2D rockTex;
        private Texture2D attaqueTex;

        //Les objets du jeu
        private Arbre arbre;

        List<Entity> objets = new List<Entity>();//Liste contenant tous les objets du jeu (sert aux collisions)

        private int[,] map = new int[mapSize, mapSize];//tableau de mapSize rangs et mapSize colonnes

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            screenWidth = graphics.PreferredBackBufferWidth = 920;
            screenHeight = graphics.PreferredBackBufferHeight = 680;
            graphics.IsFullScreen = false;
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

            floorTiles = new Texture2D[10];
            floorTiles[0] = Content.Load<Texture2D>("grass");
            floorTiles[1] = Content.Load<Texture2D>("grass2");
            floorTiles[2] = Content.Load<Texture2D>("grass3");
            floorTiles[3] = Content.Load<Texture2D>("flowers");
            floorTiles[4] = Content.Load<Texture2D>("dirt");

            arbreTex = Content.Load<Texture2D>("tree");
            attaqueTex = Content.Load<Texture2D>("attaque");
            sapinTex = Content.Load<Texture2D>("sapin");
            potTex = Content.Load<Texture2D>("pot");
            bushTex = Content.Load<Texture2D>("bush");
            doorTex = Content.Load<Texture2D>("door");
            chestTex = Content.Load<Texture2D>("chest");
            rockTex = Content.Load<Texture2D>("rocks");

            playerup = Content.Load<Texture2D>("playerup");
            playerdown = Content.Load<Texture2D>("playerdown");
            playerleft = Content.Load<Texture2D>("playerleft");
            playerright = Content.Load<Texture2D>("playerright");
            player = new Player(new Vector2(512, 512), 10, playerup, playerdown, playerleft, playerright, attaqueTex);
            camera = new Camera();


            for (int i = 0; i <= 50; i++)
            {
                objets.Add(new Arbre(new Vector2(rnd.Next(50) * tileSize, rnd.Next(50) * tileSize), rockTex, 10));
                objets.Add(new Arbre(new Vector2(rnd.Next(50) * tileSize, rnd.Next(50) * tileSize), arbreTex, 10));
                objets.Add(new Arbre(new Vector2(rnd.Next(50) * tileSize, rnd.Next(50) * tileSize), sapinTex, 10));
                objets.Add(new Arbre(new Vector2(rnd.Next(50) * tileSize, rnd.Next(50) * tileSize), bushTex, 10));
                objets.Add(new Arbre(new Vector2(rnd.Next(50) * tileSize, rnd.Next(50) * tileSize), potTex, 10));
            }
            objets.Add(new Arbre(new Vector2(rnd.Next(50) * tileSize, rnd.Next(50) * tileSize), doorTex, 10));
            objets.Add(new Arbre(new Vector2(rnd.Next(50) * tileSize, rnd.Next(50) * tileSize), chestTex, 10));

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

            player.Update(objets, map);
            camera.Follow(player);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);//Couleur d'arrière plan

            spriteBatch.Begin(SpriteSortMode.Immediate, null, SamplerState.PointClamp, null, null, null, camera.Transform*Matrix.CreateScale(2f));

            //Affichage du terrain------------------------------------------------------------------------------------------------

            for (int ligne = 0; ligne <= mapSize-1; ligne++)
            {
                for (int colonne = 0; colonne <= mapSize-1; colonne++)
                {
                    if (player.getPosition().X < colonne*tileSize + 224 && player.getPosition().X + 224 > colonne * tileSize && player.getPosition().Y < ligne * tileSize + 224 && player.getPosition().Y + 224 > ligne * tileSize)
                    {
                        int id = map[ligne, colonne];
                        spriteBatch.Draw(floorTiles[id], new Vector2(colonne * tileSize, ligne * tileSize), Color.White);
                    }
                }
            }
            foreach (Entity el in objets)//pour tous les objets de la map
            {
                if (player.getPosition().X < el.getPosition().X + 224 && player.getPosition().X + 224 > el.getPosition().X && player.getPosition().Y < el.getPosition().Y + 224 && player.getPosition().Y + 224 > el.getPosition().Y)
                {
                    el.Draw(spriteBatch);
                }
            }

            player.Draw(spriteBatch);
            spriteBatch.End();

            //spritebatch pour l'interface--------------------------------------------------------------------------------------------------------
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "HP: "+player.getHP().ToString(), new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(font, "Outils: " + player.getOutil().ToString(), new Vector2(10, 25), Color.White);
            spriteBatch.DrawString(font, "Wood: ", new Vector2(10, 40), Color.White);
            spriteBatch.DrawString(font, "Stone: ", new Vector2(10, 55), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
