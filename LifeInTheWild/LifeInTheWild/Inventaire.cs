using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeInTheWild
{
    public class Inventaire : Overlay
    {
        private Texture2D window;
        private List<Item> items;

        public Inventaire() : base()
        {
            this.window = Loader.Images["window"];
            this.items = new List<Item>();
        }

        public override void Update()
        {

        }

        public override void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            if (isActive)
            {
                spriteBatch.Draw(window, new Rectangle(0,0, 460,680), Color.White);
                for(int i = 0; i<items.Count; i++)
                {
                    spriteBatch.DrawString(font, "-" + items[i].getName() + " x" + items[i].getQuantity(), new Vector2(50, 70+(15*i)), Color.Red);
                }
                spriteBatch.DrawString(font, "INVENTAIRE", new Vector2(50, 50), Color.Red);
            }
        }

        public void AddItem(Item i)
        {
            if(items.Count <= 0)//si l'inventaire est vide
            {
                DebugConsole.addLine("Inventaire vide, on ajoute l'objet");
                this.items.Add(i);//on ajoute l'objet
            }
            else//si l'inventaire n'est pas vide
            {
                DebugConsole.addLine("Inventaire non vide");
                bool objetExiste = false;

                for (int j = 0; j < items.Count; j++)//on le parcourt l'erreur vient du fait que ça s'execute dés le premier objet, si le premier objet est un arbre, alors forcement, les cailloux qu'on ramasse ne seront pas du meme type que l'arbre qu'on a en premier dans l'inventaire !
                {
                    if (items[j].getName() == i.getName())
                    {
                        DebugConsole.addLine("Objet du meme type trouve");
                        items[j].addQuantity(1);
                        objetExiste = true;
                    }

                    if(j == items.Count-1 && objetExiste == false)
                    {
                        DebugConsole.addLine("Objet du meme type non trouve");
                        this.items.Add(i);
                    }
                }
            }
        }

        public void removeItem(Item i)
        {
            for (int j = 0; j < items.Count; j++)
            {
                if (items[j].getName() == i.getName() && items[j].getQuantity()>= 1)
                {
                    items[j].addQuantity(-1);
                }
            }
        }

        public int Size()
        {
            return this.items.Count;
        }

    }
}
