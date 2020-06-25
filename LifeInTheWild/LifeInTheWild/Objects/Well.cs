using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeInTheWild
{
    class Well : Entity
    {

        public Well(Vector2 pos, string image, int hp) : base(pos, image, hp)
        {

        }

        public override void Interact(Player player, Inventaire inventaire, List<Entity> objets)
        {
            player.addThirst(5);
            base.Interact(player, inventaire, objets);
        }
    }
}
