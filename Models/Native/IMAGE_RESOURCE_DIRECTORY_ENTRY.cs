namespace PEPG.Models.Native;

using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct ImageResourceDirectoryEntry
{
    public uint Name;
    public uint OffsetToData;
}