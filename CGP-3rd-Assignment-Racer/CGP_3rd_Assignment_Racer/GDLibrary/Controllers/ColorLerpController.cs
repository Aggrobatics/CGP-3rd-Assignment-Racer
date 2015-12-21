using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GDLibrary
{
    public class ColorLerpController : Controller
    {
        #region Fields
        private Color hiColor, loColor;
        private float lerpSpeed;
        #endregion

        #region Properties
        public Color HiColor
        {
            get
            {
                return this.hiColor;
            }
            set
            {
                this.hiColor = value;
            }
        }
        public Color LoColor
        {
            get
            {
                return this.loColor;
            }
            set
            {
                this.loColor = value;
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

        public ColorLerpController(IActor parentActor, ControllerType controllerType, bool bEnabled, 
            Color loColor, Color hiColor, float lerpSpeed)
            : base(parentActor, controllerType, bEnabled)
        {
            this.loColor = loColor;
            this.hiColor = hiColor;
            this.lerpSpeed = lerpSpeed;
        }

        public override void Update(GameTime gameTime)
        {
            this.ParentActor.GetTextureParameters().Color = ColorUtility.LerpBySineElapsedTime(this.loColor, this.hiColor, gameTime, lerpSpeed);
        }

        public override object Clone()
        {
            return new ColorLerpController(this.ParentActor,
                this.ControllerType, this.Enabled, this.loColor, this.hiColor, this.lerpSpeed);
        }
    }
}
