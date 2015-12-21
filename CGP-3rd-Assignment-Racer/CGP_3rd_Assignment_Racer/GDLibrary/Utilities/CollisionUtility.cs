using Microsoft.Xna.Framework;
using System;

namespace GDLibrary
{
    public class CollisionUtility
    {
        private static Transform2D transform;
        private static Vector2 boundsPos, boundsDim;
        private static TextureParameters textureParams;
        
        public static Rectangle GetRectangle(IActor actor)
        {
            textureParams = actor.GetTextureParameters();
            //get the actors transform
            transform = actor.GetTransform();


            //calculate the position of the top left hand corner of the bounding area
            boundsPos = transform.Position - textureParams.Origin * transform.Scale;

            //set the bounding area width and height based on the texture dimension and scale
            boundsDim = new Vector2(textureParams.SourceRectangle.Width * transform.Scale.X,
                textureParams.SourceRectangle.Height * transform.Scale.Y);

            //return a rectangle starting at the position specified with these dimensions
            return GetRectangle(boundsPos, boundsDim);
        }

        public static Rectangle GetRectangle(Vector2 pos, Vector2 dimensions)
        {
            return new Rectangle((int)pos.X, (int)pos.Y, (int)dimensions.X, (int)dimensions.Y);
        }

        public static Rectangle GetRectangle(Integer2 pos, Integer2 dimensions)
        {
            return new Rectangle(pos.X, pos.Y, dimensions.X, dimensions.Y);
        }

        public static bool CheckCollision(Sprite collider, Sprite colidee)
        {
            return collider.Transform.Bounds.Intersects(colidee.Transform.Bounds);
        }

        public static bool CheckProjectedCollision(Sprite collider, Vector2 moveVector, Sprite colidee)
        {
            return GetProjectedBounds(collider.Transform.Bounds, moveVector).Intersects(colidee.Transform.Bounds);
        }


        public static Rectangle GetProjectedBounds(Rectangle bounds, Vector2 moveVector)
        {
            return new Rectangle(
            (int)(bounds.X + moveVector.X), 
            (int)(bounds.Y + moveVector.Y),
            bounds.Width, bounds.Height);
        }

        public static bool Contains(Rectangle bounds, IActor actor)
        {
            Vector2 position = actor.GetTransform().Position;
            return bounds.Contains(new Point((int)position.X, (int)position.Y));
        }

        public static bool Contains(Rectangle bounds, Vector2 position)
        {
            return bounds.Contains(new Point((int)position.X, (int)position.Y));
        }
        

        //temp variables
        private static Vector2 leftTop, rightTop, leftBottom, rightBottom, min, max;

        /// <summary>
        /// Calculates an axis aligned rectangle which fully contains an arbitrarily transformed axis aligned rectangle.
        /// </summary>
        /// <param name="rectangle">Original bounding rectangle.</param>
        /// <param name="transform">World transform of the rectangle.</param>
        /// <returns>A new rectangle which contains the trasnformed rectangle.</returns>
        public static Rectangle CalculateTransformedBoundingRectangle(Rectangle rectangle, Matrix transform)
        {
            //   Matrix inverseMatrix = Matrix.Invert(transform);
            // Get all four corners in local space
            leftTop = new Vector2(rectangle.Left, rectangle.Top);
            rightTop = new Vector2(rectangle.Right, rectangle.Top);
            leftBottom = new Vector2(rectangle.Left, rectangle.Bottom);
            rightBottom = new Vector2(rectangle.Right, rectangle.Bottom);

            // Transform all four corners into work space
            Vector2.Transform(ref leftTop, ref transform, out leftTop);
            Vector2.Transform(ref rightTop, ref transform, out rightTop);
            Vector2.Transform(ref leftBottom, ref transform, out leftBottom);
            Vector2.Transform(ref rightBottom, ref transform, out rightBottom);

            // Find the minimum and maximum extents of the rectangle in world space
            min = Vector2.Min(Vector2.Min(leftTop, rightTop),
                                      Vector2.Min(leftBottom, rightBottom));
            max = Vector2.Max(Vector2.Max(leftTop, rightTop),
                                      Vector2.Max(leftBottom, rightBottom));

            // Return that as a rectangle
            return new Rectangle((int)Math.Round(min.X), (int)Math.Round(min.Y),
                                 (int)Math.Round(max.X - min.X), (int)Math.Round(max.Y - min.Y));
        }
        
        /*
        public static bool IntersectsBSP(IActor collider, IActor colidee)
        {
            if ((colidee != null) && (collider != colidee)) //valid and not same
            {
                int bspA = collider.GetBSP();
                int bspB = colidee.GetBSP();

                return ((bspA & bspB) > 0) ? true : false;
            }

            return false;
        }
         */
    }
}
