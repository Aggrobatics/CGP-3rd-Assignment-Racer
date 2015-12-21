using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Xml.Serialization;

namespace GDLibrary
{
    [Serializable] //step 1 - mark as serializable
    public class AnimationData
    {
        //step 3 - either make the variables public (quick and dirty) or private with properties (adheres to OOD encapsulation)
        [XmlAttribute("name")]
        public string name;
        [XmlAttribute("frameCount")]
        public int frameCount;

        public Rectangle sourceRectangle;
        public Vector2 origin;
        
        //step 2 - add a default constructor - used for doing the typecast on load()
        public AnimationData()
            : this("", new Rectangle(0,0,0,0), Vector2.Zero,0)
        {

        }

        public AnimationData(string name, Rectangle sourceRectangle, Vector2 origin, 
            int frameCount)
        {
            this.name = name.ToLower().Trim(); //e.g. "  WalK  " becomes "walk"
            this.sourceRectangle = sourceRectangle;
            this.origin = origin; 
            this.frameCount = frameCount;
        }

        public override bool Equals(object obj)
        {
            AnimationData other = obj as AnimationData;
            return this.name.Equals(other.name);
        }
        public override int GetHashCode()
        {
            int seed = 31;
            return this.name.GetHashCode() + seed * this.frameCount;
        }
    }
}
