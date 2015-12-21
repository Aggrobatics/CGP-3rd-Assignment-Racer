using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;

namespace GDLibrary
{
    public class ControllerList
    {
        #region Fields
        private string name;
        private List<IController> list;
        #endregion

        //temp vars
        private IController controller;

        #region Properties
        public List<IController> List
        {
            get
            {
                return this.list;
            }
        }
        #endregion

        public ControllerList(string name)
        {
            this.name = name;
            this.list = new List<IController>();
        }

        //add a controller
        public void Add(IController controller)
        {
            if (!this.list.Contains(controller))
                this.list.Add(controller);
        }

        //remove a controller
        public void Remove(ControllerType controllerType)
        {
            controller = GetByType(controllerType);
            if (controller != null)
                this.list.Remove(controller);
        }

        public IController GetByType(ControllerType controllerType)
        {
            for (int i = 0; i < list.Count; i++)
            {
                controller = list[i];
                if (controller.GetControllerType().Equals(controllerType))
                {
                    return controller;
                }
            }

            return null;
        }

        public void Clear()
        {
            if (this.list.Count != 0)
                this.list.Clear();
        }

        public int Count()
        {
            return this.list.Count;
        }

        //enable/disable all controllers
        public void SetAllStatusTo(bool bEnabled)
        {
            foreach (IController controller in this.list)
                controller.SetEnabled(bEnabled);
        }

        //enable/disable a controller by type
        public void SetStatusTo(ControllerType controllerType, bool bEnabled)
        {
              controller = GetByType(controllerType);
              if (controller != null)
                controller.SetEnabled(bEnabled);
        }

        //called by the pawnsprite to update all controllers for the sprite
        public void Update(GameTime gameTime)
        {
            foreach (IController controller in this.list)
            {
                if(controller.IsEnabled()) //means we can disable the controller like a on, off, or pause function
                    controller.Update(gameTime);
            }
        }

        /*
         * Since cloned lists will contain controllers and controllers all have a parent
         * When we clone a list we must be able to re-define the parent actor.
         * Otherwise the new controller will target the old parent actor (i.e. the source object being cloned)
         */
        public ControllerList GetDeepCopyNewParent(IActor parentActor)
        {
            IController cloneController = null;
            ControllerList cloneList = new ControllerList("clone - " + this.name);

            foreach(IController controller in this.list)
            {
                cloneController = (IController)controller.Clone();
                cloneController.SetParentActor(parentActor);
                cloneList.Add(cloneController);
            }

            return cloneList;
        }

        
    }
}
