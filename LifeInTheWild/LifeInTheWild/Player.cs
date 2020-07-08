using Microsoft.Win32.SafeHandles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeInTheWild
{
    public class Player : Creature//hérite de Entity
    {
        //Gameplay-----------------------------------------
        private int outil;//l'outils équipé
        private float hunger;
        private float thirst;
        //Textures-----------------------------------------
        private Texture2D down;
        private Texture2D left;
        private Texture2D right;
        private Texture2D up;
        //Audio--------------------------------------------
        SoundEffect hit;
        SoundEffect mow;

        private KeyboardState oldState;
        private Vector2 dir;//permet de stocker la direction du joueur

        // Constructeur
        public Player(Vector2 pos, int hp, string image) : base(pos, image, hp)
        {
            this.hunger = 100;
            this.thirst = 100;
            this.hit = Loader.Sounds["hit"];
            this.mow = Loader.Sounds["mow"]; ;

            this.down = Loader.Images["playerdown"];
            this.left = Loader.Images["playerleft"];
            this.right = Loader.Images["playerright"];
            this.up = Loader.Images[image];

            oldState = Keyboard.GetState();
        }

        public virtual void Update(List<Entity> objets, int[,] map, Inventaire inventaire, Crafting crafting, AffichagePancarte affPancarte)
        {
            KeyboardState newState = Keyboard.GetState();  // get the newest state
            Vector2 newPosition = position;

            hunger -= .005f;
            thirst -= .005f;

            if(hunger <= 0 || thirst <= 0){
                hp -= .05f;
            }
            else
            {
                hp = 100;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S)) {
                this.texture = up;
                this.direction.Y = 1;
                this.dir = new Vector2(0,1);
                this.newPosition.Y += 1;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Z)) {
                this.texture = down;
                this.direction.Y = -1;
                this.dir = new Vector2(0, -1);
                this.newPosition.Y -= 1;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Q)){
                this.texture = left;
                this.direction.X = -1;
                this.dir = new Vector2(-1, 0);
                this.newPosition.X -= 1;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                this.texture = right;
                this.direction.X = 1;
                this.dir = new Vector2(1, 0);
                this.newPosition.X += 1;
            }

            if (newState.IsKeyDown(Keys.P) && oldState.IsKeyUp(Keys.P))//TEMPORAIRE
            {
                outil +=1;
            }

            if (newState.IsKeyDown(Keys.M) && oldState.IsKeyUp(Keys.M))//TEMPORAIRE
            {
                outil -= 1;
            }

            if (newState.IsKeyDown(Keys.Escape) && oldState.IsKeyUp(Keys.Escape))
            {
                if (affPancarte.getActive())
                    affPancarte.Activate();
                if (inventaire.getActive())
                    inventaire.Activate();
                if (crafting.getActive())
                    crafting.Activate();
            }

            if (newState.IsKeyDown(Keys.E) && oldState.IsKeyUp(Keys.E))
            {
                if (CollisionManager(objets, newPosition + dir) is Entity theEntity)
                {
                    theEntity.Interact(this, inventaire, objets);
                }
            }

            if (newState.IsKeyDown(Keys.I) && oldState.IsKeyUp(Keys.I))
            {
                DebugConsole.addLine("Ouverture Inventaire");
                inventaire.Activate();
            }

            if (newState.IsKeyDown(Keys.C) && oldState.IsKeyUp(Keys.C))
            {
                DebugConsole.addLine("Ouverture Crafting");
                crafting.Activate();
            }

            if (newState.IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space))
            {
                if (CollisionManager(objets, position + dir * 4) != null)
                {
                    CollisionManager(objets, position + dir * 4).Damage(1);
                    hit.Play();                    
                }
                else//si on collisionne avec rien
                {
                    mow.Play();//son 
                    switch (outil)
                    {
                        case 1://planter des graines
                            spawnObject(objets,new Arbre(new Vector2(0,0), "campfire", 2));
                            inventaire.removeItem(new Item("bois", 1));
                            outil = 0;
                            break;
                        case 2://mettre du parquet
                            spawnObject(objets, new Well(new Vector2(0, 0), "well", 2));
                            inventaire.removeItem(new Item("pierre", 1));
                            outil = 0;
                            break;
                        case 3://labourer le sol
                            map[(int)((this.position.Y + 8) / 16), (int)((this.position.X + 8) / 16)] = 4;                            
                            break;
                        case 4:
                            spawnObject(objets, new Wall(new Vector2(0,0), "wallFace", "flatrock", 2));
                            outil = 0;
                            break;
                        case 5:
                            spawnObject(objets, new Arbre(new Vector2(0,0), "campfire", 2));
                            outil = 0;
                            break;
                    }
                }
            }
            oldState = newState;
            //collision avec une pancarte
            if (CollisionManager(objets, newPosition+dir) is Pancarte thePancarte && affPancarte.getActive() == false)
            {
                affPancarte.Activate(thePancarte.getText());
            }
            
            base.Update(objets);
        }

        private void spawnObject(List<Entity> objets,Entity objet)
        {
            double posX = Math.Round((this.position.X + (this.dir.X * 16)) / 16);
            double posY = Math.Round((this.position.Y + (this.dir.Y * 16)) / 16);
            objet.setPosition(new Vector2((float)posX * 16, (float)posY * 16));
            objets.Add(objet);
            DebugConsole.addLine("Spawning: " + objet);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, new Vector2(this.position.X, this.position.Y), Color.White);

        }

        //Getters & Setters-------------------------------------------------------------------------------------------------------------------------------------

        public int getOutil()
        {
            return this.outil;
        }

        public void setOutil(int n)
        {
            this.outil = n;
        }

        public Vector2 getDir()
        {
            return this.dir;
        }

        public float getHunger()
        {
            return this.hunger;
        }

        public void addHunger(int a)
        {
            this.hunger += a;
            DebugConsole.addLine("manger");
        }

        public void addThirst(int a)
        {
            this.thirst += a;
            DebugConsole.addLine("boire");
        }

        public float getThirst()
        {
            return this.thirst;
        }

        public Vector2 getDirection()
        {
            return this.direction;
        }
    }
}
