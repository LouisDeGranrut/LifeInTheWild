using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeInTheWild
{
    class Pancarte : Entity
    {

        private string text;

        public Pancarte(Vector2 pos, string image, int hp, string text) : base(pos, image, hp)
        {
            this.position = pos;
            this.text = text;
        }

        public string getText()
        {
            return this.text;
        }
    }
}
