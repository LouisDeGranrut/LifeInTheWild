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
        }

        public override void Update()
        {

        }

        public override void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            if (isActive)
            {
                spriteBatch.Draw(window, new Rectangle(0,0, 920/2,680/2), Color.White);
                for(int i = 0; i<items.Count; i++)
                {
                    spriteBatch.DrawString(font, "-" + items[i].ToString(), new Vector2(50, 70+(15*i)), Color.Red);
                }
                spriteBatch.DrawString(font, "INVENTAIRE", new Vector2(50, 50), Color.Red);
            }
        }

        public void AddItem(Item i)
        {
            this.items.Add(i);
            DebugConsole.addLine(i + " was added to inventory");
        }

    }
}
