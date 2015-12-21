
namespace GDLibrary
{
    //friendly symbolic name for different resolutions
    //note we can override the base storage type from int to, in this case sbyte - to save space
    public enum ResolutionType : sbyte
    { 
        VGA, SVGA, WVGA, XGA, 
        HD720, WXGA, SXGA, UXGA, 
        HD1080, QXGA 
    };

}
