using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeInTheWild
{
    public static class DebugConsole
    {
        private static int ligne = 0;
        private static List<String> log = new List<String>();

        //On ajoute du texte au tableau de log
        public static void addLine(String text)
        {
            log.Add(text);
            ligne += 1;
        }

        //On affiche tous le contenu du tableau
        public static void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            for(int i = 0; i < ligne; i++)
            {
                spriteBatch.DrawString(font, log[i], new Vector2(150, i * 15), Color.White);
            }
        }
    }
}
