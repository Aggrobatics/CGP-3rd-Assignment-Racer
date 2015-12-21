using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace GDLibrary
{
    public class MenuData
    {
        #region MENU_STRINGS;
        //all the strings shown to the user through the menu
        public static String TitleGame = "Space Invaders 2000";

        public static String MenuTextResume = "Play";
        public static String MenuTextAudio = "Audio";
        public static String MenuTextBackToMain = "Back";
        public static String MenuTextExit = "Exit";

        public static String MenuTextAudioUp = "Volume Up";
        public static String MenuTextAudioDown = "Volume Down";

        public static string MenuTextExitYes = "Yes";
        public static string MenuTextExitNo = "No";

        public static Color MenuColorInactiveText = Color.DarkBlue;
        public static Color MenuColorActiveText = Color.LightBlue;

        public static Color MenuColorInactiveTexture = MenuColorActiveText;
        public static Color MenuColorActiveTexture = MenuColorInactiveText;

        //used to draw the correct background texture for the current menu shown
        public static int IndexMainMenuTexture = 0;
        public static int IndexAudioMenuTexture = 1;

        //press P to show the menu
        public static Keys MenuKeyShowMenu = Keys.P;

        //mix color for the background texture
        public static Color ColorBackgroundTexture = Color.White;

        //amount by which we deflate the menu texture to show the background game around the edges
        public static Integer2 MenuTexturePadding = -20 * Integer2.One;


        #endregion;

    }
}
