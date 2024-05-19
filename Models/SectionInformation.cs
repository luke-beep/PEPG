namespace PEPG.Models;

public class SectionInformation
{
    public string Name { get; set; }
    public uint VirtualSize { get; set; }
    public uint VirtualAddress { get; set; }
    public uint SizeOfRawData { get; set; }
    public uint PointerToRawData { get; set; }
    public uint PointerToRelocations { get; set; }
    public uint PointerToLinenumbers { get; set; }
    public ushort NumberOfRelocations { get; set; }
    public ushort NumberOfLinenumbers { get; set; }
    public string Characteristics { get; set; }
    public double Entropy { get; set; }
    public string Hash { get; set; }
}