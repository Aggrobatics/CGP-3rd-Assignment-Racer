using System;
using Microsoft.Xna.Framework;

namespace GDLibrary
{
    public class ColorUtility
    {
          
        public static Color LerpBySineElapsedTime(Color a, Color b, GameTime gameTime, float lerpSpeed)
        {
            float lerp = (float)(Math.Sin(lerpSpeed * gameTime.TotalGameTime.TotalMilliseconds) * 0.5f + 0.5f);
            return Lerp(a, b, lerp);
        }

        public static Color Lerp(Color a, Color b, float factor)
        {
            int lR = Round(MathHelper.Lerp(a.R, b.R, factor));
            int lG = Round(MathHelper.Lerp(a.G, b.G, factor));
            int lB = Round(MathHelper.Lerp(a.B, b.B, factor));
            int lA = Round(MathHelper.Lerp(a.A, b.A, factor));
            return new Color(lR, lG, lB, lA);
        }

        public static int Round(float value)
        {
            return (int)Math.Round(value);
        }


        public static Vector2 LerpBySineElapsedTime(Vector2 a, Vector2 b, GameTime gameTime, float lerpSpeed)
        {
            float lerp = (float)(Math.Sin(lerpSpeed * gameTime.TotalGameTime.TotalMilliseconds) * 0.5f + 0.5f);
            return Vector2.Lerp(a, b, lerp);
        }

      
    }
}
