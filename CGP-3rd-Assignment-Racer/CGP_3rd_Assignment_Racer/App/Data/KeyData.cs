using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace GDGame
{
    public abstract class KeyData
    {
        #region Camera - HandleInput
        public static readonly int IndexCameraLeft = 0;
        public static readonly int IndexCameraRight = 1;
        public static readonly int IndexCameraUp = 2;
        public static readonly int IndexCameraDown = 3;
        public static readonly int IndexCameraZoomIn = 4;
        public static readonly int IndexCameraZoomOut = 5;
        public static readonly int IndexCameraRotateLeft = 6;
        public static readonly int IndexCameraRotateRight = 7;
        public static readonly int IndexCameraReset = 8;

        public static readonly Keys[] KeysCameraOne = { Keys.A, Keys.D, Keys.W, Keys.S, 
                                                       Keys.Q, Keys.E, Keys.Z, Keys.X, Keys.F1};


        public static Keys[] KeysCameraTwo = { Keys.J, Keys.L, Keys.I, Keys.K, 
                                                       Keys.U, Keys.O, Keys.N, Keys.M, Keys.F2};


        public static readonly float CameraTranslateRate = 1;
        public static readonly float CameraRotateIncrement = 1;
        public static readonly Vector2 CameraScaleIncrement = 0.01f * Vector2.One;
        #endregion
    }
}
