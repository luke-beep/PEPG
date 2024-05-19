using System.Runtime.InteropServices;

namespace PEPG.Models.Native;

[StructLayout(LayoutKind.Sequential)]
public struct ImageImportDirectory
{
    public uint ImportLookupTable;
    public uint TimeDateStamp;
    public uint ForwarderChain;
    public uint Name;
    public uint ImportAddressTable;
}