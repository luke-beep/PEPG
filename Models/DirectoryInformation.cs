namespace PEPG.Models;

public class DirectoryInformation
{
    public string Name { get; set; } 
    public uint VirtualAddress { get; set; } 
    public uint Size { get; set; } 
    public string Section { get; set; }  
    public double Entropy { get; set; } 
    public string Hash { get; set; }
}