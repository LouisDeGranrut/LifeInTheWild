using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeInTheWild
{
    class Wall : Entity
    {
        private Texture2D side;
        private Texture2D front;
        private bool open;

        public Wall(Vector2 pos, string image, string side, int hp) : base(pos, image, hp)
        {
            this.side = Loader.Images[side];
            this.front = Loader.Images[image];
            this.texture = front;
            this.open = true;
        }

        public override void Interact(Player player, Inventaire inventaire, List<Entity> objets)
        {
            this.open = !open;
            if (open)
            {
                this.texture = side;
            }
            else
            {
                this.texture = front;
            }
        }
    }
}
