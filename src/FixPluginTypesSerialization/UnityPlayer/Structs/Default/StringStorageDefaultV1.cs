using System.Runtime.InteropServices;

namespace FixPluginTypesSerialization.UnityPlayer.Structs.Default
{
    // core::StringStorageDefault<char> from ida -> produced C header file
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct StringStorageDefaultV1
    {
        public nint data;
        public ulong capacity;
        public ulong extra;
        public ulong size;
        public int label;
    }
}
