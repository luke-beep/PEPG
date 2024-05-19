namespace PEPG.Models;

public class DosHeaderInformation
{
    public ushort Magic { get; set; }
    public ushort BytesOnLastPage { get; set; }
    public ushort PagesInFile { get; set; }
    public ushort Relocations { get; set; }
    public ushort SizeOfHeader { get; set; }
    public ushort MinimumExtraParagraphs { get; set; }
    public ushort MaximumExtraParagraphs { get; set; }
    public ushort InitialSs { get; set; }
    public ushort InitialSp { get; set; }
    public ushort Checksum { get; set; }
    public ushort InitialIp { get; set; }
    public ushort InitialCs { get; set; }
    public ushort AddressOfRelocationTable { get; set; }
    public ushort OverlayNumber { get; set; }
    public ushort Oemid { get; set; }
    public ushort OemInfo { get; set; }
    public int AddressOfNewExeHeader { get; set; }
}