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
    public class Crafting : Overlay
    {

        private Texture2D window;

        public Crafting() : base()
        {
            this.window = Loader.Images["window"];
        }

        public override void Update()
        {
            if (isActive)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space)){
                    DebugConsole.addLine("Tried to craft something (1)");
                    //il faut maintenant définir l'outil à attribuer au joueur puis quand il placera l'objet, lui déduire les ressources
                    isActive = false;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.NumPad2)){
                    DebugConsole.addLine("Tried to craft something (2)");
                    isActive = false;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            if (isActive)
            {
                Color brun = new Color(68, 36, 52);
                spriteBatch.Draw(window, new Rectangle(460, 0, 460, 680), Color.White);
                spriteBatch.DrawString(font, "CRAFTING (veuillez selectionner l'objet a construire)", new Vector2(510, 50), brun);
                spriteBatch.DrawString(font, "1 - Campfire", new Vector2(510, 65), brun);
                spriteBatch.DrawString(font, "2 - Chest", new Vector2(510, 80), brun);
                spriteBatch.DrawString(font, "3 - Door", new Vector2(510, 95), brun);
                spriteBatch.DrawString(font, "4 - Stone Wall", new Vector2(510, 110), brun);
            }
        }

    }
}
