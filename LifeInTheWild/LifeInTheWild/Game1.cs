using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace LifeInTheWild
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;//permet d'afficher les assets du jeu
        public static int screenWidth, screenHeight;//la taille de l'écran
        Menu menuActuel;//gamestate actuel

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            screenWidth = graphics.PreferredBackBufferWidth = 920;
            screenHeight = graphics.PreferredBackBufferHeight = 680;
            graphics.IsFullScreen = false;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Loader.LoadImages(this.Content);
            Loader.LoadAudio(this.Content);
            Loader.LoadFont(this.Content);
            menuActuel = new MainMenu();
        }

        protected override void Update(GameTime gameTime)
        {

            //C'est pas là qu'il faut le mettre mais dans MainMenu, à voir plus tard
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                menuActuel = new GameMenu();
            }

            menuActuel.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            menuActuel.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
