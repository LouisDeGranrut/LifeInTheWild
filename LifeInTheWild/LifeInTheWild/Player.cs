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
    class Player : Entity//hérite de Entity
    {
        private int hp;
        public Player(Vector2 pos, Texture2D tex, int hp) : base(pos, tex)
        {
            this.hp = hp;
        }

        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                this.position.Y += 1;
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                this.position.Y -= 1;
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                this.position.X -= 1;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                this.position.X += 1;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);



        }
    }
}
