namespace PEPG.Models;

public class ImportInformation
{
    public string DllName { get; set; }
    public string Name { get; set; }
    public string UndecoratedName { get; set; }
    public long Rva { get; set; }
    public uint Hint { get; set; }
}