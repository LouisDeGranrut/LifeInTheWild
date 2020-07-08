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
    public static class Loader
    {

        public static Dictionary<string, Texture2D> Images;
        public static Dictionary<string, SoundEffect> Sounds;
        public static Dictionary<string, SpriteFont> Fonts;

        //On peut créer une fonction pour charger certaines images précises en fonction des besoins 
        //plutot que de charger toutes les images d'un coup
        public static void LoadImages(ContentManager content)
        {
            Images = new Dictionary<string, Texture2D>();

            List<string> imagesName = new List<string>()
            {
                "tree","sapin","rocks","pot","bush","door","door_open","chest","playerup","playerdown","playerleft","playerright",
                "grass","grass2","grass3","flowers","dirt","woodTile","campfire","crop","chicken_left","chicken_right",
                "chicken_up","chicken_down","wallFace","rect","window","panneau","pancarteWindow","well","anvil","flatrock",
                "heart","water","bread","logo"
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
                "hit","destroy","mow"
            };

            foreach (string file in fileName)
            {
                Sounds.Add(file, content.Load<SoundEffect>(file));
            }

        }

        public static void LoadFont(ContentManager content)
        {
            Fonts = new Dictionary<string, SpriteFont>();

            List<string> fileName = new List<string>()
            {
                "basic"
            };

            foreach (string file in fileName)
            {
                Fonts.Add(file, content.Load<SpriteFont>(file));
            }

        }

    }
}
