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
        private Player player;

        public Crafting(Player player) : base()
        {
            this.window = Loader.Images["window"];
            this.player = player;
        }

        public override void Update()
        {
            if (isActive)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.NumPad0)){
                    isActive = false;
                    player.setOutil(1);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.NumPad1)){
                    isActive = false;
                    player.setOutil(2);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.NumPad2)){
                    isActive = false;
                    player.setOutil(3);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.NumPad3)){
                    isActive = false;
                    player.setOutil(4);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.NumPad4)){
                    isActive = false;
                    player.setOutil(5);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.NumPad5)){
                    isActive = false;
                    player.setOutil(6);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.NumPad6))
                {
                    isActive = false;
                    player.setOutil(7);
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
                spriteBatch.DrawString(font, "0 - Mur en Pierre", new Vector2(510, 65), brun);
                spriteBatch.DrawString(font, "1 - Puis", new Vector2(510, 80), brun);
                spriteBatch.DrawString(font, "2 - Feu de Camp", new Vector2(510, 95), brun);
                spriteBatch.DrawString(font, "3 - Porte", new Vector2(510, 110), brun);
                spriteBatch.DrawString(font, "4 - Planter", new Vector2(510, 125), brun);
                spriteBatch.DrawString(font, "5 - Enclume", new Vector2(510, 140), brun);
                spriteBatch.DrawString(font, "6 - Parquet", new Vector2(510, 155), brun);
            }
        }
    }
}
