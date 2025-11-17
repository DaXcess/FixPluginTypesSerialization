using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FixPluginTypesSerialization.UnityPlayer.Structs.Default
{
    //2023
    [StructLayout(LayoutKind.Sequential)]
    public struct StringStorageDefaultV3
    {
        public StringStorageDefaultV3Union union;
        public int label;
        public nint labelrootref;
    }

    [StructLayout(LayoutKind.Explicit, Size = 33)]
    public struct StringStorageDefaultV3Union
    {
        [FieldOffset(0)]
        public StackAllocatedRepresentationV3 embedded;
        [FieldOffset(0)]
        public HeapAllocatedRepresentationV3 heap;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct StackAllocatedRepresentationV3
    {
        public unsafe fixed byte data[32];
        public StringStorageDefaultV3Flags flags;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    public struct StringStorageDefaultV3Flags
    {
        public byte flags;

        public bool IsHeap
        {
            get => flags == 0;
            set => flags = value ? (byte)0 : (byte)2;
        }

        public bool IsEmbedded
        {
            get => flags == 1;
            set => IsHeap = !value;
        }

        public static implicit operator int(StringStorageDefaultV3Flags f) => f.flags;
        public static implicit operator byte(StringStorageDefaultV3Flags f) => f.flags;
    }

    [StructLayout(LayoutKind.Explicit, Size = 33)]
    public struct HeapAllocatedRepresentationV3
    {
        [FieldOffset(0)]
        public nint data;
        [FieldOffset(0x8)]
        public ulong capacity;
        [FieldOffset(0x10)]
        public ulong size;
        [FieldOffset(0x20)]
        public StringStorageDefaultV3Flags flags;
    }
}
