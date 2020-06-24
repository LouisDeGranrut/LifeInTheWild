using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeInTheWild
{
    public abstract class Overlay
    {

        public bool isActive { get; set; }
        protected Game game;

        public Overlay (Game game)
        {
            this.game = game;
        }

        public void Activate()
        {
            this.isActive = !isActive;
        }

        public abstract void Update();

        public virtual void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
        }
        public virtual bool getActive()
        {
            return this.isActive;
        }
    }
}
