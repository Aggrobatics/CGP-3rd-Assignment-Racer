using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GDLibrary
{
    //static => we will never instanciate this class
    public static class MathUtility
    {
        public static Vector2 GetRandomUnitVector()
        {
            Random rand;
            int x=0, y=0;

            rand = new Random();
            do
            {  
                y = rand.Next(-45, 45);
            } while (y == 0);

            x = rand.Next(-45, 45);

            return Vector2.Normalize(new Vector2(x, y));
        }

        public static Rectangle Add(Rectangle a, Rectangle b)
        {
            return new Rectangle(a.X + b.X, a.Y + b.Y, a.Width + b.Width, a.Height + b.Height);
        }

        public static Rectangle AnimationAdd(Rectangle a, int deltaX, int deltaY)
        {
            return new Rectangle(a.X + deltaX, a.Y + deltaY, a.Width, a.Height);
        }
    }
}
