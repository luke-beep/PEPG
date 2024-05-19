namespace PEPG.Models;

public class GeneralInformation
{
    public string TargetMachine { get; set; }
    public string ImageName { get; set; }
    public string ImagePath { get; set; }
    public int ImageSize { get; set; }
    public uint EntryPoint { get; set; }
    public ulong ImageBase { get; set; }
    public double ImageEntropy { get; set; }
    public DateTime TimeStamp { get; set; }
    public uint HeaderChecksum { get; set; }
    public long HeaderSpare { get; set; }
    public string Subsystem { get; set; }
    public ushort MajorSubsystemVersion { get; set; }
    public ushort MinorSubsystemVersion { get; set; }
    public string Characteristics { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ModificationDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}