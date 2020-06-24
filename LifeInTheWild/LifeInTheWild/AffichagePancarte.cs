using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeInTheWild
{
    public class AffichagePancarte : Overlay
    {
        private Texture2D slot;
        private Texture2D window;
        private string text;
        private Game game;

        private List<Item> items;

        public AffichagePancarte(Game game) : base(game)
        {
            this.window = Loader.Images["pancarteWindow"];
            this.game = game;
        }

        public override void Update()
        {

        }

        public virtual void Activate(string text)
        {
            this.text = text;
            base.Activate();
        }

        public bool getActive()
        {
            return this.isActive;
        }

        public override void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            if (isActive)
            {
                spriteBatch.Draw(window, new Rectangle(230, 0, 460, 680), Color.White);
                spriteBatch.DrawString(font, this.text, new Vector2(254, 50), Color.Red);
            }
        }
    }
}
