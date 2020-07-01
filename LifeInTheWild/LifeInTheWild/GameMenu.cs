using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeInTheWild
{
    class GameMenu : Menu
    {

        Random rnd = new Random();//le générateur de nombres aléatoire
        private SpriteFont font;//la police d'écriture du jeu
        private static Inventaire inventaire;//inventaire du jeu
        private static Crafting crafting;
        private static AffichagePancarte affPancarte;
        private int tileSize = 16;//la taille des images du jeu (en pixels)
        private static int mapSize = 50;//la taille de la map
        private Texture2D[] floorTiles;//tableau contenant toutes les tiles de sol
        private Camera camera;//la caméra du jeu

        //Les objets du jeu
        private Player player;
        private Chicken chicken;

        private Texture2D rectTex;
        private Texture2D heart;
        private Texture2D water;

        //Liste contenant tous les objets du jeu (sert aux collisions)
        List<Entity> objets = new List<Entity>();

        //Tableau de mapSize rangs et mapSize colonnes (représente la map)
        private int[,] map = new int[mapSize, mapSize];

        public GameMenu() : base()
        {
            font = Loader.Fonts["basic"];

            inventaire = new Inventaire();
            crafting = new Crafting();
            affPancarte = new AffichagePancarte();

            floorTiles = new Texture2D[10];
            floorTiles[0] = Loader.Images["grass"];
            floorTiles[1] = Loader.Images["grass2"];
            floorTiles[2] = Loader.Images["grass3"];
            floorTiles[3] = Loader.Images["flowers"];
            floorTiles[4] = Loader.Images["dirt"];
            floorTiles[5] = Loader.Images["woodTile"];

            DebugConsole.addLine("   -Debug Console-:");
            rectTex = Loader.Images["rect"];
            heart = Loader.Images["heart"];
            water = Loader.Images["water"];

            player = new Player(new Vector2(256, 256), 100, "playerup");
            camera = new Camera();
            chicken = new Chicken(new Vector2(256 + 16, 256 + 16), "chicken_left", 10);

            //fais apparaitre 75 arbres, buissons, cailloux...
            for (int i = 0; i <= 75; i++)
            {
                objets.Add(new Arbre(new Vector2(rnd.Next(mapSize) * tileSize, rnd.Next(mapSize) * tileSize), "bush", 3));
                objets.Add(new Rock(new Vector2(rnd.Next(mapSize) * tileSize, rnd.Next(mapSize) * tileSize), "rocks", 3));
                objets.Add(new Arbre(new Vector2(rnd.Next(mapSize) * tileSize, rnd.Next(mapSize) * tileSize), "tree", 3));
                objets.Add(new Arbre(new Vector2(rnd.Next(mapSize) * tileSize, rnd.Next(mapSize) * tileSize), "sapin", 3));
                objets.Add(new Vegetable(new Vector2(rnd.Next(mapSize) * tileSize, rnd.Next(mapSize) * tileSize), "crop", 3));
                objets.Add(new Well(new Vector2(rnd.Next(mapSize) * tileSize, rnd.Next(mapSize) * tileSize), "well", 3));
                objets.Add(new Pancarte(new Vector2(rnd.Next(mapSize) * tileSize, rnd.Next(mapSize) * tileSize), "panneau", 3, "Bienvenu sur Life In The Wild, il semblerait que vous ayez\n" +
                "atterit dans un terriroire inconnu. Avant de mourrir de faim,\nil est suggere de partir recuperer du bois et de la pierre\npour construire un abris et" +
                "de planter des graines pour\npouvoir subvenir a vos besoins." +
                " Les touches I et C de votre\nclavier devraient vous interesser...\nBonne Chance"));
                objets.Add(new Door(new Vector2(rnd.Next(50) * tileSize, rnd.Next(50) * tileSize), "door", "door_open", 10));
            }

            //Charge un tableau 2D et le remplis de valeurs aléatoires (Map)//---------------------------------------------------------------------------------
            for (int i = 0; i <= mapSize - 1; i++)
            {
                for (int j = 0; j <= mapSize - 1; j++)
                {
                    map[i, j] = rnd.Next(0, 4);
                }
            }
        }

        public override void Update(GameTime time)
        {
            player.Update(objets, map, inventaire, crafting, affPancarte);
            camera.Follow(player);
            crafting.Update();
            inventaire.Update();
            chicken.Update(objets);

            for (int i = 0; i < objets.Count; i++)//pour toutes les entites
            {
                //objets[i].Update();//la mettre à jour
                if (objets[i].getHP() <= 0)//si l'entité n'a plus de hp
                {
                    objets[i].Destroy(inventaire, objets, objets[i]);
                    Loader.Sounds["destroy"].Play();
                    //DebugConsole.addLine("Destroying: " + objets[i]);
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //GraphicsDevice.Clear(Color.Black);//Couleur d'arrière plan
            spriteBatch.Begin(SpriteSortMode.Immediate, null, SamplerState.PointClamp, null, null, null, camera.Transform * Matrix.CreateScale(2f));

            //Affichage du terrain-----------------------------------------------------------------------------------------------------------------------------

            for (int ligne = 0; ligne <= mapSize - 1; ligne++)
            {
                for (int colonne = 0; colonne <= mapSize - 1; colonne++)
                {
                    if (player.getPosition().X < colonne * tileSize + 1224 &&
                        player.getPosition().X + 224 > colonne * tileSize &&
                        player.getPosition().Y < ligne * tileSize + 1176 &&
                        player.getPosition().Y + 176 > ligne * tileSize)
                    {
                        int id = map[ligne, colonne];
                        spriteBatch.Draw(floorTiles[id], new Vector2(colonne * tileSize, ligne * tileSize), Color.White);
                    }
                }
            }

            foreach (Entity el in objets)//pour tous les objets de la map--------------------------------------------------------------------------------------
            {
                //if (player.getPosition().X < el.getPosition().X + 1224 && player.getPosition().X + 1224 > el.getPosition().X && player.getPosition().Y < el.getPosition().Y + 176 && player.getPosition().Y + 176 > el.getPosition().Y)
                //{
                el.Draw(spriteBatch);
                //spriteBatch.Draw(rectTex, new Vector2((int)el.getPosition().X, (int)el.getPosition().Y), Color.Fuchsia);
                //}
            }

            player.Draw(spriteBatch);
            chicken.Draw(spriteBatch);
            double posX = Math.Round((player.getPosition().X + (player.getDir().X * 16)) / 16);
            double posY = Math.Round((player.getPosition().Y + (player.getDir().Y * 16)) / 16);
            spriteBatch.Draw(rectTex, new Vector2((int)posX * 16, (int)posY * 16), Color.Fuchsia);
            spriteBatch.End();

            //nouvelle spritebatch pour l'interface------------------------------------------------------------------------------------------------------------
            spriteBatch.Begin(SpriteSortMode.Immediate, null, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(1f));
            spriteBatch.DrawString(font, "HP: " + player.getHP().ToString(), new Vector2(10, 10), Color.Red);
            spriteBatch.DrawString(font, "Faim: " + player.getHunger().ToString(), new Vector2(10, 25), Color.Red);
            spriteBatch.DrawString(font, "Soif: " + player.getThirst().ToString(), new Vector2(10, 40), Color.Red);
            spriteBatch.DrawString(font, "Outils: " + player.getOutil().ToString(), new Vector2(10, 55), Color.Red);
            spriteBatch.DrawString(font, ("Player Pos: " + player.getPosition().X) + " " + (player.getPosition().Y), new Vector2(10, 70), Color.Red);
            spriteBatch.DrawString(font, "Player Map Pos: " + Math.Round(player.getPosition().X / tileSize) + " " + Math.Round(player.getPosition().Y / tileSize), new Vector2(10, 85), Color.Red);
            spriteBatch.DrawString(font, "Objet Count: " + objets.Count, new Vector2(10, 115), Color.Red);
            spriteBatch.DrawString(font, "Inventory Size: " + inventaire.Size(), new Vector2(10, 130), Color.Red);

            for (int i = 0; i <= player.getHP() / 10; i++)
            {
                spriteBatch.Draw(heart, new Vector2((18 * i) + 150, 0), Color.Fuchsia);
            }

            for (int i = 0; i <= player.getHunger() / 10; i++)
            {
                spriteBatch.Draw(rectTex, new Vector2((18 * i) + 150, 25), Color.Fuchsia);
            }

            for (int i = 0; i <= player.getThirst() / 10; i++)
            {
                spriteBatch.Draw(water, new Vector2((18 * i) + 150, 50), Color.Fuchsia);
            }

            DebugConsole.Draw(spriteBatch, font, new Vector2(10, 145));
            if (inventaire.isActive)
                inventaire.Draw(spriteBatch, font);
            if (affPancarte.isActive)
                affPancarte.Draw(spriteBatch, font);
            if (crafting.isActive)
                crafting.Draw(spriteBatch, font);
            spriteBatch.End();

        }

    }
}
