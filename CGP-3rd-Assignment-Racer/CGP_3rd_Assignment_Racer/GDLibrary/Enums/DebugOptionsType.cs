using System;

namespace GDLibrary
{
    //See https://msdn.microsoft.com/en-us/library/cc138362.aspx for more on Flags
    [Flags]
    public enum DebugOptionsType 
    {
        DisableDebugDrawer = 0, //note that all values are powers of 2. why?
        DrawBoundingBoxes = 1,
        DrawBSPSectors = 2,
        DrawBSPSectorData = 4,
        DrawCollisionData = 8,
        DrawFPS = 16
    }
}
