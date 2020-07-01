using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeInTheWild
{
    class Vegetable : Entity
    {

        private float age;

        public Vegetable(Vector2 pos, string image, int hp):base(pos,image,hp)
        {
            age = 0;
        }

        public override void Interact(Player player, Inventaire inventaire, List<Entity> objets)
        {
            player.addHunger(5);
            this.Destroy(inventaire, objets, this);
            base.Interact(player, inventaire, objets);
        }

        public override void Update()
        {
            age += .01f;
            base.Update();
        }

        public float getAge()
        {
            return this.age;
        }

        public override void Destroy(Inventaire inventaire, List<Entity> objets, Entity entity)
        {
            inventaire.AddItem(new Item("graine", 1));
            base.Destroy(inventaire, objets, entity);
        }
    }
}
