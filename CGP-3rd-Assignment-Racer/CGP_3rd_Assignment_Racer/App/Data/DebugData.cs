using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace GDGame
{
    public abstract class DebugData
    {
        public static int DrawOrderSpriteManager = 1;
        public static int DrawOrderHudManager = 2;
        public static int DrawOrderDebugManager = 3; //dont want to see debug over menu
        public static int DrawOrderMenuManager = 4;

        //the debug rectangles and text colours
        public static Color ColorBounds = Color.Red;
        public static Color ColorText = Color.White;



    }
}
