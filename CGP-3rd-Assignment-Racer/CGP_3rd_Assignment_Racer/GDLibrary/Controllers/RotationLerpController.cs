using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GDLibrary
{
    public class RotationLerpController : Controller
    {
        #region Fields
        private float rotation;
        #endregion

        #region Properties
        public float Rotation
        {
            get
            {
                return this.rotation;
            }
            set
            {
                this.rotation = value;
            }
        }
        #endregion

        public RotationLerpController(IActor parentActor, ControllerType controllerType, bool bEnabled,
            float rotation)
            : base(parentActor, controllerType, bEnabled)
        {
            this.rotation = rotation;
        }

        public override void Update(GameTime gameTime)
        {
            this.ParentActor.GetTransform().Rotation += rotation;
        }

        public override object Clone()
        {
            return new RotationLerpController(this.ParentActor,
                this.ControllerType, this.Enabled, this.rotation);
        }

    }
}
