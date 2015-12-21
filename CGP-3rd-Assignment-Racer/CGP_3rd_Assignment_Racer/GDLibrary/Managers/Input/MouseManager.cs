using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GDGame;

namespace GDLibrary
{
    public class MouseManager : GameComponent
    {
        private MouseState newState, oldState;

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle(newState.X, newState.Y, 1, 1);
             }
        }
        public bool IsMouseOver(Rectangle r)
        {
            return this.Bounds.Intersects(r);
        }

        public MouseManager(Main game)
            : base(game)
        {
            this.newState = Mouse.GetState();
        }
        public override void Update(GameTime gameTime)
        {
            this.oldState = this.newState;
            this.newState = Mouse.GetState();
            base.Update(gameTime);            
        }

        //IsLeftClick, IsRightClick
        public bool IsLeftClick()
        {
            return this.newState.LeftButton
                == ButtonState.Pressed;
        }
        public bool IsRightClick()
        {
            return this.newState.RightButton
                == ButtonState.Pressed;
        }
        //IsLeftFirstClick
        public bool IsLeftFirstClick()
        {
            return (this.newState.LeftButton
                == ButtonState.Pressed)
                && (this.oldState.LeftButton
                == ButtonState.Released);
        }
       
        //IsRightFirstClick
        public bool IsRightFirstClick()
        {
            return (this.newState.RightButton
                == ButtonState.Pressed)
                && (this.oldState.RightButton
                == ButtonState.Released);
        }
        //GetPosition
        public Vector2 GetPosition()
        {
            return new Vector2(this.newState.X, this.newState.Y);
        }

        //GetWheelDelta
        public int GetWheelDelta()
        {
            return (this.newState.ScrollWheelValue
                - this.oldState.ScrollWheelValue);
        }

        public bool IsWheelScrollPositive()
        {
            return ((this.newState.ScrollWheelValue
                - this.oldState.ScrollWheelValue) > 0) ? true : false;
        }      
    }
}
