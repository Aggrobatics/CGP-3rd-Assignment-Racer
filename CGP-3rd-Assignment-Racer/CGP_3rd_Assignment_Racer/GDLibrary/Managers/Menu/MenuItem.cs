using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GDGame;


namespace GDLibrary
{
    public class MenuItem : ICloneable
    {
        #region Fields
        private String text;
        private Color inactiveColor, activeColor;
        private Rectangle bounds;
        private Vector2 position;
        #endregion

        //temp vars
        private Color color;
        public bool bIsOver = false;
        private bool bMouseOver;
        private SpriteFont font;

        //set to update bounds because of text, position, or font changes
        private bool bDirty = true;

        #region Properties
        public bool IsMouseOver
        {
            get
            {
                return this.bMouseOver;
            }
        }
        public String Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
                this.bDirty = true;
            }
        }
        public SpriteFont Font
        {
            get
            {
                return this.font;
            }
            set
            {
                this.font = value;
                this.bDirty = true;
            }
        }
        public Color Color
        {
            get
            {
                return this.color;
            }
        }
        public Color InactiveColor
        {
            get
            {
                return this.inactiveColor;
            }
            set
            {
                this.inactiveColor = value;
            }
        }
        public Color ActiveColor
        {
            get
            {
                return this.activeColor;
            }
            set
            {
                this.activeColor = value;
            }
        }
        public Rectangle Bounds
        {
            get
            {
                if (this.bDirty)
                {
                    this.bounds = StringUtility.GetBoundsFromFont(this.font, this.text, this.position);
                    this.bDirty = false;
                }

                return this.bounds;
            }
        }
        public Vector2 Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
                this.bDirty = true;
            }
        }

        #endregion

        public MenuItem(SpriteFont font, String text, Vector2 position, Color inactiveColor, Color activeColor)
        {
            this.font = font;
            this.text = text;
            this.inactiveColor = inactiveColor;
            this.activeColor = activeColor;
            this.color = inactiveColor;
            this.Position = position;
        }


        public void HandleMouseOver(MouseManager mouseManager)
        {
            if(mouseManager.IsMouseOver(this.bounds)) //mouse is over this item
            {
                if (!this.bMouseOver) //only set color if it WAS set to INACTIVE
                {
                    this.color = this.activeColor;
                    this.bMouseOver = true;
                }
            }
            else //mouse is not over this item
            {
               if(this.bMouseOver) //only set color if it WAS set to ACTIVE
               {
                    this.color = this.inactiveColor;
                    this.bMouseOver = false;
                }
            }
        }

        public bool Update(GameTime gameTime, MouseManager mouseManager) 
        {
            //test for collisions with the mouse pointer
            HandleMouseOver(mouseManager);

            //if the user has clicked this item then return true
            if (this.bMouseOver && mouseManager.IsLeftFirstClick())
                return true;
            
            //no mouse over and no click
            return false;
        }

        public object Clone()
        {
            //all the variables are C# or XNA data types i.e. no custom (i.e. user-defined) data types
            return this.MemberwiseClone();
        }
    }
}
