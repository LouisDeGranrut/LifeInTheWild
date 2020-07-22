using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeInTheWild
{
    public abstract class Entity
    {

        protected Vector2 position;
        protected Texture2D texture;
        protected bool solid;
        protected float hp;

        public Entity(Vector2 pos, string image, int hp)
        {
            this.position = pos;
            this.texture = Loader.Images[image];
            this.hp = hp;
            this.solid = true;
        }

        public Vector2 getPosition()
        {
            return this.position;
        }

        public bool getSolid()
        {
            return this.solid;
        }

        public void setPosition(Vector2 newPos)
        {
            this.position = newPos;
        }

        public float getHP()
        {
            return this.hp;
        }

        public virtual void Interact(Player player, Inventaire inventaire, List<Entity> objets)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(this.texture, new Vector2(this.position.X, this.position.Y + 2), Color.Gray);// potentielles ombres
            spriteBatch.Draw(this.texture, this.position, Color.White);
        }

        //Inflige des dégats à l'entité
        public void Damage(int damage)
        {
            this.hp -= damage;
            DebugConsole.addLine("HP: " + this.hp);
        }

        public virtual void Destroy(Inventaire inventaire, List<Entity> objets,Entity entity) {
            objets.Remove(entity);
        }

        public virtual void Update()
        {
        }
    }
}
