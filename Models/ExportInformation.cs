namespace PEPG.Models;

public class ExportInformation
{
    public string DllName { get; set; }
    public string Name { get; set; }
    public uint Ordinal { get; set; }
    public uint Rva { get; set; }
    public long Address { get; set; }
    public uint Hint { get; set; }
}