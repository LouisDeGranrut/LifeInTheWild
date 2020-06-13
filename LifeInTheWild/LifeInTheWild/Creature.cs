using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeInTheWild
{
    public abstract class Creature : Entity
    {

        //Movement-----------------------------------------
        protected Vector2 velocity;
        protected Vector2 direction;
        protected float speed;

        public Creature(Vector2 pos, string image, int hp) : base(pos, image, hp)
        {
            //this.texture = Loader.Images[image];

            this.speed = .4f;
            this.position = pos;
            this.direction = new Vector2(0, 0);
            this.velocity = new Vector2(0, 0);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------
        //Collisions avec les objets proche, retourne l'objet avec lequel la creature collisionne
        public Entity CollisionManager(List<Entity> objets, Vector2 v)
        {
            Entity res = null;
            foreach (Entity el in objets)//pour tous les objets de la map
            {
                if (checkDistance(el) && collisionObjet(el, v))//si l'objet est proche de la creature et qu'il collisionne avec
                {
                    res = el;//on retourne l'objet en question
                }
            }
            return res;
        }

        // Si l'objet en paramètre est à 16 unités de la créature, alors on retourne "vrai"
        public bool checkDistance(Entity objet)
        {
            return ((Math.Pow(this.position.X - objet.getPosition().X, 2) + Math.Pow(this.position.Y - objet.getPosition().Y, 2)) < (16*16));
        }

        //retourne un booleen si la créature collisionne avec l'objet donné
        public bool collisionObjet(Entity objet, Vector2 v)
        {
            int size = 14;
            return (v.X < objet.getPosition().X + size &&
                    v.X + size > objet.getPosition().X &&
                    v.Y < objet.getPosition().Y + size &&
                    v.Y + size > objet.getPosition().Y);
        }

        public virtual void Update(List<Entity> objets)
        {

            //à ce niveau, chaque créature modifie xDir et yDir en fonction de son IA

            //Gestion des mouvements
            velocity += direction * speed;

            if (CollisionManager(objets, position + new Vector2(velocity.X * speed, 0)) == null){
                position.X += velocity.X * speed;
            }
            else
            {
                //DebugConsole.addLine("Collision en X");
                //this.position.X += this.direction.X;
            }

            if (CollisionManager(objets, position + new Vector2(0, velocity.Y * speed)) == null){
                position.Y += velocity.Y * speed;
            }
            else
            {
                //DebugConsole.addLine("Collision en Y");
                //this.position.Y += this.direction.Y;
            }

            velocity *= .80f;
            
            direction = new Vector2(0, 0);

            base.Update();
        }

    }
}
