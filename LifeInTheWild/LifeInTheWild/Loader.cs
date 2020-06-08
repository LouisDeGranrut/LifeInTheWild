using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeInTheWild
{
    public class Loader
    {

        public static Dictionary<string, Texture2D> Images;

        //On peut créer une fonction pour charger certaines images précises en fonction des besoins 
        //plutot que de charger toutes les images d'un coup
        public static void LoadImages(ContentManager content)
        {
            Images = new Dictionary<string, Texture2D>();

            List<string> imagesName = new List<string>()
            {
                "tree","sapin","rocks","pot","bush","door","chest","playerup","playerdown","playerleft","playerright",
                "grass","grass2","grass3","flowers","dirt","campfire"
            };

            foreach(string img in imagesName)
            {
                Images.Add(img, content.Load<Texture2D>(img));
            }

        }

    }
}
