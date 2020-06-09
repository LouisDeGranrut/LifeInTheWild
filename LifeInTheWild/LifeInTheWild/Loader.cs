using Microsoft.Xna.Framework.Audio;
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
        public static Dictionary<string, SoundEffect> Sounds;

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

        public static void LoadAudio(ContentManager content)
        {
            Sounds = new Dictionary<string, SoundEffect>();

            List<string> fileName = new List<string>()
            {
                "hit"
            };

            foreach (string file in fileName)
            {
                Sounds.Add(file, content.Load<SoundEffect>(file));
            }

        }

    }
}
