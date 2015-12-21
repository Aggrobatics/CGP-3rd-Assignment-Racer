using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GDLibrary
{
    public class DistanceActivatedController : Controller
    {
        private Transform2D parentTransform;
        private Vector2 originalPosition;
        private MouseManager mouseManager;
        private Vector2 moveDirection;
        private float moveSpeed;
        private float maxDisplacement;
        private float activationDistance;
        private Vector2 finalPosition;
        public DistanceActivatedController(IActor parentActor, 
            ControllerType controllerType, bool bEnabled, 
            Vector2 moveDirection, float moveSpeed, float maxDisplcement, float activationDistance, MouseManager mouseManager)
            :base(parentActor, controllerType, bEnabled)
        {
            this.mouseManager = mouseManager;
            this.moveDirection = moveDirection;
            this.moveSpeed = moveSpeed;
            this.maxDisplacement = maxDisplcement;
            this.activationDistance = activationDistance;
            this.parentTransform = this.ParentActor.GetTransform();
            this.originalPosition = this.parentTransform.Position;

            this.finalPosition = this.parentTransform.Position + this.maxDisplacement * this.moveDirection;
        }


        public override void Update(GameTime gameTime)
        {
            // if target is within a certain distance then displace
            Vector2 mousePosition = this.mouseManager.GetPosition();
            float distance = Vector2.Distance(this.parentTransform.Position, mousePosition);
            
            if(distance <= this.activationDistance)
            {
                float lerpFactor = distance / this.activationDistance;
                this.parentTransform.Position = 
                    Vector2.Lerp(this.finalPosition, this.originalPosition, lerpFactor);
            
            }


        }
    }

}
