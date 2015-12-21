using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GDGame;

namespace GDLibrary
{
    /// <summary>
    /// Draws a debug rectangle to visualise the bounding area of any collidable sprite
    /// </summary>
    public class DebugDrawer2D : DrawableGameComponent
    {
        #region Fields
        private Main game;

        //defined by the user in Main::Initialise()
        private DebugOptionsType debugOptions;

        //debug settings for rectangle and text colors and their colors
        private Texture2D debugTexture;
        private SpriteFont debugFont;
        private Color debugFontColor, debugTextureColor;

        //used to estimate FPS
        private int elapsedTimeInMs, fpsCount;
        private string fpsText = "";


        //temp debug data on collidable and non-collidable count
        string collidableText, nonCollidableText;
        private Camera2D camera2D;



        #endregion

        #region Properties

        #endregion

        public DebugDrawer2D(Main game, DebugOptionsType debugOptions, Texture2D debugTexture, Color debugTextureColor, SpriteFont debugFont, Color debugFontColor)
            : base(game)
        {
            this.game = game;
            this.debugOptions = debugOptions;

            this.debugTexture = debugTexture;
            this.debugTextureColor = debugTextureColor;

            this.debugFontColor = debugFontColor;
            this.debugFont = debugFont;

        }


        public override void Update(GameTime gameTime)
        {
            UpdateFrameRate(gameTime);

            base.Update(gameTime);
        }

        private void UpdateFrameRate(GameTime gameTime)
        {
            if ((this.debugOptions & DebugOptionsType.DrawFPS) != 0)
            {
                this.fpsCount++;
                this.elapsedTimeInMs += gameTime.ElapsedGameTime.Milliseconds;
                if (elapsedTimeInMs >= 1000)
                {
                    this.elapsedTimeInMs = 0;
                    this.fpsText = "FPS: " + this.fpsCount;
                    this.fpsCount = 0;
                }
            }
        }



        public override void Draw(GameTime gameTime)
        {
            for (int i = 0; i < this.game.CameraManager.Size(); i++)
            {
                camera2D = this.game.CameraManager[i];
                this.game.GraphicsDevice.Viewport = camera2D.Viewport;

                this.game.SpriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend,
               null, DepthStencilState.Default, null, null, camera2D.GetTransform().World);

                DrawFrameRate(gameTime);
                DrawBoundingBoxes();
                DrawBSPSectors();
                DrawColllisionData();

                this.game.SpriteBatch.End();
            }
            base.Draw(gameTime);
        }

        private void DrawFrameRate(GameTime gameTime)
        {
            if ((this.debugOptions & DebugOptionsType.DrawFPS) != 0)
            {
                this.game.SpriteBatch.DrawString(this.debugFont, this.fpsText, AppData.DebugFPSTextPosition, this.debugFontColor);
            }
        }

        private void DrawBSPSectors()
        {
            if ((this.debugOptions & DebugOptionsType.DrawBSPSectors) != 0)
            {
                foreach (BSPSector bspSector in game.ScreenManager)
                {
                    this.game.SpriteBatch.Draw(this.debugTexture, bspSector.Bounds, this.debugTextureColor);
                }
            }

            if ((this.debugOptions & DebugOptionsType.DrawBSPSectorData) != 0)
            {
                Vector2 textPosition;
                int textOffset = 10;

                foreach (BSPSector bspSector in game.ScreenManager)
                {
                    textPosition = new Vector2(bspSector.Bounds.X + textOffset, bspSector.Bounds.Y + textOffset);
                    this.game.SpriteBatch.DrawString(this.debugFont, bspSector.ToString(), textPosition, this.debugFontColor);
                }
            }
        }

        private void DrawBoundingBoxes()
        {
            if ((this.debugOptions & DebugOptionsType.DrawBoundingBoxes) != 0)
            {
                foreach(Sprite s in this.game.SpriteManager.CollidableList)
                {
                    this.game.SpriteBatch.Draw(this.debugTexture, s.Transform.Bounds, Color.Red);
                }
            }
        }

        private void DrawColllisionData()
        {
            if ((this.debugOptions & DebugOptionsType.DrawCollisionData) != 0)
            {
                collidableText = AppData.DebugCollidableText + this.game.SpriteManager.CollidableList.Count;
                nonCollidableText = AppData.DebugNonCollidableText + this.game.SpriteManager.NonCollidableList.Count;

                this.game.SpriteBatch.DrawString(this.debugFont, collidableText, AppData.DebugCollidableTextPosition, this.debugFontColor);
                this.game.SpriteBatch.DrawString(this.debugFont, nonCollidableText, AppData.DebugNonCollidableTextPosition, this.debugFontColor);
            }
        }

    }
}
