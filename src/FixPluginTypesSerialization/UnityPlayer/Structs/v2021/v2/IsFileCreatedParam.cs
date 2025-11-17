using FixPluginTypesSerialization.UnityPlayer.Structs.Default;
using System;
using System.Runtime.InteropServices;

namespace FixPluginTypesSerialization.UnityPlayer.Structs.v2021.v2
{
    public class IsFileCreatedParam
    {
        public IsFileCreatedParam()
        {

        }

        public IsFileCreatedParam(IntPtr pointer)
        {
            Pointer = pointer;
        }

        public IntPtr Pointer { get; set; }

        private unsafe char* data => *(char**)Pointer;
        private unsafe ulong length => *(ulong*)(Pointer + (nint)0x8);

        public unsafe string ToStringAnsi()
        {
            return Marshal.PtrToStringAnsi((nint)data, (int)length);
        }
    }
}
