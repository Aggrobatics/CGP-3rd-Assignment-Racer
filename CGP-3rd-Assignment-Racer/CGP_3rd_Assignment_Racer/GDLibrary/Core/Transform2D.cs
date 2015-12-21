using System;
using Microsoft.Xna.Framework;
using GDGame;

namespace GDLibrary
{
    public class Transform2D : ICloneable
    {
        #region Fields
        public static Main game;
        private Vector2 position, scale, originalPosition, originalScale;
        private float rotationInDegrees, rotationInRadians, originalRotationInDegrees;
        private Vector2 look, up, originalLook, originalUp;
        private Vector2 origin, originalOrigin;

        private bool bBoundsDirty, bLookDirty;
        private Matrix worldMatrix, originMatrix, positionMatrix, rotationMatrix, scaleMatrix;

        private Rectangle bounds, originalBounds;
        private Integer2 originalDimensions;
        private int bspSector;
        #endregion

        #region Properties
        protected Matrix RotationMatrix
        {
            get
            {
                return this.rotationMatrix;
            }
        }
        protected Matrix PositionMatrix
        {
            get
            {
                return this.positionMatrix;
            }
        }
        public int BSPSector
        {
            get
            {
                return this.bspSector;
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
                this.positionMatrix = Matrix.CreateTranslation(new Vector3(this.position, 0));
                this.bBoundsDirty = true;
            }
        }
        public float Rotation
        {
            get
            {
                return this.rotationInDegrees;
            }
            set
            {
                this.rotationInDegrees = value;
                this.rotationInDegrees %= 360;
                this.rotationInRadians = MathHelper.ToRadians(rotationInDegrees);
                this.rotationMatrix = Matrix.CreateRotationZ(this.rotationInRadians);
                this.bBoundsDirty = true;
                this.bLookDirty = true;
            }
        }
        public float RotationInRadians
        {
            get
            {
                return this.rotationInRadians;
            }
        }
        public Vector2 Scale
        {
            get
            {
                return this.scale;
            }
            set
            {
                //do not allow scale to go to zero
                this.scale = (value != Vector2.Zero) ? value : Vector2.One;
                this.scaleMatrix = Matrix.CreateScale(new Vector3(this.scale, 1));
                this.bBoundsDirty = true;
            }
        }
        public Vector2 Look
        {
            get
            {
                return this.look;
            }
        }
        public Vector2 Up
        {
            get
            {
                return this.up;
            }
        }
        public Vector2 Origin
        {
            get
            {
                return this.origin;
            }
            set
            {
                this.origin = value;
                this.originMatrix = Matrix.CreateTranslation(new Vector3(-origin, 0));
                this.bBoundsDirty = true;
            }
        }
        public Matrix World
        {
            get
            {
                return this.worldMatrix;
            }
            protected set
            {
                this.worldMatrix = value;
            }
        }
        public Rectangle Bounds
        {
            get
            {
                return this.bounds;
            }
            protected set
            {
                this.bounds = value;
            }
        }
        public Rectangle OriginalBounds
        {            
            get
            {
                return this.originalBounds;
            }
            set
            {
                this.originalBounds = value;
            }
        }
        #endregion

        //used by dynamic sprites i.e. which need a look and right vector for movement
        public Transform2D(Vector2 position, float rotationInDegrees, Vector2 scale,
            Vector2 look, Vector2 up, Vector2 origin, Integer2 dimensions)
        {
            //set using properties
            Set(position, rotationInDegrees, scale, origin, dimensions);

            //store defaults used for reset
            SetDefaults(position, rotationInDegrees, scale, look, up, origin, dimensions);
        }

        //used by static background sprites that cover the entire screen OR more than the entire screen
        public Transform2D(Vector2 scale)
        {
            //set using properties
            Set(Vector2.Zero, 0, scale, Vector2.Zero, Integer2.Zero);

            //store defaults used for reset
            SetDefaults(Vector2.Zero, 0, scale, Vector2.UnitX, -Vector2.UnitY, Vector2.Zero, Integer2.Zero);
        }

        //set all new values
        public void Set(Vector2 position, float rotationInDegrees, Vector2 scale,
            Vector2 origin, Integer2 dimensions)
        {
            this.Position = position;
            this.Scale = scale;
            this.Rotation = rotationInDegrees;
            this.Origin = origin;


            //original bounding box based on the texture source rectangle dimensions
            this.originalBounds = new Rectangle(0, 0, dimensions.X, dimensions.Y);
            this.originalDimensions = dimensions;

            this.bBoundsDirty = true;
            this.bLookDirty = true;
        }

        //store the original default values for any subsequent reset
        private void SetDefaults(Vector2 position, float rotationInDegrees, Vector2 scale,
            Vector2 look, Vector2 up, Vector2 origin, Integer2 dimensions)
        {
            this.originalPosition = position;
            this.originalScale = scale;
            this.originalRotationInDegrees = rotationInDegrees;
            this.originalLook = look;
            this.originalUp = up;
            this.originalDimensions = dimensions;
            this.originalOrigin = origin;
        }

        //used to reset sprite to it's original transform
        public void Reset()
        {
            Set(originalPosition, originalRotationInDegrees, originalScale, originalOrigin, originalDimensions);
        }


        public virtual void Update()
        {
            //these if statements trigger on two conditions - first run, and when either pos, rot, or scale change
            if (this.bLookDirty)
            {
                Vector2.Transform(ref this.originalLook, ref this.rotationMatrix, out look);
                Vector2.Transform(ref this.originalUp, ref this.rotationMatrix, out up);
                this.bLookDirty = false;
            }

            if (this.bBoundsDirty)
            {
                UpdateWorld();
                UpdateBounds();
                UpdateBSP();

                this.bBoundsDirty = false;
            }
        }

        protected virtual void UpdateWorld()
        {
            this.worldMatrix = originMatrix * scaleMatrix * rotationMatrix * positionMatrix;
        }

        protected virtual void UpdateBSP()
        {
            this.bspSector = game.ScreenManager.GetBSP(this.bounds);
        }

        protected virtual void UpdateBounds()
        {
            this.bounds = CollisionUtility.CalculateTransformedBoundingRectangle(
                this.originalBounds, this.worldMatrix);
  
        }




        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }


}
