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

        //Movement---------------------------------------------------------------------------------------------------------------------------------------------
        protected float speed;
        protected Vector2 direction;
        protected Vector2 newPosition;
        protected Vector2 OldPosition;

        public Creature(Vector2 pos, string image, int hp) : base(pos, image, hp)
        {
            this.speed = .4f;
            this.position = pos;
            this.direction = new Vector2(0, 0);
    }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------
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
            return ((Math.Pow(this.position.X - objet.getPosition().X, 2) + Math.Pow(this.position.Y - objet.getPosition().Y, 2)) < (256));
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

            //Gestion des mouvements
            OldPosition = position;
            if (CollisionManager(objets, newPosition) == null){//si on a pas de collisions
                position = newPosition;
            }
            else//sinon
            {
                newPosition = OldPosition;
            }

            base.Update();
        }

    }
}
