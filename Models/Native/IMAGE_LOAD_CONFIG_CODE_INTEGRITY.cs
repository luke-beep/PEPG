using System.Runtime.InteropServices;

namespace PEPG.Models.Native;


[StructLayout(LayoutKind.Sequential)]
public struct ImageLoadConfigCodeIntegrity
{
    public ushort Flags;
    public ushort Catalog;
    public uint CatalogOffset;
    public uint Reserved;
}