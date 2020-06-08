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
        protected int hp;

        public Entity(Vector2 pos, string image, int hp)
        {
            this.position = pos;
            this.texture = Loader.Images[image];
            this.hp = hp;
        }

        public Vector2 getPosition()
        {
            return this.position;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(this.texture, new Vector2(this.position.X, this.position.Y + 2), Color.Gray);// potentiels ombres
            spriteBatch.Draw(this.texture, this.position, Color.White);
        }

        //Inflige des dégats à l'entité
        public void Damage(int damage)
        {
            this.hp -= damage;
            Console.WriteLine("HP: " + this.hp);
        }

        public void Delete()
        {
            Console.WriteLine("SUPPRESSION");
        }
    }
}
