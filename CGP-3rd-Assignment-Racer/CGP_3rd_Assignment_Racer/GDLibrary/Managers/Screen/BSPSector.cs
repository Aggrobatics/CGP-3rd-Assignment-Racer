using System;
using Microsoft.Xna.Framework;

namespace GDLibrary
{
    /// <summary>
    /// Stores the rectangle and sector number for each collision sector onscreen.
    /// The greater the number of collision sectors, the less CDCR tests and single sprite must perform.
    /// </summary>
    public class BSPSector : ICloneable
    {
        public static int startSectorIndex = 0;      //index of the current sector, increases as we add, maximum is totalSectorNumber

        #region Fields
        private Rectangle sectorBounds;                 //bounding rectangle for each sector
        private int sectorNumber;                       //sector number for each rectangle (must be power of 2)
        #endregion

        #region Properties
        public int Number
        {
            get
            {
                return sectorNumber;
            }
        }
        public Rectangle Bounds
        {
            get
            {
                return sectorBounds;
            }
        }
        
        public int Width
        {
            get
            {
                return sectorBounds.Width;
            }
            set
            {
                sectorBounds.Width = value > 0 ? value : 100;
            }
        }
        public int Height
        {
            get
            {
                return sectorBounds.Height;
            }
            set
            {
                sectorBounds.Height = value > 0 ? value : 100;
            }
        }
        #endregion

        public BSPSector(float x, float y, int width, int height)
        {
            //every new sector gets a power of two sector number, e.g. 1, 2, 4, 8 etc
            this.sectorNumber = (int)Math.Pow(2, startSectorIndex++);
            this.sectorBounds = new Rectangle((int)x, (int)y, width, height);
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;

            if (obj == null)
                return false;

            if (!this.GetType().Equals(obj.GetType()))
                return false;

            BSPSector other = (BSPSector)obj;
            return this.sectorBounds.Equals(other.Bounds);
        }

        public override int GetHashCode()
        {
            int seed = 31;
            return this.sectorBounds.GetHashCode() + seed *  this.sectorNumber;
        }

        public Object Clone()
        {
            return (BSPSector)this.MemberwiseClone();
        }

        public override string ToString()
        {
            return "[" + this.sectorNumber + "] : " + StringUtility.GetRectangleAsCompactString(this.Bounds);
        } 
    }
}
