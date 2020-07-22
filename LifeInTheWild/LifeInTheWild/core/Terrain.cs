using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeInTheWild
{
    class Terrain
    {

        private int size;
        private int[,] data;
        private Texture2D[] tiles;
        private Random rnd = new Random();

        //Constructeur
        public Terrain(int size, Texture2D[] list)
        {
            this.size = size;
            data = new int[size, size];
            Generate(data,size);
            tiles = list;
        }

        //Remplis le tableau "data" par des valeurs aléatoires
        public void Generate(int[,] data, int size)
        {
            for (int i = 0; i <= size - 1; i++)
            {
                for (int j = 0; j <= size - 1; j++)
                {
                    data[i, j] = rnd.Next(0, 4);
                }
            }
        }

        //Parcours le tableau "data" et affiche une image en fonction de la valeur trouvée
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int ligne = 0; ligne <= size - 1; ligne++)
            {
                for (int colonne = 0; colonne <= size - 1; colonne++)
                {
                    int id = data[ligne, colonne];
                    spriteBatch.Draw(tiles[id], new Vector2(colonne * 16, ligne * 16), Color.White);
                }
            }
        }

        //Getters Setters
        public int[,] getMapData()
        {
            return this.data;
        }

        public int getMapSize()
        {
            return this.size;
        }
    }
}
