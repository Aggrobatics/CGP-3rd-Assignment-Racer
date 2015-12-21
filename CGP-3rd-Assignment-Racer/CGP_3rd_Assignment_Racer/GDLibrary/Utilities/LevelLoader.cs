using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using GDGame;
using System;

namespace GDLibrary
{
    public class LevelLoader
    {
        private static readonly Color ColorLevelLoaderIgnore = Color.White;

        private Main game;
        public LevelLoader(Main game)
        {
            this.game = game;
        }

        public List<Sprite> Load(Texture2D texture, int tileWidth, int tileHeight)
        {
            List<Sprite> list = new List<Sprite>();
            Color[] colorData = new Color[texture.Height * texture.Width];
            texture.GetData<Color>(colorData);

            Color color; 
            Vector2 position; 
            Sprite obj;

            for (int y = 0; y < texture.Height; y++)
            {
                for (int x = 0; x < texture.Width; x++)
                {
                    color = colorData[x + y * texture.Width];

                    if (!color.Equals(ColorLevelLoaderIgnore))
                    {
                        position = new Vector2(x * tileWidth, y * tileHeight);
                        obj = getObjectFromColor(color, position, tileWidth, tileHeight);

                        if (obj != null)
                            list.Add(obj);
                    }
                } //end for x
            } //end for y
            return list;
        }

        private Random rand = new Random();
        private Sprite getObjectFromColor(Color color, Vector2 position, int tileWidth, int tileHeight)
        {
            Vector2 scale = Vector2.One;
            Texture2D texture = null;
            Transform2D transform = null;
            TextureParameters textureParameters = null;
            IController controller = null;
            Sprite sprite = null;

            if (color.Equals(new Color(255, 0, 0)))   //red could be a player
            {
                //make sprite and set parameters
            }
            //add an else if for each type of sprite you want to load...

            return sprite;
        }


    }
}
