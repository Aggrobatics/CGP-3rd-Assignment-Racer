using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GDLibrary
{
    public class CameraTransform2D : Transform2D
    {
        #region Fields
        private Viewport viewPort;
        private Vector3 centreViewPort;
        private Integer2 offSets;
        #endregion

        #region Properties
        public Viewport Viewport
        {
            get
            {
                return viewPort;
            }
        }
        public Integer2 OffSets
        {
            get
            {
                return offSets;
            }
        }
        #endregion

        public CameraTransform2D(Vector2 position, float rotationInDegrees, Vector2 scale,
            Vector2 look, Vector2 up, Vector2 origin, Integer2 dimensions, Viewport viewPort)
             : base(position, rotationInDegrees, scale, look, up, origin, dimensions)
         {
             this.viewPort = viewPort;
             this.centreViewPort = new Vector3(viewPort.Width / 2, viewPort.Height / 2, 0);
             this.offSets = Integer2.Zero;
             UpdateOffsetAndCentre();
         }

         private void UpdateOffsetAndCentre()
         {
             this.offSets.X = (int)Math.Round(Position.X - viewPort.Width / (2 * Scale.X), 1);
             this.offSets.Y = (int)Math.Round(Position.Y - viewPort.Height / (2 * Scale.Y), 1);
         }

         private Matrix boundsMatrix;
         protected override void UpdateBounds()
         {
             UpdateOffsetAndCentre();

             this.boundsMatrix = Matrix.CreateTranslation(-centreViewPort)
                           * this.RotationMatrix
                                * Matrix.CreateScale(1.0f / Scale.X, 1.0f / Scale.Y, 1)
                                   * this.PositionMatrix;

             this.Bounds = CollisionUtility.CalculateTransformedBoundingRectangle(this.OriginalBounds, 
                                boundsMatrix);
         }

         protected override void UpdateWorld()
         {
             this.World = Matrix.CreateTranslation(new Vector3(-this.Position, 0))
                              * this.RotationMatrix
                                  * Matrix.CreateScale(this.Scale.X, this.Scale.Y, 1)
                                     * Matrix.CreateTranslation(this.centreViewPort);
         }

         public new CameraTransform2D GetTransform()
         {
             return this;
         }
    }
}
