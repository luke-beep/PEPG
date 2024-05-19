namespace PEPG.Models;

public class ResourceInformation
{
    public uint Id { get; set; }
    public string Type { get; set; }
    public uint OffsetToData { get; set; }
    public uint Size { get; set; }
    public uint CodePage { get; set; }
    public string Language { get; set; }
    public string Hash { get; set; }
    public double Version { get; set; }
}