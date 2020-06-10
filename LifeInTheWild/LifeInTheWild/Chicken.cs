using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeInTheWild
{
    class Chicken : Creature
    {

        public Chicken(Vector2 pos, string image, int hp) : base(pos, image, hp)
        {
            this.texture = Loader.Images[image];
        }

        public override void Update(List<Entity> objets)
        {
            Random rnd = new Random();
            int dir = rnd.Next(0,4);

            switch (dir)
            {
                case 0:
                    xDir = speed;
                    break;
                case 1:
                    xDir -= speed;
                    break;
                case 2:
                    yDir = speed;
                    break;
                case 3:
                    yDir = -speed;
                    break;
                case 4:
                    xDir = 0;
                    yDir = 0;
                    break;
            }

            base.Update(objets);
        }

    }
}
