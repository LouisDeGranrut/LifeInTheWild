using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeInTheWild
{
    class Rock : Entity
    {

        public Rock(Vector2 pos, string image, int hp) : base(pos, image, hp)
        {
            this.texture = Loader.Images[image];
        }

        public override void Destroy(Inventaire inventaire)
        {
            inventaire.AddItem(new Item("rock", 1));
        }

    }
}
