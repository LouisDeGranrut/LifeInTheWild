using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeInTheWild
{
    abstract class Entity
    {

        protected Vector2 position;
        protected Texture2D texture;

        public Entity(Vector2 pos, Texture2D tex)
        {
            this.position = pos;
            this.texture = tex;
        }

        public Vector2 getPosition()
        {
            return this.position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, this.position, Color.White);
        }

    }
}
