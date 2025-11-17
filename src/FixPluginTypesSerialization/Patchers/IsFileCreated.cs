using System;
using System.Linq;
using System.Runtime.InteropServices;
using FixPluginTypesSerialization.UnityPlayer.Structs.Default;
using FixPluginTypesSerialization.UnityPlayer.Structs.v2021.v2;
using MonoMod.RuntimeDetour;

namespace FixPluginTypesSerialization.Patchers
{
    internal static class IsFileCreated
    {
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate bool IsFileCreatedDelegate(IntPtr str);

        private static IsFileCreatedDelegate original;

        private static NativeDetour _detour;

        internal static bool IsApplied { get; private set; }

        public static void Apply(IntPtr from)
        {
            var hookPtr =
                Marshal.GetFunctionPointerForDelegate(new IsFileCreatedDelegate(OnIsFileCreated));

            _detour = new NativeDetour(from, hookPtr, new NativeDetourConfig { ManualApply = true });

            original = _detour.GenerateTrampoline<IsFileCreatedDelegate>();
            _detour.Apply();

            IsApplied = true;
        }

        internal static void Dispose()
        {
            _detour?.Dispose();
            IsApplied = false;
        }

        private static bool OnIsFileCreated(IntPtr str)
        {
            var assemblyString = new IsFileCreatedParam(str);
            var actualString = assemblyString.ToStringAnsi();

            if (actualString is not null && Preload.PluginNames.Any(actualString.EndsWith))
                return true;

            return original(str);
        }
    }
}