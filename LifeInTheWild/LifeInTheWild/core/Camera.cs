using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeInTheWild
{
   public class Camera
    {
        public Matrix Transform { get; private set; }

        public void Follow(Player player)
        {
            Transform = Matrix.CreateTranslation(-(player.getPosition().X - (8))*.9f, -(player.getPosition().Y - (8))*.9f, 0) * Matrix.CreateTranslation(Game1.screenWidth / 4,  Game1.screenHeight / 4, 0);
        }
    }
}
