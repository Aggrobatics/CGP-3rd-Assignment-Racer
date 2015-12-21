#define DEBUG

using System.Collections.Generic;
using GDLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

/*
 * Version: 3.4
 * ------------------------------------------------------------------------
 * Date:
 * - 15/12/15
 * 
 * Added:
 * - Notice we had to re-order intialisation of some of the managers (keyboard, camera, screen) because of a temporal dependence. 
 * - Added ScreenResolution helper
 * 
 * Known Bugs:
 * - Main menu screen is split - call ScreenManager::SetCameras() when moving from menu to game and vice versa - is this fixed?
 * - Are controllers cloning properly?
 * - Will animation clone across controllers?
 * - Will collision work for rotated boxes?
 * - Are the look and up vector being updated?
 * 
 *
 * Fixed Bugs:
 * 
 * Tests:
 * - None
 * 
 */


/*
 * Version: 3.3
 * ------------------------------------------------------------------------
 * Date:
 * - 11/12/15
 * 
 * Added:
 * - Added keyboard controller and started player controller demo for the Camera2D - see KeyData for control key mappings
 * 
 * Known Bugs:
 * - Main menu screen is split!
 * - Are controllers cloning properly?
 * - Will animation clone across controllers?
 * - Will collision work for rotated boxes?
 * - Are the look and up vector being updated?
 * 
 *
 * Fixed Bugs:
 * 
 * Tests:
 * - None
 * 
 */

/*
 * Version: 3.2
 * ------------------------------------------------------------------------
 * Date:
 * - 11/12/15
 * 
 * Added:
 * - Added camera splitscreen to SpriteManager and DebugManager
 * 
 * Known Bugs:
 * - Main menu screen is split!
 * - Are controllers cloning properly?
 * - Will animation clone across controllers?
 * - Will collision work for rotated boxes?
 * - Are the look and up vector being updated?
 * 
 *
 * Fixed Bugs:
 * 
 * Tests:
 * - None
 * 
 */
/*
 * Version: 3.1
 * ------------------------------------------------------------------------
 * Date:
 * - 11/12/15
 * 
 * Added:
 * - CameraTransform2D and Camera2D
 * 
 * Known Bugs:
 * - Are controllers cloning properly?
 * - Will animation clone across controllers?
 * - Will collision work for rotated boxes?
 * - Are the look and up vector being updated?
 * 
 *
 * Fixed Bugs:
 * 
 * Tests:
 * - None
 * 
 */

/*
 * Version: 3.0
 * ------------------------------------------------------------------------
 * Date:
 * - 7/12/15
 * 
 * Added:
 * - Controllers for PawnSprite and Camera
 * 
 * Known Bugs:
 * - Are controllers cloning properly?
 * - Will animation clone across controllers?
 * - Will collision work for rotated boxes?
 * - Are the look and up vector being updated?
 * 
 *
 * Fixed Bugs:
 * 
 * Tests:
 * - None
 * 
 */

namespace GDGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Main : Microsoft.Xna.Framework.Game
    {
        #region Fields
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private AssetDictionary<Texture2D> textureDictionary;
        private AssetDictionary<SpriteFont> fontDictionary;
        private GenericDictionary<string, Sprite> spriteDictionary;
        private GenericDictionary<string, Transform2DCurve> curveDictionary;

        private ScreenManager screenManager;
        private KeyboardManager keyboardManager;
        private MouseManager mouseManager;
        private SpriteManager spriteManager;
        private SoundManager soundManager;
        private MenuManager menuManager;
        private CameraManager cameraManager;

#if DEBUG
        private DebugDrawer2D debugDrawer2D;
#endif
        #endregion

        //temp vars
        private PawnSprite sprite;
        private Curve1D curve;

        #region Properties
        public Viewport Viewport
        {
            get
            {
                return this.GraphicsDevice.Viewport;
            }
        }
        public CameraManager CameraManager
        {
            get
            {
                return this.cameraManager;
            }
        }
        public KeyboardManager KeyboardManager
        {
            get
            {
                return this.keyboardManager;
            }
        }
        public MouseManager MouseManager
        {
            get
            {
                return this.mouseManager;
            }
        }

        public SpriteManager SpriteManager
        {
            get
            {
                return this.spriteManager;
            }
        }

        public ScreenManager ScreenManager
        {
            get
            {
                return this.screenManager;
            }
        }

        public SpriteBatch SpriteBatch
        {
            get
            {
                return this.spriteBatch;
            }
        }
        public GraphicsDeviceManager Graphics
        {
            get
            {
                return this.graphics;
            }
        }
        #endregion

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        #region Initialise
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            InitialiseInputManagers();

            InitialiseCameraManager();

            //resolution etc
            InitialiseScreenManager(ResolutionType.XGA, 
                CameraLayoutType.FullScreen,
                true, false, BSPSectorLayoutType.TwoByTwo);

            //dictionaries
            InitialiseDictionaries();

            //load textures
            LoadTextures();

            //load fonts
            LoadFonts();

            //curves for controllers
            LoadCurves();

            //set statics
            InitialiseStatics();

            //managers
            InitialiseManagers();

            //load level from image
            LoadLevel("level 1");

            //Add sprites
           // AddSprites();
            AddDistanceSprites();
            AddCompoundComontrollerDemo();
            AddAnimationControllerSprites();

#if DEBUG
                DebugOptionsType debugOptions = DebugOptionsType.DrawBoundingBoxes
             //     | DebugOptionsType.DrawBSPSectors 
                    | DebugOptionsType.DrawCollisionData 
                    | DebugOptionsType.DrawFPS;         
            
            InitialiseDebugDrawer(debugOptions);
#endif

            base.Initialize();
        }

        private void InitialiseInputManagers()
        {
            this.keyboardManager = new KeyboardManager(this);
            Components.Add(this.keyboardManager);

            this.mouseManager = new MouseManager(this);
            Components.Add(this.mouseManager);
        }

        private void InitialiseCameraManager()
        {
            this.cameraManager = new CameraManager(this);
            Components.Add(this.cameraManager);
        }
        private void InitialiseStatics()
        {
            Transform2D.game = this;
        }
        private void InitialiseDictionaries()
        {
            this.textureDictionary = new AssetDictionary<Texture2D>(this, "texture dictionary");
            this.fontDictionary = new AssetDictionary<SpriteFont>(this, "font dictionary");
            this.spriteDictionary = new GenericDictionary<string, Sprite>("sprite dictionary");
            this.curveDictionary = new GenericDictionary<string, Transform2DCurve>("curve dictionary");

        }
        private void InitialiseManagers()
        {
            this.spriteManager = new SpriteManager(this); //drawable!
            this.spriteManager.DrawOrder = DebugData.DrawOrderSpriteManager;
            Components.Add(this.spriteManager);

            //HUD

            //menu
            Texture2D[] menuTextures = 
            { 
                this.textureDictionary["mainmenu"], 
                this.textureDictionary["audiomenu"]
            };
            this.menuManager = new MenuManager(this, this.fontDictionary["menu"], menuTextures, MenuData.ColorBackgroundTexture, this.textureDictionary["white"], MenuData.MenuTexturePadding);
            this.menuManager.DrawOrder = DebugData.DrawOrderMenuManager;
            Components.Add(this.menuManager);

            //sound
            this.soundManager = new SoundManager(this);
            Components.Add(this.soundManager);

            //this.cameraManager = new CameraManager(this);
            //Components.Add(this.cameraManager);
        }

        private void InitialiseScreenManager(ResolutionType resolutionType, 
            CameraLayoutType cameraLayoutType,
           bool isMouseVisible, bool isFixedFrameRate, BSPSectorLayoutType bspSectorLayoutType)
        {
            this.screenManager = new ScreenManager(this, "My Awesome Game",
                resolutionType, cameraLayoutType, isMouseVisible, isFixedFrameRate, bspSectorLayoutType);

        }
        #endregion


        #region Load Sprites


        private void AddDistanceSprites()
        {
            Texture2D texture = null;
            TextureParameters textureParameters = null;
            Transform2D transform = null;
            PawnSprite pawnSprite = null;
            ControllerList controllerList = null;

            texture = this.textureDictionary["grass"];
            textureParameters = new TextureParameters(texture, SpriteEffects.None, Color.White, 1);
            transform = new Transform2D(new Vector2(512, 398), 0, Vector2.One, Vector2.UnitX, -Vector2.UnitY, textureParameters.Origin, textureParameters.Dimensions);
            pawnSprite = new PawnSprite("platform", transform, textureParameters, true, true);
            controllerList = pawnSprite.GetControllerList();
            controllerList.Add(new DistanceActivatedController
                (
                pawnSprite, ControllerType.DistancePlatform, true, 
                -Vector2.UnitY, 10, 50, 100, this.mouseManager));


            this.spriteManager.Add(pawnSprite);
        }
        private void AddSprites()
        {
            AddMovingPlatformSprite();

        }
        private void AddMovingPlatformSprite()
        {
            Texture2D texture = null;
            TextureParameters textureParameters = null;
            Transform2D transform = null;
            PawnSprite pawnSprite = null;
            ControllerList controllerList = null;
            CurveController curveController = null;
            PawnSprite clone = null;

            #region Platform 1 - Moves diagonally
            texture = this.textureDictionary["grass"];
            textureParameters = new TextureParameters(texture, SpriteEffects.None, Color.White, 0);
            transform = new Transform2D(new Vector2(100, 100), 0, Vector2.One, Vector2.UnitX, -Vector2.UnitY, textureParameters.Origin, textureParameters.Dimensions);
            pawnSprite = new PawnSprite("a", transform, textureParameters, true, true);
            controllerList = pawnSprite.GetControllerList();
            #endregion

            #region Clones

            //curve
            Transform2DCurve transform2DCurve = (Transform2DCurve)this.curveDictionary[AppData.CurveNamePlatformHorizontal1];
            controllerList.Add(new CurveController(pawnSprite, ControllerType.Curve, true, transform2DCurve));
            pawnSprite.ActorType = ActorType.PlatformMoveable;
            this.spriteManager.Add(pawnSprite);
          

            clone = (PawnSprite)pawnSprite.Clone();
            curveController = (CurveController)clone.GetControllerList().GetByType(ControllerType.Curve);
            curveController.Transform2DCurveOffsets = new Transform2DCurveOffsets(new Vector2(200, 0), Vector2.One, 0, 1.5f);
            this.spriteManager.Add(clone);

            clone = (PawnSprite)pawnSprite.Clone();
            curveController = (CurveController)clone.GetControllerList().GetByType(ControllerType.Curve);
            curveController.Transform2DCurveOffsets = new Transform2DCurveOffsets(new Vector2(400, 0), Vector2.One, 0, 2.5f);
            this.spriteManager.Add(clone);

            #endregion

        }
        
        private void AddCompoundComontrollerDemo()
        {
            Texture2D texture = this.textureDictionary["grass"];
            TextureParameters textureParameters = new TextureParameters(texture, SpriteEffects.None, Color.White, 0);

            Transform2D transform = new Transform2D(new Vector2(100, 100), 0, Vector2.One, Vector2.UnitX, -Vector2.UnitY,
                textureParameters.Origin, textureParameters.Dimensions);

            this.sprite = new PawnSprite("a", transform, textureParameters, true, true);
            ControllerList controllerList = this.sprite.GetControllerList();

            ////scale
            controllerList.Add(new ScaleLerpController(this.sprite, ControllerType.ScaleLerp,
              true, new Vector2(2,2), 0.001f));

            //position
            controllerList.Add(new PositionLerpController(this.sprite, ControllerType.PositionLerp,
               true, new Vector2(400, 400), 0.001f));

            controllerList.Add(new RotationLerpController(this.sprite, ControllerType.RotationLerp,
               true, 1));

            sprite.ActorType = ActorType.PlatformMoveable;
            this.spriteManager.Add(sprite);
        }
        
        
        private void AddAnimationControllerSprites()
        {
            Texture2D texture = this.textureDictionary["debuganimation"];
            TextureParameters textureParameters = new TextureParameters(texture, SpriteEffects.None, Color.White, 0);

            Transform2D transform = new Transform2D(new Vector2(500, 600), 45, Vector2.One, Vector2.UnitX, -Vector2.UnitY,
                textureParameters.Origin, textureParameters.Dimensions);

            this.sprite = new PawnSprite("a", transform, textureParameters, true, true);
            ControllerList controllerList = this.sprite.GetControllerList();

            Dictionary<string, AnimationData> dictionary = LoadAnimationData(AppData.StringPathAnimated, "debuganimation");
            controllerList.Add(new AnimationController(this.sprite, ControllerType.Animation, true,
                dictionary, "walk", true, 1));

            sprite.ActorType = ActorType.PlayerAnimated;
            this.spriteManager.Add(sprite);

            //bug cloned animation not working since 3.0
            //PawnSprite cloned = (PawnSprite)sprite.Clone();
            //cloned.Transform.Position += new Vector2(150, 0);
            //this.spriteManager.Add(cloned);




        }

         


        private void LoadLevel(string resourceName)
        {
            //clear anything already there
            this.spriteManager.ClearAll();

            //reset menu and hud

            //load
        }

        #endregion

        #region Load Assets
        private Dictionary<string, AnimationData> LoadAnimationData(string xmlPath, string xmlName)
        {
            List<AnimationData> list = (List<AnimationData>)SerialisationUtility.Load(AppData.StringPathContentRoot + xmlPath, xmlName + ".xml", typeof(List<AnimationData>));

            Dictionary<string, AnimationData> dictionary = new Dictionary<string, AnimationData>();

            foreach (AnimationData animationData in list)
            {
                if (!dictionary.ContainsKey(animationData.name))
                {
                    dictionary.Add(animationData.name, animationData);
                }
            }

            return dictionary;
        }
        private void LoadTextures()
        {
            //sky
            this.textureDictionary.Add(AppData.StringPathSky + "cloud1");
            this.textureDictionary.Add(AppData.StringPathSky + "cloud2");
            this.textureDictionary.Add(AppData.StringPathSky + "cloud3");


            //terrain
            this.textureDictionary.Add(AppData.StringPathTerrain + "grass");
            this.textureDictionary.Add(AppData.StringPathTerrain + "grassCenter");
            this.textureDictionary.Add(AppData.StringPathTerrain + "grassCliffLeft");
            this.textureDictionary.Add(AppData.StringPathTerrain + "grassCliffRight");
            this.textureDictionary.Add(AppData.StringPathTerrain + "grassHalfLeft");
            this.textureDictionary.Add(AppData.StringPathTerrain + "grassHalfMid");
            this.textureDictionary.Add(AppData.StringPathTerrain + "grassHalfRight");
            this.textureDictionary.Add(AppData.StringPathTerrain + "liquidWater");
            this.textureDictionary.Add(AppData.StringPathTerrain + "liquidWaterTop");

            //sky
            this.textureDictionary.Add(AppData.StringPathFoliage + "cactus");

            //animated
            this.textureDictionary.Add(AppData.StringPathAnimated + "debuganimation");
            this.textureDictionary.Add(AppData.StringPathAnimated + "playerwalksheet");

            //debug
            this.textureDictionary.Add(AppData.StringPathDebug + "debugrectangle");
            this.textureDictionary.Add(AppData.StringPathDebug + "debug");

            //menu
            this.textureDictionary.Add(AppData.StringPathMenu + "mainmenu2");
            this.textureDictionary.Add(AppData.StringPathMenu + "mainmenu");
            this.textureDictionary.Add(AppData.StringPathMenu + "audiomenu");
            this.textureDictionary.Add(AppData.StringPathMenu + "white");

            //levels
            this.textureDictionary.Add(AppData.StringPathLevels + "level1");
            //  this.textureDictionary.Add(AppData.StringPathLevels + "level2");
        }

        private void LoadFonts()
        {
            this.fontDictionary.Add(AppData.StringPathFonts + "hud");
            this.fontDictionary.Add(AppData.StringPathFonts + "menu");
            this.fontDictionary.Add(AppData.StringPathFonts + "debug");
        }


        private void LoadSounds()
        {
            this.soundManager.Add(new SoundEffectInfo(this,"boing_spring", "Assets/Audio/boing_spring", 1, 0.5f, 0, false));

            this.soundManager.Add(new SoundEffectInfo(this,"start", "Assets/Audio/start", 0.2f, 0.5f, 0, true));
        }

        private void LoadCurves()
        {
            Transform2DCurve transform2DCurve = null;
            
            transform2DCurve = new Transform2DCurve(CurveLoopType.Oscillate);
            transform2DCurve.Add(new Vector2(100, 100), new Vector2(1, 1), 0, 0);
            transform2DCurve.Add(new Vector2(500, 500), new Vector2(1, 1), 180, 5);
            transform2DCurve.Add(new Vector2(700, 200), new Vector2(1, 1), 180, 10);
            this.curveDictionary.Add(AppData.CurveNamePlatformHorizontal1, transform2DCurve);

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        #endregion

        #region Update & Draw
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            base.Update(gameTime);
        }

     

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        #endregion

        #region Demo & Debug

        private void InitialiseDebugDrawer(DebugOptionsType debugOptions)
        {
            if (debugOptions != 0)
            {
                this.debugDrawer2D = new DebugDrawer2D(this, debugOptions, this.textureDictionary["debug"], DebugData.ColorBounds, this.fontDictionary["debug"], DebugData.ColorText);
                //set draw order high so between spriteManager and hudManager the debug rectangles are always shown
                this.debugDrawer2D.DrawOrder = DebugData.DrawOrderDebugManager;
                Components.Add(this.debugDrawer2D);
            }
        }

        private void demoPathFinding()
        {
            PathFindingEngine engine = new PathFindingEngine("path finder"); //step 1

            engine.addNode(new Node("a", new Vector2(100, 200))); //step 2
            engine.addNode(new Node("b", new Vector2(200, 200)));
            engine.addNode(new Node("c", new Vector2(300, 200)));
            engine.addNode(new Node("d", new Vector2(300, 400)));
            engine.addNode(new Node("e", new Vector2(500, 700)));

            engine.addEdge("a", "b", 4); //step 3
            engine.addEdge("b", "c", 5);
            engine.addEdge("c", "d", 2);
            engine.addEdge("d", "e", 1);

            engine.Initialise(); //step 4

            //-----------------------------------
            engine.setStartNode("a"); //step 5

            List<Node> list = engine.getPath("e"); //step 6

            PathFindingEngine.printPath(list);

            string id = engine.findNearestNode(new Vector2(300, 210));

            System.Diagnostics.Debug.WriteLine(id);
        }

        #endregion


    }
}
