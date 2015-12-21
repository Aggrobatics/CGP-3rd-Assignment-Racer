using Microsoft.Xna.Framework;
namespace GDLibrary
{
    public class Controller : IController
    {
        #region Fields
        private IActor parentActor;
        private ControllerType controllerType;
        private bool bEnabled;
        #endregion

        #region Properties
        public bool Enabled
        {
            get
            {
                return this.bEnabled;
            }
            set
            {
                this.bEnabled = value;
            }
        }
        public ControllerType ControllerType
        {
            get
            {
                return this.controllerType;
            }
            set
            {
                this.controllerType = value;
            }
        }
        public IActor ParentActor
        {
            get
            {
                return this.parentActor;
            }
            set
            {
                this.parentActor = value;
            }
        }
        #endregion

        public Controller(IActor parentActor, ControllerType controllerType, bool bEnabled)
        {
            this.parentActor = parentActor;
            this.controllerType = controllerType;
            SetEnabled(bEnabled);
        }

        //set to on/off
        public void SetEnabled(bool bEnabled)
        {
            this.bEnabled = bEnabled;
        }

        //get status
        public bool IsEnabled()
        {
            return this.bEnabled;
        }

        //toggle on and off
        public bool Toggle()
        {
            this.bEnabled = !this.bEnabled;
            return this.bEnabled;
        }

        public ControllerType GetControllerType()
        {
            return this.controllerType;
        }

        public IActor GetParentActor()
        {
            return this.parentActor;
        }

        public void SetParentActor(IActor actor)
        {
            this.parentActor = actor;
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Reset()
        {

        }

        public virtual object Clone()
        {
            return new Controller(this.parentActor, this.controllerType, this.bEnabled);
        }
    }
}
