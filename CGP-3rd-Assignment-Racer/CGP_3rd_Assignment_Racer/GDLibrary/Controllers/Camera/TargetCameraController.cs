using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GDLibrary
{
    //used when we want the camera to follow a pawn sprite
    public class TargetCameraController : Controller
    {
        private Transform2D cameraTransform, parentTransform;

        public TargetCameraController(IActor parentActor, ControllerType controllerType, bool bEnabled, PawnSprite parentSprite)
            : base(parentActor, controllerType, bEnabled)
        {
            this.cameraTransform = parentActor.GetTransform();
            this.parentTransform = parentSprite.GetTransform();
        }

        public override void Update(GameTime gameTime)
        {
            this.cameraTransform.Position = parentTransform.Position;
            base.Update(gameTime);
        }

        //add clone...
    }
}
