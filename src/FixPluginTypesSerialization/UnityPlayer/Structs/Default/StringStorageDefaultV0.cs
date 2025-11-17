using System.Runtime.InteropServices;

namespace FixPluginTypesSerialization.UnityPlayer.Structs.Default
{
    [StructLayout(LayoutKind.Sequential)]
    public struct StringStorageDefaultV0
    {
        public nint data;
        public ulong extra1;
        public ulong size;
        public ulong flags;
        public ulong extra2;
    }
}
