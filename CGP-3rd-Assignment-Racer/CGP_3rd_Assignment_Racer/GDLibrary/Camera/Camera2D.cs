using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GDLibrary
{
    public class Camera2D : IActor
    {
        #region Fields
        private string id;
        private CameraTransform2D transform;
        private ControllerList controllerList;
        #endregion

        #region Properties
        public Viewport Viewport
        {
            get
            {
                return this.transform.Viewport;
            }

        }

        private CameraTransform2D Transform
        {
            get
            {
                return this.transform;
            }
            set
            {
                this.transform = value;
            }
        }
        #endregion

        public Camera2D(string id, CameraTransform2D transform)
        {
            this.id = id;
            this.transform = transform;
            this.controllerList = new ControllerList(id + " controller list");
        }

        public virtual void Update(GameTime gameTime)
        {
            if (this.controllerList != null)
                this.controllerList.Update(gameTime);

            this.Transform.Update();
        }

        public virtual void Reset()
        {
            this.transform.Reset();
        }

        public int GetBSP()
        {
            return this.transform.BSPSector;
        }

        public Transform2D GetTransform()
        {
            return this.transform;
        }

        public ControllerList GetControllerList()
        {
            return this.controllerList;
        }

        //design flaw
        public TextureParameters GetTextureParameters()
        {
            return null;
        }
    }
}
