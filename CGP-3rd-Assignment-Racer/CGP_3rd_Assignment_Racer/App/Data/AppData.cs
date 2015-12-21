using Microsoft.Xna.Framework;
namespace GDGame
{
    public abstract class AppData
    {
        //title
        public static string StringDefaultGameTitle = "No Title Set";

        //paths
        public static readonly string StringPathContentRoot = "Content/";
        public static readonly string StringPathBackground = "Assets/Textures/Background/";
        public static readonly string StringPathCharacters = "Assets/Textures/Character/";
        public static readonly string StringPathDebug = "Assets/Textures/Debug/";
        public static readonly string StringPathAnimated = "Assets/Textures/Animated/";
        public static readonly string StringPathMenu = "Assets/Textures/Menu/";
        public static readonly string StringPathFonts = "Assets/Fonts/";
        public static readonly string StringPathLevels = "Assets/Textures/Levels/";
        public static readonly string StringPathProps = "Assets/Textures/Props/";

        public static readonly string StringPathSky = "Assets/Textures/Background/Sky/";
        public static readonly string StringPathTerrain = "Assets/Textures/Background/Terrain/";
        public static readonly string StringPathFoliage = "Assets/Textures/Background/Foliage/";


        //positions, times etc
        public static int PlayerBottomVerticalOffset = 10;
        public static int PlayerScreenHorizontalPadding = -20;
        public static Vector2 BulletOffset = new Vector2(0, -50);
        public static int BulletDamage = 20;
        public static int PlayerTimeBetweenBulletInMs = 100;

        //win/lose
        public static int EnemyOneHealth = 100;

        //debug
        public static string DebugCollidableText = "#collidable:";
        public static string DebugNonCollidableText = "#non-collidable:";

        public static Vector2 DebugFPSTextPosition = new Vector2(20, 680);  //i.e. "FPS:"

        public static Vector2 DebugCollidableTextPosition = new Vector2(20, 700);
        public static Vector2 DebugNonCollidableTextPosition = new Vector2(20, 720);

        //level loader
        public static Color ColorLevelLoaderIgnore = Color.Black;

        //jump/fall
        public static float Gravity = 2f;
        public static float PlayerJumpHeight = 6;


        //curves
        public static readonly string CurveNamePlatformHorizontal1 = "thereandbackagain";







    }
}
