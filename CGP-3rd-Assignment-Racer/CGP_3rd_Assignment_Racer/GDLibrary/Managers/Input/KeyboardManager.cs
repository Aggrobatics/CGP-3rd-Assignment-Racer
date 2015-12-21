using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GDGame;


namespace GDLibrary
{
    public class KeyboardManager : GameComponent
    {
        private KeyboardState newState, oldState;

        public KeyboardManager(Main game)
            : base(game)
        {
            //storing the state from current update
            this.newState = Keyboard.GetState();
        }
        public override void Update(GameTime gameTime)
        {
            //storing state from previous update
            this.oldState = this.newState;
            //storing the state from current update
            this.newState = Keyboard.GetState();
            base.Update(gameTime);            
        }
        //IsKeyDown/Up, IsKeyFirstPress, IsAnyKeyPressed
        public bool IsKeyDown(Keys aKey)
        {
            return this.newState.IsKeyDown(aKey);
        }
        public bool IsFirstKeyPress(Keys aKey)
        {
            return (this.oldState.IsKeyUp(aKey)
                && this.newState.IsKeyDown(aKey));
        }
        public bool IsAnyKeyPressed()
        {
            return (this.newState.GetPressedKeys().Length > 0) 
                ? true : false;
        }
        public Keys[] GetAllKeysPressed()
        {
            return this.newState.GetPressedKeys();
        }
    }
}
