using System.Collections.Generic;
using Microsoft.Xna.Framework;
using GDGame;
using System.Collections;
using Microsoft.Xna.Framework.Graphics;

namespace GDLibrary
{
    public class ScreenManager : IEnumerable<BSPSector>
    {
        #region Fields
        private Main game;
        private Rectangle screenBounds;
        private Integer2 screenDimensions;
        private List<BSPSector> sectorList;
        private BSPSectorLayoutType bspLayoutType;
        private bool isMouseVisible, isFixedTimeStep;
        private Viewport originalViewPort;
        private CameraLayoutType currentCameraLayout;

        #endregion

        #region Properties
        public Viewport ViewPort 
        {
            get
            {
                return new Viewport(0, 0,
                    (int)this.screenDimensions.X,
                     (int)this.screenDimensions.Y);
            }
        }
        public Vector2 ScreenCentre
        {
            get
            {
                return new Vector2(this.screenDimensions.X / 2,
                                this.screenDimensions.Y / 2);
            }
        }

        public bool IsFixedTimeStep
        {
            get
            {
                return this.isFixedTimeStep;
            }
            set
            {
                this.isFixedTimeStep = value;
                this.game.IsFixedTimeStep = value;
            }

        }

        public bool IsMouseVisible
        {
            get
            {
                return this.isMouseVisible;
            }
            set
            {
                this.isMouseVisible = value;
                this.game.IsMouseVisible = value;
            }

        }

        public int Count
        {
            get
            {
                return this.sectorList.Count;
            }
        }

        public BSPSector this[int index] //used to visualise the BSP sectors - see DebugDrawer2D
        {
            get
            {
                return this.sectorList[index];
            }
        }

        public Rectangle ScreenBounds
        {
            get
            {
                return this.screenBounds;
            }
        }
        public Integer2 ScreenDimensions
        {
            get
            {
                return this.screenDimensions;
            }
        }
        #endregion

        public ScreenManager(Main game, string title, 
            ResolutionType resolutionType, CameraLayoutType cameraLayoutType,
            bool isMouseVisible, bool isFixedTimeStep, BSPSectorLayoutType bspLayoutType)
        {
            this.game = game;

            //set game title in the blue bar at the top of the screen
            this.game.Window.Title = title;
            
            //show/hide the mouse
            this.IsMouseVisible = isMouseVisible;

            //allows us to unlock the frame rate
            this.IsFixedTimeStep = false;

            //set resolution and BSPs
            SetResolution(resolutionType, bspLayoutType);

            //record so we can reset between full and split
            this.originalViewPort = this.game.Viewport;

            //set the split
            SetCameras(cameraLayoutType);

         }

        #region Cameras
        public void SetCameras(CameraLayoutType cameraLayoutType)
        {
            //only call if it represent a layout change
            if (this.currentCameraLayout != cameraLayoutType)
            {
                //remove all existing cameras - bug - switching into menu during gamplay resets the cameras 
                this.game.CameraManager.Clear();

                if (cameraLayoutType == CameraLayoutType.SplitVertical)
                    LoadVerticalCameras();
                else if (cameraLayoutType == CameraLayoutType.SplitHorizontal)
                    LoadHorizontalCameras();
                else
                    LoadFullScreenCamera();

                //record so we can compare new to old to prevent redundant call
                this.currentCameraLayout = cameraLayoutType;
            }
        }

        private void LoadFullScreenCamera()
        {
            Camera2D camera = null;
            CameraTransform2D cameraTransform2D = null;
            ControllerList cList = null;

            //camera 1
            cameraTransform2D = new CameraTransform2D(
                this.ScreenCentre,
                0, Vector2.One, Vector2.UnitX, -Vector2.UnitY,
                Vector2.Zero,
                this.ScreenDimensions,
                this.originalViewPort);

            camera = new Camera2D("full", cameraTransform2D);

            cList = camera.GetControllerList();
            cList.Add(new InputCameraController(camera, ControllerType.Camera, true,
                this.game.KeyboardManager, KeyData.KeysCameraOne, KeyData.CameraTranslateRate, KeyData.CameraRotateIncrement,
                KeyData.CameraScaleIncrement));
            this.game.CameraManager.Add(camera);
        }

        private void LoadHorizontalCameras()
        {
            Camera2D camera = null;
            CameraTransform2D cameraTransform2D = null;
            ControllerList cList = null;

            Viewport viewPortLeft = this.originalViewPort; //set to fullscreen then resize
            viewPortLeft.Height /= 2;

            Viewport viewPortRight = viewPortLeft;
            viewPortRight.Y += viewPortRight.Height;

            //camera 1
            cameraTransform2D = new CameraTransform2D(
                this.ScreenCentre,
                0, Vector2.One, Vector2.UnitX, -Vector2.UnitY,
                Vector2.Zero,
                this.ScreenDimensions,
                viewPortLeft);

            camera = new Camera2D("left", cameraTransform2D);

            cList = camera.GetControllerList();
            cList.Add(new InputCameraController(camera, ControllerType.Camera, true,
                this.game.KeyboardManager, KeyData.KeysCameraOne, KeyData.CameraTranslateRate, KeyData.CameraRotateIncrement,
                KeyData.CameraScaleIncrement));
            this.game.CameraManager.Add(camera);


            //camera 2   
            cameraTransform2D = new CameraTransform2D(
                this.ScreenCentre,
                0, Vector2.One, Vector2.UnitX, -Vector2.UnitY,
                Vector2.Zero,
                this.ScreenDimensions,
                viewPortRight);

            camera = new Camera2D("right", cameraTransform2D);

            cList = camera.GetControllerList();
            cList.Add(new InputCameraController(camera, ControllerType.Camera, true,
                this.game.KeyboardManager, KeyData.KeysCameraTwo, KeyData.CameraTranslateRate, KeyData.CameraRotateIncrement,
                KeyData.CameraScaleIncrement));

            this.game.CameraManager.Add(camera);
        }

        private void LoadVerticalCameras()
        {
            Camera2D camera = null;
            CameraTransform2D cameraTransform2D = null;
            ControllerList cList = null;

            Viewport viewPortLeft = this.originalViewPort; //set to fullscreen then resize
            viewPortLeft.Width /= 2;

            Viewport viewPortRight = viewPortLeft;
            viewPortRight.X += viewPortRight.Width;

            //camera 1
            cameraTransform2D = new CameraTransform2D(
                this.game.ScreenManager.ScreenCentre,
                0, Vector2.One, Vector2.UnitX, -Vector2.UnitY,
                Vector2.Zero,
                this.screenDimensions,
                viewPortLeft);

            camera = new Camera2D("left", cameraTransform2D);

            cList = camera.GetControllerList();
            cList.Add(new InputCameraController(camera, ControllerType.Camera, true,
                this.game.KeyboardManager, KeyData.KeysCameraOne, KeyData.CameraTranslateRate, KeyData.CameraRotateIncrement,
                KeyData.CameraScaleIncrement));
            this.game.CameraManager.Add(camera);


            //camera 2
            cameraTransform2D = new CameraTransform2D(
                this.game.ScreenManager.ScreenCentre,
                0, Vector2.One, Vector2.UnitX, -Vector2.UnitY,
                Vector2.Zero,
                this.screenDimensions,
                viewPortRight);

            camera = new Camera2D("right", cameraTransform2D);

            cList = camera.GetControllerList();
            cList.Add(new InputCameraController(camera, ControllerType.Camera, true,
                this.game.KeyboardManager, KeyData.KeysCameraTwo, KeyData.CameraTranslateRate, KeyData.CameraRotateIncrement,
                KeyData.CameraScaleIncrement));

            this.game.CameraManager.Add(camera);
        }

        #endregion












        //creates BSP sectors automatically based on specified layout
        private void SetBSPSpaces()
        {
            //first sector should always be 2^0 == pow(2,0) == 1
            BSPSector.startSectorIndex = 0;

            int sectorWidth = (int)(this.screenBounds.Width / this.bspLayoutType.CountHorizontal);
            int sectorHeight = (int)(this.screenBounds.Height / this.bspLayoutType.CountVertical);

            for (int row = 0; row < this.bspLayoutType.CountVertical; row++)
            {
                for (int col = 0; col < this.bspLayoutType.CountHorizontal; col++)
                {
                    this.sectorList.Add(new BSPSector(col * sectorWidth, row * sectorHeight, sectorWidth, sectorHeight));
                }
            }
        }

        public void SetResolution(ResolutionType resolutionType, BSPSectorLayoutType bspLayoutType)
        {

            if (this.sectorList == null)         //first time around
                this.sectorList = new List<BSPSector>(bspLayoutType.TotalSectors);
            else                                    //else if we call this again within the game to re-dimension the screen
                this.sectorList.Clear();

            this.screenDimensions = ScreenResolution.GetResolution(resolutionType);
            int width = screenDimensions.X;
            int height = screenDimensions.Y;
            //set the bounds of the screen based on resolution
            this.screenBounds = new Rectangle(0, 0, width, height);

            //set the screen resolution
            this.game.Graphics.PreferredBackBufferWidth = width;
            this.game.Graphics.PreferredBackBufferHeight = height;
            this.game.Graphics.ApplyChanges();

            //define type of BSP configuration e.g. 3 x 3 or 2 x 2 etc.
            this.bspLayoutType = bspLayoutType;

            //generate the BSP sectors from the layout passed in e.g. 2x2
            SetBSPSpaces();
        }

        //returns the BSP sector number for the supplied bounds
        public int GetBSP(Rectangle bounds)
        {
            int bsp = 0;
            foreach (BSPSector bspSector in this.sectorList)
            {
                if (bounds.Intersects(bspSector.Bounds))
                    bsp |= bspSector.Number;
            }
            return bsp;
        }

        public IEnumerator<BSPSector> GetEnumerator()
        {
            return this.sectorList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
