using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GDLibrary
{
    /* Used to create a pseudo enum that can associate three (e.g. "2x4", 4, 2) variables with a type (e.g. FourByFour)
     * Sealed means this class can never be inherited from
     */
    public sealed class BSPSectorLayoutType
    {
        //these statics create pre-defined arrangements for BSP sectors used by the ScreenManager
        public static readonly BSPSectorLayoutType OneByTwo = new BSPSectorLayoutType("1x2", 1, 2);
        public static readonly BSPSectorLayoutType TwoByOne = new BSPSectorLayoutType("2x1", 2, 1);
        public static readonly BSPSectorLayoutType TwoByTwo = new BSPSectorLayoutType("2x2", 2, 2);
        public static readonly BSPSectorLayoutType ThreeByThree = new BSPSectorLayoutType("3x3", 3, 3);
        public static readonly BSPSectorLayoutType TwoByFour = new BSPSectorLayoutType("4x2", 2, 4);
        public static readonly BSPSectorLayoutType FourByTwo = new BSPSectorLayoutType("2x4", 4, 2);
        public static readonly BSPSectorLayoutType FourByFour = new BSPSectorLayoutType("4x4", 4, 4);
        public static readonly BSPSectorLayoutType TwoByEight = new BSPSectorLayoutType("2x8", 2, 8);
        //etc...

        #region Fields
        private string id;
        private int countHorizontal, countVertical;
        #endregion

        #region Properties
        public string ID
        {
            get
            {
                return this.id;
            }
        }
        public int CountHorizontal
        {
            get
            {
                return this.countHorizontal;
            }
        }
        public int CountVertical
        {
            get
            {
                return this.countVertical;
            }
        }
        public int TotalSectors
        {
            get
            {
                return this.countHorizontal * this.countVertical;
            }
        }

        #endregion

        private BSPSectorLayoutType(string id, int countVertical, int countHorizontal)
        {
            this.id = id;
            this.countHorizontal = countHorizontal;
            this.countVertical = countVertical;
        }

        public override string ToString()
        {
            return "ID: " + this.id + "(" + this.countHorizontal + "," + this.countVertical + ")";
        }

    }
}
