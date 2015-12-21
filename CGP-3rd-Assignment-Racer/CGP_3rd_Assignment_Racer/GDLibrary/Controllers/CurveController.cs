using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GDLibrary
{
    public class CurveController : Controller
    {
        #region Fields
        private Transform2DCurve transform2DCurve;
        private Transform2DCurveOffsets offsets;
        #endregion

        //temp vars
        Vector2 position, scale;
        float rotation;
        private float elapsedTime;
        private bool bFirstTime;

        #region Properties
        public Transform2DCurve Transform2DCurve
        {
            get
            {
                return this.transform2DCurve;
            }
            set
            {
                this.transform2DCurve = value;
            }
        }
        public Transform2DCurveOffsets Transform2DCurveOffsets
        {
            get
            {
                return this.offsets;
            }
            set
            {
                this.offsets = value;
            }
        }
        #endregion

        public CurveController(IActor parentActor, ControllerType controllerType, bool bEnabled,
            Transform2DCurve transform2DCurve, Transform2DCurveOffsets offsets)
            : base(parentActor, controllerType, bEnabled)
        {
            this.transform2DCurve = transform2DCurve;
            this.offsets = offsets;
            this.bFirstTime = true; //used for time offsets
        }

        //no offsets specified
        public CurveController(IActor parentActor, ControllerType controllerType, bool bEnabled,
            Transform2DCurve transform2DCurve)
            : this(parentActor, controllerType, bEnabled, transform2DCurve, Transform2DCurveOffsets.Zero)
        {

        }


        public override void Update(GameTime gameTime)
        {
            if (offsets.TimeInMs != 0 && this.bFirstTime) //adds the time offset first time in to kick the controller forward at the start of its run
            {
                elapsedTime += offsets.TimeInMs;
                this.bFirstTime = false;
            }

            elapsedTime += gameTime.ElapsedGameTime.Milliseconds;
            this.transform2DCurve.Evalulate(elapsedTime, 1, out position, out scale, out rotation);

            this.ParentActor.GetTransform().Position = position + offsets.Position;
            this.ParentActor.GetTransform().Rotation = rotation + offsets.Rotation;
            this.ParentActor.GetTransform().Scale = scale * offsets.Scale;
        }

        public override object Clone()
        {
            return new CurveController(this.ParentActor,
                this.ControllerType, this.Enabled, this.transform2DCurve, this.offsets);
        }
    }
}
