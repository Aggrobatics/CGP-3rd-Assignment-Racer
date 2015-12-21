using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GDLibrary
{
    public class TextureParameters : ICloneable
    {
        #region Fields
        private string name;
        private Texture2D texture;
        private float layerDepth, originalLayerDepth;
        private SpriteEffects spriteEffects, originalSpriteEffects;
        private Rectangle sourceRectangle, originalSourceRectangle;
        private Color color, originalColor;
        #endregion

        #region Properties
        public string Name
        {
            get
            {
                return this.name;
            }
        }
        public Texture2D Texture
        {
            get
            {
                return this.texture;
            }
            set
            {
                this.texture = value;
            }
        }
        public float LayerDepth
        {
            get
            {
                return this.layerDepth;
            }
            set
            {
                this.layerDepth = (value >= 0 && (value <= 1)) ? value : 1; //1 == front, 0 == back
            }
        }
        public SpriteEffects SpriteEffects
        {
            get
            {
                return this.spriteEffects;
            }
            set
            {
                this.spriteEffects = value;
            }
        }
        public Color Color
        {
            get
            {
                return this.color;
            }
            set
            {
                this.color = value;
            }
        }
        public Rectangle SourceRectangle
        {
            get
            {
                return this.sourceRectangle;
            }
            set
            {
                this.sourceRectangle = value;
            }
        }
        public Vector2 Origin
        {
            get
            {
                return new Vector2(this.sourceRectangle.Width / 2.0f, this.sourceRectangle.Height / 2.0f);
            }

        }
        public Integer2 Dimensions
        {
            get
            {
                return new Integer2(this.sourceRectangle.Width, this.sourceRectangle.Height);
            }
        }
        #endregion

        //used when we draw all the texture e.g. background
        public TextureParameters(Texture2D texture, SpriteEffects spriteEffects, Color color, float layerDepth)
            : this(texture, new Rectangle(0, 0, texture.Width, texture.Height), spriteEffects, color, layerDepth)
        {

        }


        public TextureParameters(Texture2D texture, Rectangle sourceRectangle, 
            SpriteEffects spriteEffects, Color color, float layerDepth)
        {
            //set 
            Set(texture, sourceRectangle, spriteEffects, color, layerDepth);

            //store defaults used for reset
            SetDefaults(sourceRectangle, spriteEffects, color, layerDepth);
        }

        private void SetDefaults(Rectangle sourceRectangle, Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects, Microsoft.Xna.Framework.Color color, float layerDepth)
        {
            this.originalSourceRectangle = sourceRectangle;
            this.originalSpriteEffects = spriteEffects;
            this.originalColor = color;
            this.originalLayerDepth = layerDepth;
        }

        public void Set(Texture2D texture, Rectangle sourceRectangle,
            SpriteEffects spriteEffects, Color color, float layerDepth)
        {
            this.name = texture.Name;
            this.texture = texture;
            this.sourceRectangle = sourceRectangle;
            this.spriteEffects = spriteEffects;
            this.color = color;
            //call property to set 0-1 layer depth
            this.LayerDepth = layerDepth;
        }

        //used to reset sprite to it's original parameters
        public void Reset()
        {
            Set(this.texture, originalSourceRectangle, originalSpriteEffects, originalColor, originalLayerDepth);
        }

        public object Clone()
        {
            return new TextureParameters(this.texture, this.sourceRectangle, this.spriteEffects, this.color, this.layerDepth);
        }

        //calculates the scale factor required to make sprite fit a target dimension e.g. screen background
        public Vector2 GetScaleFactorToFit(Vector2 targetDimensions)
        {
           return new Vector2(targetDimensions.X / texture.Width, targetDimensions.Y / texture.Height);
        }

        public Vector2 GetScaleFactorToFit(int width, int height)
        {
            return new Vector2(width / texture.Width, height / texture.Height);
        }
    }
}
