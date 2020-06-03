using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeInTheWild
{
    class Arbre : Entity
    {
        private int hp; 
        public Arbre(Vector2 pos, Texture2D tex, int hp) : base(pos, tex)
        {
            this.hp = hp;
        }

    }
}
