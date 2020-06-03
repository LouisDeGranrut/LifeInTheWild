using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeInTheWild
{
    class Player : Entity//hérite de Entity
    {
        private int hp;

        private Vector2 velocity;
        private Vector2 direction;
        private float speed;
        private float yDir;
        private float xDir;
        public Player(Vector2 pos, Texture2D tex, int hp) : base(pos, tex)
        {
            this.hp = hp;

            this.speed = .4f;
            this.position = pos;
            this.direction = new Vector2(0, 0);
            this.velocity = new Vector2(0, 0);
            yDir = 0;
            xDir = 0;
        }

        private bool collision(List<Entity> objets, Vector2 v)
        {
            bool res = false;
            foreach(Entity el in objets)//pour tous les objets de la map
            {
                if (checkDistance(el))//si l'objet est proche du joueur, on regarde s'il collisionne avec
                {
                    res = (v.X < el.getPosition().X + 16 && v.X + 16 > el.getPosition().X &&
                v.Y < el.getPosition().Y + 16 && v.Y + 16 > el.getPosition().Y);
                    Console.WriteLine("COLLISIONS");
                }
            }
            return res;
        } 

        // Si l'objet en paramètre est à une 5 unités du joueur, alors on retourne "vrai"
        private bool checkDistance(Entity objet)
        {
            return ((Math.Pow(this.position.X - objet.getPosition().X, 2) + Math.Pow(this.position.Y - objet.getPosition().Y, 2)) < (16*16));
        }

        public void Update(List<Entity> objets)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                yDir = speed;
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                yDir =-speed;
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                xDir = -speed;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                xDir = speed;

            velocity.X += xDir * (float)Math.Cos(-direction.X) - yDir * (float)Math.Sin(-direction.X);
            velocity.Y += yDir * (float)Math.Cos(-direction.X) + xDir * (float)Math.Sin(-direction.X);

            //check collision sur l'axe X (if position + velocity.X != collision)
            if (!collision(objets, position + velocity))
            {
                position.X += velocity.X * speed;
            }

            //check collision sur l'axe Z (if position + velocity.Z != collision)
            if (!collision(objets, position + velocity))
            {
                position.Y += velocity.Y * speed;
            }

            velocity *= .85f;
            xDir = 0;
            yDir = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public int getHP()
        {
            return this.hp;
        }
    }
}
