using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeInTheWild
{
    class Arbre : Entity
    {
        public Arbre(Vector2 pos, string image, int hp) : base(pos, image, hp)
        {
            this.texture = Loader.Images[image];
        }

        public override void Destroy(Inventaire inventaire, List<Entity> objets, Entity entity)
        {
            inventaire.AddItem(new Item("bois", 1));
            base.Destroy(inventaire, objets, entity);
        }

    }
}
