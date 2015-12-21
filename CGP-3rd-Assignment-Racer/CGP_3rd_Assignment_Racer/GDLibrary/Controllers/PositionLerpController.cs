using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GDLibrary
{
    public class PositionLerpController : Controller
    {
        #region Fields
        private Vector2 hiPosition, loPosition;
        private float lerpSpeed;
        #endregion

        #region Properties
        public Vector2 HiPosition
        {
            get
            {
                return this.hiPosition;
            }
            set
            {
                this.hiPosition = value;
            }
        }
        public float LerpSpeed
        {
            get
            {
                return this.lerpSpeed;
            }
            set
            {
                this.lerpSpeed = value;
            }
        }
        #endregion

        public PositionLerpController(IActor parentActor, ControllerType controllerType, bool bEnabled,
            Vector2 hiPosition, float lerpSpeed)
            : base(parentActor, controllerType, bEnabled)
        {
            this.loPosition = this.ParentActor.GetTransform().Position;
            this.hiPosition = hiPosition;
            this.lerpSpeed = lerpSpeed;
        }

        public override void Update(GameTime gameTime)
        {
            this.ParentActor.GetTransform().Position = ColorUtility.LerpBySineElapsedTime(this.loPosition, this.hiPosition, gameTime, lerpSpeed);
        }

        public override object Clone()
        {
            return new PositionLerpController(this.ParentActor,
                this.ControllerType, this.Enabled, this.hiPosition, this.lerpSpeed);
        }

    }
}
