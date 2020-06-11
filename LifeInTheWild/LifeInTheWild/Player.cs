using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeInTheWild
{
    public class Player : Creature//hérite de Entity
    {
        //Gameplay-----------------------------------------
        private int outil;//l'outils équipé
        private int rock;//Inventaire
        private int wood;//inventaire
        //Textures-----------------------------------------
        private Texture2D down;
        private Texture2D left;
        private Texture2D right;
        private Texture2D up;
        //Audio--------------------------------------------
        SoundEffect hit;
        SoundEffect mow;

        private KeyboardState oldState;

        // Constructeur
        public Player(Vector2 pos, int hp, string image, string down, string left, string right, SoundEffect hit, SoundEffect mow) : base(pos, image, hp)
        {
            this.hp = hp;
            this.wood = 0;
            this.rock = 0;

            this.down = Loader.Images[down];
            this.left = Loader.Images[left];
            this.right = Loader.Images[right];
            this.up = Loader.Images[image];

            this.hit = hit;
            this.mow = mow;

            oldState = Keyboard.GetState();
        }

        public virtual void Update(List<Entity> objets, int[,] map)
        {
            KeyboardState newState = Keyboard.GetState();  // get the newest state

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

            if (newState.IsKeyDown(Keys.P) && oldState.IsKeyUp(Keys.P))//change l'outil équipé
            {
                outil +=1;
            }

            if (newState.IsKeyDown(Keys.M) && oldState.IsKeyUp(Keys.M))//change l'outil équipé
            {
                outil -= 1;
            }

            if (newState.IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space))
            {
                if (CollisionManager(objets, position+velocity) !=null)// PIRE CODE EVER
                {
                    CollisionManager(objets, position + velocity).Damage(1);// PIRE CODE EVER
                    hit.Play();                    
                }
                else
                {
                    map[(int)((this.position.Y+8) / 16), (int)((this.position.X+8) / 16)] = 4;
                    mow.Play();
                    if (this.outil == 1)
                    {
                        //TODO
                        objets.Add(new Rock(new Vector2(this.position.X, this.position.Y), "crop", 10));
                        Console.WriteLine("PLANTAGE DE GRAINES");
                    }
                }
            }

            oldState = newState;

            base.Update(objets);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, new Vector2(this.position.X, this.position.Y), Color.White);
        }

        //Getters & Setters-------------------------------------------------------------------------------------------------------------------------------------
        /*public int getHP()
        {
            return this.hp;
        }*/

        public int getOutil()
        {
            return this.outil;
        }

        public int getWood()
        {
            return this.wood;
        }

        public void addWood(int add)
        {
            this.wood += add;
        }

        public int getRock()
        {
            return this.rock;
        }

        public void addRock(int add)
        {
            this.rock += add;
        }
    }
}
