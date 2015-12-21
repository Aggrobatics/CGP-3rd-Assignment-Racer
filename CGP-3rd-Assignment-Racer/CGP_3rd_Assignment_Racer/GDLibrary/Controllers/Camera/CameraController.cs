using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GDGame;

namespace GDLibrary
{
    //used when we want a player to be able to control the camera
    public class InputCameraController : Controller
    {
        private KeyboardManager keyboardManager;
        private IActor parentActor;
        private Transform2D parentTransform;

        private  Keys[] moveKeys;
        private float moveRate, rotateIncrement;
        private Vector2 scaleIncrement;

        public InputCameraController(IActor parentActor, ControllerType controllerType, bool bEnabled,
            KeyboardManager keyboardManager, Keys[] moveKeys, float moveRate, float rotateIncrement, Vector2 scaleIncrement)
            : base(parentActor, controllerType, bEnabled)
        {
            this.keyboardManager = keyboardManager;
            this.parentActor = parentActor;
            this.parentTransform = parentActor.GetTransform();

            this.moveKeys = moveKeys;
            this.moveRate = moveRate;
            this.rotateIncrement = rotateIncrement;
            this.scaleIncrement = scaleIncrement;
        }

        public override void Update(GameTime gameTime)
        {
            HandleInput(gameTime);
            base.Update(gameTime);
        }

        private void HandleInput(GameTime gameTime)
        {
            if (keyboardManager.IsKeyDown(this.moveKeys[KeyData.IndexCameraLeft]))
            {
                this.parentTransform.Position -=
               this.parentTransform.Look * gameTime.ElapsedGameTime.Milliseconds * this.moveRate;
            }
            else if (keyboardManager.IsKeyDown(this.moveKeys[KeyData.IndexCameraRight]))
            {
                this.parentTransform.Position +=
               this.parentTransform.Look * gameTime.ElapsedGameTime.Milliseconds * this.moveRate;
            }


            if (keyboardManager.IsKeyDown(this.moveKeys[KeyData.IndexCameraZoomIn]))
            {
                this.parentTransform.Scale += this.scaleIncrement;
            }
            else if (keyboardManager.IsKeyDown(this.moveKeys[KeyData.IndexCameraZoomOut]))
            {
                this.parentTransform.Scale -= this.scaleIncrement;
            }

            if (keyboardManager.IsKeyDown(this.moveKeys[KeyData.IndexCameraRotateLeft]))
            {
                this.parentTransform.Rotation += this.rotateIncrement;
            }
            else if (keyboardManager.IsKeyDown(this.moveKeys[KeyData.IndexCameraRotateRight]))
            {
                this.parentTransform.Rotation -= this.rotateIncrement;
            }

            if (keyboardManager.IsKeyDown(this.moveKeys[KeyData.IndexCameraReset]))
            {
                this.parentActor.Reset();
            }
        }

        //add clone...
    }
}
