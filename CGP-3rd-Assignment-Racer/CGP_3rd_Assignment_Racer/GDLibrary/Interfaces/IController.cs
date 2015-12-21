using Microsoft.Xna.Framework;
using System;
namespace GDLibrary
{
    public interface IController : ICloneable
    {
        bool IsEnabled();               //check status
        void SetEnabled(bool bEnabled); //turn on, off controllers
        bool Toggle();

        ControllerType GetControllerType();       //find by type
        void Update(GameTime gameTime);
        void Reset();

        IActor GetParentActor();
        void SetParentActor(IActor actor);
    }
}
