namespace PEPG.Models;

public class FileHeaderInformation
{
    public uint Signature { get; set; }
    public ushort Machine { get; set; }
    public ushort NumberOfSections { get; set; }
    public DateTime TimeDateStamp { get; set; }
    public uint PointerToSymbolTable { get; set; }
    public uint NumberOfSymbols { get; set; }
    public ushort SizeOfOptionalHeader { get; set; }
    public ushort Characteristics { get; set; }
}