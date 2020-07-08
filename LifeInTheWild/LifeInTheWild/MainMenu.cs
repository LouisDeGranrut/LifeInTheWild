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
        public int selection;
        public Texture2D logo;

        public MainMenu() : base()
        {
           font = Loader.Fonts["basic"];
            selection = 0;
            logo = Loader.Images["logo"];
        }

        public override void Update(GameTime time)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad1))
            {
                selection = 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad2))
            {
                //Exit();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(logo, new Vector2(100, 100), Color.White);
            spriteBatch.DrawString(font, "Appuyez sur 1 pour lancer le jeu", new Vector2(100, 210), new Color(68,36,52));
            spriteBatch.DrawString(font, "Appuyez sur 2 pour quitter", new Vector2(100, 230), new Color(68, 36, 52));
            spriteBatch.End();
        }
    }
}
