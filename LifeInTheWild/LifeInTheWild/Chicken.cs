using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeInTheWild
{
    class Chicken : Entity
    {

        public Chicken(Vector2 pos, string image, int hp) : base(pos, image, hp)
        {
            this.texture = Loader.Images[image];
        }

        public override void Update()
        {
            this.position.X += 1;
            this.position.Y += 1;
        }

    }
}
