using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeInTheWild
{
    class Door : Entity
    {

        private Texture2D openTex;
        private Texture2D closeTex;
        private bool open;

        public Door(Vector2 pos, string image, string open, int hp) : base(pos, image, hp)
        {
            this.openTex = Loader.Images[open];
            this.closeTex = Loader.Images[image];
            this.texture = closeTex;
            this.open = true;
        }

        public void setOpen(bool open)
        {
            if (open)
            {
                this.texture = openTex;
            }
            else
            {
                this.texture = closeTex;
            }
            this.open = open;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, new Vector2(this.position.X, this.position.Y), Color.White);
        }

    }
}
