using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text.RegularExpressions;

namespace GDLibrary
{
    public class StringUtility
    {
        public static string ParseNameFromPath(string path)
        {
            return Regex.Match(path, @"[^\\/]*$").Value;
        }

        public static Rectangle GetBoundsFromFont(SpriteFont font, string text, Vector2 position)
        {
            Vector2 textDimensions = font.MeasureString(text);
            return new Rectangle((int)position.X, (int)position.Y, (int)textDimensions.X, (int)textDimensions.Y);
        }


        public static string GetRectangleAsCompactString(Rectangle rectangle)
        {
            return "(" + rectangle.X + ", " + rectangle.Y + ", " + rectangle.Width + ", " + rectangle.Height + ")";
        }
    }
}
