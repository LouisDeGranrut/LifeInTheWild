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
    class MainMenu : Menu
    {
        private SpriteFont font;

        public MainMenu() : base()
        {
           font = Loader.Fonts["basic"];
        }

        public override void Update(GameTime time)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Life In The Wild", new Vector2(100, 100), Color.White);
            spriteBatch.DrawString(font, "press enter to start game", new Vector2(100, 120), Color.White);
            spriteBatch.End();
        }

    }
}
