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

        private Texture2D slot;
        private Texture2D window;
        private Game game;

        private List<Item> items;

        public Inventaire(Game game) : base(game)
        {
            this.slot = Loader.Images["rect"];
            this.window = Loader.Images["window"];
            this.game = game;
            this.items = new List<Item>();
            items.Add(new Item("arbre", 1));
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
            for (int j = 0; j < items.Count; j++)
            {
                if (items[j].getName() == i.getName())
                {
                    items[j].addQuantity(1);
                    DebugConsole.addLine(i + " type already exists");
                    return;
                }
                if (items[j].getName() != i.getName())
                {
                    DebugConsole.addLine(i + " was not found");
                    this.items.Add(i);
                    return;
                }
            }
        }

        public int Size()
        {
            return this.items.Count;
        }

    }
}
