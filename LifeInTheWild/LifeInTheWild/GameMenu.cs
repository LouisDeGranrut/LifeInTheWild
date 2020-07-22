using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeInTheWild
{
    class GameMenu : Menu
    {
        Random rnd = new Random();//générateur de nombres aléatoire
        private SpriteFont font;//police d'écriture du jeu
        private static Inventaire inventaire;//interface inventaire
        private static Crafting crafting;//interface de craft
        private static AffichagePancarte affPancarte;//interface des pancartes
        private Texture2D[] floorTiles;//tableau contenant toutes les tiles de sol
        private Camera camera;
        private bool debug;//mode debug
        private Terrain terrain;//la map

        //Les créatures en jeu
        private Player player;
        private Chicken chicken;

        //Textures 
        private Texture2D rectTex;
        private Texture2D heart;
        private Texture2D water;
        private Texture2D bread;

        //Liste contenant tous les objets du jeu (sert aux collisions)
        List<Entity> objets = new List<Entity>();

        public GameMenu() : base()
        {
            font = Loader.Fonts["basic"];
            debug = false;

            floorTiles = new Texture2D[6];
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
            bread = Loader.Images["bread"];

            player = new Player(new Vector2(256, 256), 100, "playerup");
            objets.Add(new Pancarte(new Vector2(14*16, 14*16), "panneau", 3, "Bienvenu sur Life In The Wild, il semblerait que vous ayez\n" +
                "atterit dans un terriroire inconnu. Avant de mourrir de faim,\nil est suggere de partir recuperer du bois et de la pierre\npour construire un abris et" +
                "de planter des graines pour\npouvoir subvenir a vos besoins." +
                " Les touches I et C de votre\nclavier devraient vous interesser...\nBonne Chance"));
            camera = new Camera();
            chicken = new Chicken(new Vector2(256 + 16, 256 + 16), "chicken_left", 10);

            inventaire = new Inventaire();
            crafting = new Crafting(player);
            affPancarte = new AffichagePancarte();

            terrain = new Terrain(50, floorTiles);
            int mapSize = terrain.getMapSize();

            //fais apparaitre 70 arbres, buissons, cailloux...
            for (int i = 0; i <= 70; i++)
            {
                objets.Add(new Arbre(new Vector2(rnd.Next(mapSize-1) * 16, rnd.Next(mapSize-1) * 16), "bush", 3));
                objets.Add(new Rock(new Vector2(rnd.Next(mapSize-1) * 16, rnd.Next(mapSize-1) * 16), "rocks", 3));
                objets.Add(new Arbre(new Vector2(rnd.Next(mapSize-1) * 16, rnd.Next(mapSize-1) * 16), "tree", 3));
                objets.Add(new Arbre(new Vector2(rnd.Next(mapSize-1) * 16, rnd.Next(mapSize-1) * 16), "sapin", 3));
                objets.Add(new Vegetable(new Vector2(rnd.Next(mapSize-1) * 16, rnd.Next(mapSize-1) * 16),"seed", "crop", 3));
            }

            //Géneration de barrieres autour du terrain
            for(int i = 0; i < 50; i++)
            {
                objets.Add(new Arbre(new Vector2(i * 16, 0), "tree", 900));
                objets.Add(new Arbre(new Vector2(0, i * 16), "tree", 900));
                objets.Add(new Arbre(new Vector2(i * 16, 50 * 16), "tree", 900));
                objets.Add(new Arbre(new Vector2(50 * 16, i * 16), "tree", 900));
            }

            //Vérifie qu'aucune entité n'est sur le joueur
            for (int i = 0; i < objets.Count; i++)
            {
                if (objets[i].getPosition() == player.getPosition())
                {
                    objets[i].Destroy(inventaire, objets, objets[i]);
                    DebugConsole.addLine("Destroying: " + objets[i]);
                }
            }
        }

        public override void Update(GameTime time)
        {
            player.Update(objets, terrain.getMapData(), inventaire, crafting, affPancarte);
            camera.Follow(player);
            crafting.Update();
            inventaire.Update();
            chicken.Update(objets);

            if (Keyboard.GetState().IsKeyDown(Keys.F1))
            {
                debug = !debug;
            }

            //Vérifie chaque entitée et la détruit si elle n'a plus de vie
            for (int i = 0; i < objets.Count; i++)
            {
                if(objets[i] is Vegetable)
                {
                    objets[i].Update();//performance heavy !!
                }
                if (objets[i].getHP() <= 0)//si l'entité n'a plus de hp
                {
                    DebugConsole.addLine("Destruction de:" + objets[i]);
                    objets[i].Destroy(inventaire, objets, objets[i]);
                    Loader.Sounds["destroy"].Play();
                }
            }

            if (player.getHP() <= 0)
            {
                DebugConsole.addLine("Vous avez perdu la partie, vous etes mort");
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, null, SamplerState.PointClamp, null, null, null, camera.Transform * Matrix.CreateScale(2f));
            terrain.Draw(spriteBatch);

            foreach (Entity el in objets)//pour tous les objets de la map--------------------------------------------------------------------------------------
            {
                el.Draw(spriteBatch);

                if (debug)//affiche la console uniquement en mode debug
                {
                    spriteBatch.Draw(rectTex, new Vector2((int)el.getPosition().X, (int)el.getPosition().Y), Color.Fuchsia);
                }
            }

            player.Draw(spriteBatch);
            chicken.Draw(spriteBatch);
            double posX = Math.Round((player.getPosition().X + (player.getDir().X * 16)) / 16);
            double posY = Math.Round((player.getPosition().Y + (player.getDir().Y * 16)) / 16);
            spriteBatch.Draw(rectTex, new Vector2((int)posX * 16, (int)posY * 16), Color.Fuchsia);
            spriteBatch.End();

            //nouvelle spritebatch pour l'interface------------------------------------------------------------------------------------------------------------
            spriteBatch.Begin(SpriteSortMode.Immediate, null, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(1f));

            for (int i = 0; i <= player.getHP() / 10; i++)
            {spriteBatch.Draw(heart, new Vector2((18 * i) + 10, 0), Color.White);}

            for (int i = 0; i <= player.getHunger() / 10; i++)
            {spriteBatch.Draw(bread, new Vector2((18 * i) + 10, 25), Color.White);}

            for (int i = 0; i <= player.getThirst() / 10; i++)
            {spriteBatch.Draw(water, new Vector2((18 * i) + 10, 50), Color.White);}

            if (debug)//affiche la console uniquement en mode debug
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
