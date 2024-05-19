namespace PEPG.Models.Native;

using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct ImageResourceDirectory
{
    public uint Characteristics;
    public uint TimeDateStamp;
    public ushort MajorVersion;
    public ushort MinorVersion;
    public ushort NumberOfNamedEntries;
    public ushort NumberOfIdEntries;
}