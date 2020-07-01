using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeInTheWild
{
    public abstract class Menu
    {

        protected Menu()
        {
        }

        public abstract void Update(GameTime time);

        public abstract void Draw(SpriteBatch spriteBatch);

    }
}
