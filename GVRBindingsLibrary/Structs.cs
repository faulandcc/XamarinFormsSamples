using System;
using System.Runtime.InteropServices;
using ObjCRuntime;

namespace GVRVideoBinding
{
    [Native]
    public enum GVRWidgetDisplayMode : long
    {
        Embedded = 1,
        Fullscreen,
        FullscreenVR
    }

    [StructLayout (LayoutKind.Sequential)]
    public struct GVRHeadRotation
    {
        public nfloat yaw;

        public nfloat pitch;
    }

    public enum GVRVideoType
    {
        Mono = 1,
        StereoOverUnder
    }
}
