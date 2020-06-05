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
    public class Player : Entity//hérite de Entity
    {
        //Gameplay-----------------------------------------
        private int outil;//l'outils équipé
        //Movement-----------------------------------------
        private Vector2 velocity;
        private Vector2 direction;
        private float speed;
        private float yDir;
        private float xDir;
        //Textures-----------------------------------------
        private Texture2D down;
        private Texture2D left;
        private Texture2D right;
        private Texture2D up;
        private Texture2D attaque;

        // Constructeur
        public Player(Vector2 pos, int hp, Texture2D tex, Texture2D down, Texture2D left, Texture2D right, Texture2D attaque) : base(pos, tex, hp)
        {
            this.hp = hp;

            this.speed = .4f;
            this.position = pos;
            this.direction = new Vector2(0, 0);
            this.velocity = new Vector2(0, 0);
            yDir = 0;
            xDir = 0;

            this.down = down;
            this.left = left;
            this.right = right;
            this.up = tex;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------
        //Collisions avec les objets proche, retourne l'objet avec lequel le joueur collisionne
        private Entity CollisionManager(List<Entity> objets, Vector2 v)
        {
            Entity res = null;
            foreach(Entity el in objets)//pour tous les objets de la map
            {
                if (checkDistance(el) && collisionObjet(el, v))//si l'objet est proche du joueur et qu'il collisionne avec
                {
                    res = el;//on retourne l'objet en question
                }
            }
            return res;
        }

        // Si l'objet en paramètre est à 16 unités du joueur, alors on retourne "vrai"
        private bool checkDistance(Entity objet)
        {
            return ((Math.Pow(this.position.X - objet.getPosition().X, 2) + Math.Pow(this.position.Y - objet.getPosition().Y, 2)) < (16*16));
        }

        //retourne un booleen si player collisionne avec l'objet donné
        private bool collisionObjet(Entity objet, Vector2 v)
        {
            return (v.X < objet.getPosition().X + 16 && v.X + 16 > objet.getPosition().X && v.Y < objet.getPosition().Y + 16 && v.Y + 16 > objet.getPosition().Y);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------
        public void Update(List<Entity> objets, int[,] map)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.S)) {
                yDir = speed;
                this.texture = up;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Z)) {
                yDir =-speed;
                this.texture = down;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Q)){
                xDir = -speed;
                this.texture = left;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                xDir = speed;
                this.texture = right;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.OemPlus))
            {
                outil +=1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.OemMinus))
            {
                outil -= 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))// PIRE CODE EVER
            {
                if (CollisionManager(objets, position+velocity) !=null)// PIRE CODE EVER
                {
                    CollisionManager(objets, position + velocity).Damage( 1);// PIRE CODE EVER
                }
                else
                {
                    map[(int)((this.position.Y) / 16), (int)((this.position.X) / 16)] = 4;
                }
            }

            velocity.X += xDir * (float)Math.Cos(-direction.X) - yDir * (float)Math.Sin(-direction.X);
            velocity.Y += yDir * (float)Math.Cos(-direction.X) + xDir * (float)Math.Sin(-direction.X);

            if (CollisionManager(objets, position + new Vector2(velocity.X, 0))==null){
                position.X += velocity.X * speed;
            }

            if (CollisionManager(objets, position + new Vector2(0,velocity.Y))==null){
                position.Y += velocity.Y * speed;
            }

            velocity *= .85f;
            xDir = 0;
            yDir = 0;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        //Getters-----------------------------------------------------------------------------------------------------------------------------------------------
        public int getHP()
        {
            return this.hp;
        }

        public int getOutil()
        {
            return this.outil;
        }
    }
}
