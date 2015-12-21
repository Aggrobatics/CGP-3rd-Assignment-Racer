
using Microsoft.Xna.Framework;
namespace GDLibrary
{
    public interface IActor
    {
        int GetBSP();
        Transform2D GetTransform();
        ControllerList GetControllerList();
        void Reset();

        //there's an issue with this method since both Camera and PawnSprite can be IActor 
        //but it doesnt make sense for camera to have this method
        TextureParameters GetTextureParameters();
    }
}
