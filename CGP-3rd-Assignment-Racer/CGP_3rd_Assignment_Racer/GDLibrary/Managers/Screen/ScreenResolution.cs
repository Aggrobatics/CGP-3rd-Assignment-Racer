using System;

namespace GDLibrary
{
    /*
     * NMCG
     * 6.12.13
     * inner class to store name-resolution pairs
     */
    class ResolutionData
    {
        private string name;
        private Integer2 resolution;
        private string aspectRatio;

        #region PROPERTIES
        public string Name
        {
            get
            {
                return name;
            }

        }
        public string AspectRatio
        {
            get
            {
                return aspectRatio;
            }

        }
        public Integer2 Resolution
        {
            get
            {
                return resolution;
            }

        }
        #endregion

        public ResolutionData(string name, Integer2 resolution, string aspectRatio)
        {
            this.name = name; this.resolution = resolution; this.aspectRatio = aspectRatio;
        }
    }

    /*
     * NMCG
     * 6.12.13
     * stores info on typical screen resolutions
     */

    public class ScreenResolution
    {
     
        //See http://upload.wikimedia.org/wikipedia/commons/e/e5/Vector_Video_Standards2.svg
        //stores info on screen resolution as e.g. "wvga", (800,480)
        private static ResolutionData[] resolutionData
            = { 
                  new ResolutionData("vga", new Integer2(640,480), "4:3"),
                  new ResolutionData("svga", new Integer2(800, 600), "4:3"),
                  new ResolutionData("wvga", new Integer2(800, 480), "16:9"),
                  new ResolutionData("xga", new Integer2(1024, 768), "4:3"),
                  new ResolutionData("hd 720", new Integer2(1280, 720), "4:3"),
                  new ResolutionData("wxga", new Integer2(1280, 768), "8:5"),
                  new ResolutionData("sxga", new Integer2(1280, 1024), "5:4"),
                  new ResolutionData("uxga", new Integer2(1600, 1200), "4:3"),
                  new ResolutionData("hd 1080", new Integer2(1920, 1080), "16:9"),
                  new ResolutionData("qxga", new Integer2(2048, 1536), "4:3")
                  //add any new resolutions here...
              };

        private static int currentIndex = 0;

        #region PROPERTIES
        public static string CurrentResolutionName
        {
            get
            {
                return resolutionData[currentIndex].Name;
            }
        }
        public static Integer2 CurrentResolution
        {
            get
            {
                return resolutionData[currentIndex].Resolution;
            }
        }
        public static string CurrentAspectRatio
        {
            get
            {
                return resolutionData[currentIndex].AspectRatio;
            }
        }
        #endregion

        //cycles through resolutions available and returns details of next available resolution
        public static Integer2 GetNextResolutionData()
        {
            //if we reach end of the list then set back to zero, otherwise use then increment
            currentIndex = (currentIndex == resolutionData.Length - 1) ? 0 : ++currentIndex;
            return resolutionData[currentIndex].Resolution;
        }
        public static Integer2 GetResolution(ResolutionType resolution)
        {
            int index = (int)resolution;

            if ((index >= 0) && (index < resolutionData.Length))
            {
                //set the currentIndex to be where you found the string
                currentIndex = index;
            }
            else
            {
                //invalid so set to 0 resolution i.e. (640,480)
                currentIndex = 0;
            }
            //return the resolution information for use in setting screen dimensions
            return resolutionData[currentIndex].Resolution;

        }
        public static string GetAspectRatio(ResolutionType resolution)
        {
            int index = (int)resolution;
            if ((index >= 0) && (index < resolutionData.Length))
            {
                //set the currentIndex to be where you found the string
                return resolutionData[index].AspectRatio;
            }
            else
            {
                //invalid so set to 0 resolution i.e. (640,480)
                return resolutionData[index].AspectRatio;
            }
        }
        public static string GetCurrentAspectRatio()
        {
            //set the currentIndex to be where you found the string
            return resolutionData[currentIndex].AspectRatio;
        }

        //not used at the moment
        private static int GetIndexByName(String resolutionName)
        {
            //simple lookup - find the index of this string
            for (int i = 0; i < resolutionData.Length; i++)
            {
                if(resolutionData[i].Name.Equals(resolutionName))
                    return i;
            }
            return 0;
        }
    }
}
