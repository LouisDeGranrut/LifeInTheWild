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
        private Texture2D window;
        private string text;

        public AffichagePancarte() : base()
        {
            this.window = Loader.Images["pancarteWindow"];
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
                Color brun = new Color(68, 36, 52);
                spriteBatch.Draw(window, new Rectangle(230, 0, 460, 680), Color.White);
                spriteBatch.DrawString(font, this.text, new Vector2(254, 50), brun);
            }
        }
    }
}
