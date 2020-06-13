using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;

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
        private SoundEffect playerHit;
        private SoundEffect playerMow;
        private Camera camera;//la caméra du jeu
        private Loader loader;//gère les assets du jeu

        //Les objets du jeu
        private Player player;
        private Chicken chicken;

        private Texture2D rectTex;

        //Liste contenant tous les objets du jeu (sert aux collisions)
        List<Entity> objets = new List<Entity>();

        //Tableau de mapSize rangs et mapSize colonnes (représente la map)
        private int[,] map = new int[mapSize, mapSize];

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
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("basic");
            Loader.LoadImages(this.Content);
            Loader.LoadAudio(this.Content);

            playerHit = Loader.Sounds["hit"];
            playerMow = Loader.Sounds["mow"];

            floorTiles = new Texture2D[10];
            floorTiles[0] = Loader.Images["grass"];
            floorTiles[1] = Loader.Images["grass2"];
            floorTiles[2] = Loader.Images["grass3"];
            floorTiles[3] = Loader.Images["flowers"];
            floorTiles[4] = Loader.Images["dirt"];
            floorTiles[5] = Loader.Images["woodTile"];

            DebugConsole.addLine("   -Debug Console-:");
            rectTex = Loader.Images["rect"];

            player = new Player(new Vector2(512, 512), 10, "playerup", "playerdown", "playerleft", "playerright", playerHit, playerMow);
            camera = new Camera();
            chicken = new Chicken(new Vector2(512 + 16, 512 + 16), "chicken_left","chicken_right", "chicken_up", "chicken_down", 10);

            //fais apparaitre 75 arbres, buissons, cailloux...
            for (int i = 0; i <= 75; i++)
            {
                objets.Add(new Arbre(new Vector2(rnd.Next(50) * tileSize, rnd.Next(50) * tileSize), "bush", 3));
                objets.Add(new Rock(new Vector2(rnd.Next(50) * tileSize, rnd.Next(50) * tileSize), "rocks", 3));
                objets.Add(new Arbre(new Vector2(rnd.Next(50) * tileSize, rnd.Next(50) * tileSize), "tree", 3));
                objets.Add(new Arbre(new Vector2(rnd.Next(50) * tileSize, rnd.Next(50) * tileSize), "sapin", 3));

                //objets.Add(new Arbre(new Vector2(rnd.Next(50) * tileSize, rnd.Next(50) * tileSize), "crop", 10));
                //objets.Add(new Arbre(new Vector2(rnd.Next(50) * tileSize, rnd.Next(50) * tileSize), "campfire", 10));
                //objets.Add(new Arbre(new Vector2(rnd.Next(50) * tileSize, rnd.Next(50) * tileSize), "door", 10));
                //objets.Add(new Arbre(new Vector2(rnd.Next(50) * tileSize, rnd.Next(50) * tileSize), "door", 10));
                //objets.Add(new Arbre(new Vector2(rnd.Next(50) * tileSize, rnd.Next(50) * tileSize), "chest", 10));
            }

            //Charge un tableau 2D et le remplis de valeurs aléatoires (Map)
            for (int i = 0; i <= mapSize-1; i++)
            {
                for (int j = 0; j <= mapSize-1; j++)
                {
                    map[i,j] = rnd.Next(0, 4);
                }
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(objets, map);
            camera.Follow(player);

            for(int i = 0; i<objets.Count;i++)//pour toutes les entites
            {
                //objets[i].Update();//la mettre à jour
                if (objets[i].getHP() <= 0)//si l'entité n'a plus de hp
                {
                    objets[i].Destroy(player);
                    Loader.Sounds["destroy"].Play();
                    DebugConsole.addLine("Destroying: " + objets[i]);
                    objets.Remove(objets[i]);//la retirer de la liste
                }
            }

            chicken.Update(objets);//met a jour la poule
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);//Couleur d'arrière plan
            spriteBatch.Begin(SpriteSortMode.Immediate, null, SamplerState.PointClamp, null, null, null, camera.Transform * Matrix.CreateScale(2f));

            //Affichage du terrain------------------------------------------------------------------------------------------------

            for (int ligne = 0; ligne <= mapSize-1; ligne++)
            {
                for (int colonne = 0; colonne <= mapSize-1; colonne++)
                {
                    if (player.getPosition().X < colonne*tileSize + 224 && player.getPosition().X + 224 > colonne * tileSize && player.getPosition().Y < ligne * tileSize + 176 && player.getPosition().Y + 176 > ligne * tileSize)
                    {
                        int id = map[ligne, colonne];
                        spriteBatch.Draw(floorTiles[id], new Vector2(colonne * tileSize, ligne * tileSize), Color.White);
                    }
                }
            }

            foreach (Entity el in objets)//pour tous les objets de la map ("""optimisation pas indispensable""")
            {
                if (player.getPosition().X < el.getPosition().X + 224 && player.getPosition().X + 224 > el.getPosition().X && player.getPosition().Y < el.getPosition().Y + 176 && player.getPosition().Y + 176 > el.getPosition().Y)
                {
                    el.Draw(spriteBatch);
                    //spriteBatch.Draw(rectTex, new Vector2((int)el.getPosition().X, (int)el.getPosition().Y), Color.Fuchsia);
                }
            }

            player.Draw(spriteBatch);
            chicken.Draw(spriteBatch);
            double posX = Math.Round((player.getPosition().X + (player.getDir().X * 16)) / 16);
            double posY = Math.Round((player.getPosition().Y + (player.getDir().Y * 16)) / 16);
            spriteBatch.Draw(rectTex, new Vector2((int)posX *16,(int)posY*16), Color.Fuchsia);

            spriteBatch.End();

            //nouvelle spritebatch pour l'interface-----------------------------------------------------------------------------------------------
            spriteBatch.Begin(SpriteSortMode.Immediate, null, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(1f));
            spriteBatch.DrawString(font, "HP: "+player.getHP().ToString(), new Vector2(10, 10), Color.LightGreen);
            spriteBatch.DrawString(font, "Outils: " + player.getOutil().ToString(), new Vector2(10, 25), Color.LightGreen);
            spriteBatch.DrawString(font, "Wood: " + player.getWood(), new Vector2(10, 40), Color.LightGreen);
            spriteBatch.DrawString(font, "Rocks: " + player.getRock(), new Vector2(10, 55), Color.LightGreen);
            spriteBatch.DrawString(font, ("Player Pos: " + player.getPosition().X) + " " + (player.getPosition().Y), new Vector2(10, 70), Color.LightGreen);
            spriteBatch.DrawString(font, "Player Map Pos: " + Math.Round(player.getPosition().X / tileSize) + " " + Math.Round(player.getPosition().Y / tileSize), new Vector2(10, 85), Color.LightGreen);
            spriteBatch.DrawString(font, "Player Dir: " + player.getDir().X + " " + player.getDir().Y, new Vector2(10, 100), Color.LightGreen);
            spriteBatch.DrawString(font, "Objet Count: " + objets.Count, new Vector2(10, 115), Color.LightGreen);
            DebugConsole.Draw(spriteBatch, font, new Vector2(10,140));
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
