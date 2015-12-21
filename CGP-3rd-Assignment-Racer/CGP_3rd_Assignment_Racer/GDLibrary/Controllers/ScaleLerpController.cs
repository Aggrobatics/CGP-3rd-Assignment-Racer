using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GDLibrary
{
    public class ScaleLerpController : Controller
    {
        #region Fields
        private Vector2 hiScale, loScale;
        private float lerpSpeed;
        #endregion

        #region Properties

        public Vector2 HiScale
        {
            get
            {
                return this.hiScale;
            }
            set
            {
                this.hiScale = value;
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

        public ScaleLerpController(IActor parentActor, ControllerType controllerType, bool bEnabled, 
            Vector2 hiScale, float lerpSpeed)
            : base(parentActor, controllerType, bEnabled)
        {
            this.loScale = this.ParentActor.GetTransform().Scale;
            this.hiScale = hiScale;
            this.lerpSpeed = lerpSpeed;
        }

        public override void Update(GameTime gameTime)
        {
            this.ParentActor.GetTransform().Scale = ColorUtility.LerpBySineElapsedTime(this.loScale, this.hiScale, gameTime, lerpSpeed);
        }

        public override object Clone()
        {
            return new ScaleLerpController(this.ParentActor,
                this.ControllerType, this.Enabled, this.hiScale,  this.lerpSpeed);
        }

    }
}
