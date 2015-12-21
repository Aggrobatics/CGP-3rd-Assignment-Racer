using Microsoft.Xna.Framework;
using System;

namespace GDLibrary
{
    public class Sprite : ICloneable
    {
        #region Fields
        private string id;
        private Transform2D transform;
        private TextureParameters textureParameters;
        private bool isCollidable, originalIsCollidable, isVisible,  originalIsVisible;
        private ActorType actorType = ActorType.Decorator;
        #endregion

        #region Properties
        public ActorType ActorType
        {
            get
            {
                return this.actorType;
            }
            set
            {
                this.actorType = value;
            }
        }
        public string ID
        {
            get
            {
                return this.id;
            }
        }
        public Transform2D Transform
        {
            get
            {
                return this.transform;
            }
        }
        public TextureParameters TextureParameters
        {
            get
            {
                return this.textureParameters;
            }
        }
        public bool IsCollidable
        {
            get
            {
                return this.isCollidable;
            }
            set
            {
                this.isCollidable = value;
            }
        }
        public bool IsVisible
        {
            get
            {
                return this.isVisible;
            }
            set
            {
                this.isVisible = value;
            }
        }
        #endregion

        public Sprite(string id, Transform2D transform,
            TextureParameters textureParameters, bool isCollidable, bool isVisible)
        {
            this.id = id.Trim();
            this.transform = transform;
            this.textureParameters = textureParameters;

            this.isCollidable = isCollidable;
            this.isVisible = isVisible;


            //store for reset
            this.originalIsCollidable = isCollidable;
            this.originalIsVisible = isVisible;
        }

        public virtual void Update(GameTime gameTime)
        {
            //call update to set bounds, look abd up vectors, and matrices
            this.transform.Update();
        }

        public virtual void Reset()
        {
            this.transform.Reset();
            this.textureParameters.Reset();
            this.isCollidable = originalIsCollidable;
            this.isVisible = originalIsVisible;
        }

        public object Clone()
        {
            return new Sprite("clone - " + this.id, (Transform2D)transform.Clone(), (TextureParameters)textureParameters.Clone(), this.isCollidable, this.isVisible);
        }
    }
}
