using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeInTheWild
{
    class Chicken : Creature
    {
        private Texture2D right;
        private Texture2D left;
        private Texture2D down;
        private Texture2D up;
        public Chicken(Vector2 pos, string image, string right,string up, string down, int hp) : base(pos, image, hp)
        {
            this.right = Loader.Images[right];
            this.left = Loader.Images[image];
            this.up = Loader.Images[up];
            this.down = Loader.Images[down];
        }

        public override void Update(List<Entity> objets)
        {
            Random rnd = new Random();
            int dir = rnd.Next(0,4);

            switch (dir)
            {
                case 0:
                    xDir = speed;
                    this.texture = left;
                    break;
                case 1:
                    xDir -= speed;
                    this.texture = right;
                    break;
                case 2:
                    yDir = speed;
                    this.texture = down;
                    break;
                case 3:
                    yDir = -speed;
                    this.texture = up;
                    break;
                case 4:
                    xDir = 0;
                    yDir = 0;
                    break;
            }

            base.Update(objets);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, new Vector2(this.position.X, this.position.Y), Color.White);
        }

    }
}
