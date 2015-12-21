using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using GDGame;

namespace GDLibrary
{
    public class SpriteManager : DrawableGameComponent
    {
        #region Fields
        private Main game;
        private List<Sprite> collidableList, nonCollidableList, removeList;
        private bool bPaused = false;
        #endregion


        //temp vars
        private Transform2D transform2D;
        private TextureParameters textureParameters;

        #region Properties
        public bool Paused
        {
            get
            {
                return this.bPaused;
            }
            set
            {
                this.bPaused = value;
                //show/hide mouse
                game.ScreenManager.IsMouseVisible = this.Paused;

                if (this.Paused)
                    this.game.ScreenManager.SetCameras(CameraLayoutType.FullScreen);
                else
                    this.game.ScreenManager.SetCameras(CameraLayoutType.SplitVertical);
            }
        }
        public List<Sprite> CollidableList
        {
            get
            {
                return this.collidableList;
            }
        }
        public List<Sprite> NonCollidableList
        {
            get
            {
                return this.nonCollidableList;
            }
        }
        #endregion

        public SpriteManager(Main game)
            : base(game)
        {
            this.game = game;
            this.collidableList = new List<Sprite>(); 
            this.nonCollidableList = new List<Sprite>();
            this.removeList = new List<Sprite>(); //sprites that are queued for removal (e.g. enemy where health == 0)
        }

        public void Add(Sprite s)
        {
            if (s.IsCollidable)
                this.collidableList.Add(s);
            else
                this.nonCollidableList.Add(s);
        }

        public void AddAll(List<Sprite> list)
        {
            foreach (Sprite s in list)
                Add(s);
        }

        public void Remove(Sprite s)
        {
            this.removeList.Add(s);
        }

        public void ClearAll()
        {
            ClearCollidable();
            ClearNonCollidable();
        }
        
        public void ClearCollidable()
        {
            this.collidableList.Clear();
        }
        
        public void ClearNonCollidable()
        {
            this.nonCollidableList.Clear();
        }

        //used to pause draw/update when we show/hide the menu
        public bool TogglePause()
        {
            this.bPaused = !bPaused;
            return this.bPaused;
        }


        public override void Update(GameTime gameTime)
        {
           if (!bPaused)
            {
                ProcessRemoveList();

                for (int i = 0; i < this.collidableList.Count; i++)
                {
                    this.collidableList[i].Update(gameTime);
                }

                for (int i = 0; i < this.nonCollidableList.Count; i++)
                {
                    this.nonCollidableList[i].Update(gameTime);
                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// Formally removes all sprites in the removeList from the update
        /// </summary>
        private void ProcessRemoveList()
        {
            foreach(Sprite s in this.removeList)
            {
                if (s.IsCollidable)
                    this.collidableList.Remove(s);
                else
                    this.nonCollidableList.Remove(s);
            }

            this.removeList.Clear();
        }

        Camera2D camera2D;
        Viewport viewport;
        CameraTransform2D transform;
        public override void Draw(GameTime gameTime)
        {
            if (!bPaused)
            {

                for (int i = 0; i < this.game.CameraManager.Size(); i++)
                {
                    camera2D = this.game.CameraManager[i];               
                    this.game.GraphicsDevice.Viewport = camera2D.Viewport;

                    this.game.SpriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, 
                        null, DepthStencilState.Default, null, null, camera2D.GetTransform().World);

                    foreach (Sprite s in this.nonCollidableList)
                        DrawSprite(s);

                    foreach (Sprite s in this.collidableList)
                        DrawSprite(s);

                    this.game.SpriteBatch.End();
                }






            }
            base.Draw(gameTime);
        }

        //notice that we've moved the actual draw method outside of the sprite to the sprite manager
        private void DrawSprite(Sprite s)
        {
            if (s.IsVisible) //if we can see the sprite then draw it - used to only draw what camera can see
            {
                transform2D = s.Transform;
                textureParameters = s.TextureParameters;

                this.game.SpriteBatch.Draw(textureParameters.Texture, transform2D.Position,
                    textureParameters.SourceRectangle, textureParameters.Color,
                    transform2D.RotationInRadians, transform2D.Origin, transform2D.Scale,
                    textureParameters.SpriteEffects, textureParameters.LayerDepth);
            }
        }

  
    }
}
