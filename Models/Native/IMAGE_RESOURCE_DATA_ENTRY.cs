using System.Runtime.InteropServices;

namespace PEPG.Models.Native;

[StructLayout(LayoutKind.Sequential)]
public struct ImageResourceDataEntry
{
    public uint OffsetToData;
    public uint Size;
    public uint CodePage;
    public uint Reserved;
}
