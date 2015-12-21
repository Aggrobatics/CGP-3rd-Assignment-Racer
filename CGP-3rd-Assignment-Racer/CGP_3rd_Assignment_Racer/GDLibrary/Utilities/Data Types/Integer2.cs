using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GDLibrary
{
    public class Integer2 
    {
        public static Integer2 Zero = new Integer2(0, 0);
        public static Integer2 One = new Integer2(1, 1);
        public static Integer2 UnitX = new Integer2(1, 0);
        public static Integer2 UnitY = new Integer2(0,1);

        private int x, y;
        public int X
        {
            get { return this.x; }
            set { this.x = value; }
        }
        public int Y
        {
            get { return this.y; }
            set { this.y = value; }
        }

        public Integer2(int x, int y)
        {
            this.x = x; this.y = y;
        }
        public Integer2(float x, float y)
            : this((int)x, (int)y)
        {

        }
        public Integer2(Vector2 v)
            : this((int)v.X, (int)v.Y)
        {

        }

        public static Integer2 ToInteger2(Vector2 v)
        {
            return new Integer2(v.X, v.Y);
        }




        public static bool operator == (Integer2 first, Integer2 second)
        {
            return ((first.X == second.X) && (first.Y == second.Y));
        }

        public static bool operator != (Integer2 first, Integer2 second)
        {
            return ((first.X != second.X) || (first.Y != second.Y));
        }

        public static Integer2 operator + (Integer2 first, Integer2 second)
        {
            return new Integer2(first.X + second.X, first.Y + second.Y);
        }

        public static Integer2 operator - (Integer2 first, Integer2 second)
        {
            return new Integer2(first.X - second.X, first.Y - second.Y);
        }

        public static Integer2 operator * (Integer2 first, Integer2 second)
        {
            return new Integer2(first.X * second.X, first.Y * second.Y);
        }

        public static Integer2 operator *(Integer2 first, int second)
        {
            return new Integer2(first.X * second, first.Y * second);
        }

        public static Integer2 operator *(int second, Integer2 first)
        {
            return new Integer2(first.X * second, first.Y * second);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!this.GetType().Equals(obj.GetType()))
                return false;

            Integer2 other = obj as Integer2;

            return ((this.x == other.X) && (this.y == other.Y));
        }

        public override int GetHashCode()
        {
            int seed = 31;
            return this.x.GetHashCode() + seed * this.y.GetHashCode();
        }

    }
}
