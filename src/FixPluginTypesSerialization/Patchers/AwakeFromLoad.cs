using System;
using System.Runtime.InteropServices;
using FixPluginTypesSerialization.UnityPlayer.Structs.Default;
using FixPluginTypesSerialization.UnityPlayer.Structs.v2023.v1;
using MonoMod.RuntimeDetour;

namespace FixPluginTypesSerialization.Patchers
{
    internal static class AwakeFromLoad
    {
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void AwakeFromLoadDelegate(IntPtr _monoManager, int awakeMode);

        private static AwakeFromLoadDelegate original;

        private static NativeDetour _detour;

        internal static MonoManager CurrentMonoManager;
        internal static bool IsApplied { get; private set; }

        public static void Apply(IntPtr from)
        {
            var hookPtr =
                Marshal.GetFunctionPointerForDelegate(new AwakeFromLoadDelegate(OnAwakeFromLoad));

            _detour = new NativeDetour(from, hookPtr, new NativeDetourConfig { ManualApply = true });

            original = _detour.GenerateTrampoline<AwakeFromLoadDelegate>();
            _detour.Apply();

            IsApplied = true;
        }

        internal static void Dispose()
        {
            _detour?.Dispose();
            IsApplied = false;
        }

        private static void OnAwakeFromLoad(IntPtr _monoManager, int awakeMode)
        {
            CurrentMonoManager = new MonoManager();

            CurrentMonoManager.CopyNativeAssemblyListToManaged();

            CurrentMonoManager.AddAssembliesToManagedList(Preload.PluginPaths);

            CurrentMonoManager.AllocNativeAssemblyListFromManaged();

            original(_monoManager, awakeMode);

            // Dispose detours as we don't need them anymore
            // and could hog resources for nothing otherwise
            IsFileCreated.Dispose();
            ConvertSeparatorsToPlatform.Dispose();
        }
    }
}