using System.Runtime.InteropServices;
using System.Text;
using PEPG.Models;
using PEPG.Models.Native;
using static PEPG.Helpers.NativeHelper;
using static PEPG.Helpers.BinaryHelper;
using static PEPG.Helpers.ClassHelper;


namespace PEPG.Helpers;

public class PeHelper
{
    private const uint ImageNtSignature = 0x00004550; // PE00
    private const ushort ImageNtOptionalHdr64Magic = 0x20B;

    public static uint GetPeSignature(FileStream fs, BinaryReader reader, int loc)
    {
        fs.Seek(loc, SeekOrigin.Begin);
        var peSignature = reader.ReadUInt32();
        return peSignature != 0x00004550 ? 0 : peSignature;
    }

    public static uint GetMagic(FileStream fs, BinaryReader reader)
    {
        var magic = reader.ReadUInt16();
        fs.Seek(-2, SeekOrigin.Current);
        return magic;
    }

    public static void VerifySignature(uint peSignature)
    {
        if (peSignature != 0x00004550)
            throw new Exception("Invalid PE Signature.");
    }

    public static string GetSubsystem(ushort subsystem)
    {
        return subsystem switch
        {
            1 => "Native",
            2 => "Windows GUI",
            3 => "Windows CUI",
            5 => "OS/2 CUI",
            7 => "POSIX CUI",
            8 => "Native Windows",
            9 => "Windows CE GUI",
            10 => "EFI Application",
            11 => "EFI Boot Service Driver",
            12 => "EFI Runtime Driver",
            13 => "EFI ROM",
            14 => "Xbox",
            16 => "Windows boot application",
            _ => $"Unknown (0x{subsystem:X})"
        };
    }

    public static string GetImageCharacteristicsDescription(uint characteristics)
    {
        string[] descriptions =
        [
            "Stripped", "Executable", "Line numbers stripped", "Local symbols stripped", "Aggressive working set trim",
            "Large address aware", "Reserved", "Bytes reversed low", "32-bit machine", "Debugging information removed",
            "System file", "DLL", "Uniprocessor system only", "Bytes reversed high"
        ];
        uint[] masks =
        [
            0x0001, 0x0002, 0x0004, 0x0008, 0x0010, 0x0020, 0x0040, 0x0080, 0x0100, 0x0200, 0x1000, 0x2000, 0x4000,
            0x8000
        ];

        var result = string.Empty;
        for (var i = 0; i < masks.Length; i++)
            if ((characteristics & masks[i]) == masks[i])
                result += $"{descriptions[i]}, ";
        return result.TrimEnd(',', ' ');
    }

    public static string GetDllCharacteristicsDescription(uint characteristics)
    {
        string[] descriptions =
        [
            "Reserved", "Reserved", "Reserved", "Reserved", "High entropy VA", "Dynamic base", "Force integrity",
            "NX compatible", "No isolation", "No SEH", "No bind", "App container", "WDM driver", "Guard CF",
            "Terminal server aware"
        ];
        uint[] masks =
        [
            0x0001, 0x0002, 0x0004, 0x0008, 0x0020, 0x0040, 0x0080, 0x0100, 0x0200, 0x0400, 0x0800, 0x1000, 0x2000,
            0x4000, 0x8000
        ];

        var result = string.Empty;
        for (var i = 0; i < masks.Length; i++)
            if ((characteristics & masks[i]) == masks[i])
                result += $"{descriptions[i]}, ";
        return result.TrimEnd(',', ' ');
    }

    public static string GetMachineType(ushort machine)
    {
        return machine switch
        {
            0x0 => "Unknown",
            0x184 => "Alpha AXP 32-bit",
            0x284 => "Alpha AXP 64-bit",
            0x1d3 => "AM33",
            0x8664 => "AMD64",
            0x1c0 => "ARM little endian",
            0xaa64 => "ARM64",
            0x1c4 => "ARM Thumb-2 little endian",
            0xebc => "EFI byte code",
            0x14c => "Intel 386",
            0x200 => "Intel Itanium",
            0x6232 => "LoongArch 32-bit",
            0x6264 => "LoongArch 64-bit",
            0x9041 => "M32R little endian",
            0x266 => "MIPS16",
            0x366 => "MIPS with FPU",
            0x466 => "MIPS16 with FPU",
            0x1f0 => "PowerPC little endian",
            0x1f1 => "PowerPC with floating point support",
            0x166 => "MIPS little endian",
            0x5032 => "RISC-V 32-bit",
            0x5064 => "RISC-V 64-bit",
            0x5128 => "RISC-V 128-bit",
            0x1a2 => "Hitachi SH3",
            0x1a3 => "Hitachi SH3 DSP",
            0x1a6 => "Hitachi SH4",
            0x1a8 => "Hitachi SH5",
            0x1c2 => "Thumb",
            0x169 => "MIPS little-endian WCE v2",
            _ => $"Unknown (0x{machine:X})"
        };
    }

    public static string GetSectionCharacteristics(uint characteristics)
    {
        string[] descriptions =
        [
            "Executable", "Initialized data", "Uninitialized data", "Data referenced through GP",
            "Extended relocations", "Discardable", "Not cachable", "Not pageable", "Shared", "Executable", "Readable",
            "Writable"
        ];
        uint[] masks =
        [
            0x00000020, 0x00000040, 0x00000080, 0x00008000, 0x01000000, 0x02000000, 0x04000000, 0x08000000, 0x10000000,
            0x20000000, 0x40000000, 0x80000000
        ];

        var result = string.Empty;
        for (var i = 0; i < masks.Length; i++)
            if ((characteristics & masks[i]) == masks[i])
                result += $"{descriptions[i]}, ";
        return result.TrimEnd(',', ' ') + $" (0x{characteristics:X})";
    }

    public static string GetDirectoryType(int index)
    {
        return index switch
        {
            0 => "Export Table",
            1 => "Import Table",
            2 => "Resource Table",
            3 => "Exception Table",
            4 => "Security Table",
            5 => "Base Relocation Table",
            6 => "Debug",
            7 => "Architecture",
            8 => "Global Ptr",
            9 => "TLS Table",
            10 => "Load Config Table",
            11 => "Bound Import",
            12 => "IAT",
            13 => "Delay Import Descriptor",
            14 => "CLR Runtime Header",
            _ => "Unknown"
        };
    }

    public static ImageDataDirectory[] GetDataDirectories(dynamic optionalHeader)
    {
        return
        [
            optionalHeader.ExportTable,
            optionalHeader.ImportTable,
            optionalHeader.ResourceTable,
            optionalHeader.ExceptionTable,
            optionalHeader.CertificateTable,
            optionalHeader.BaseRelocationTable,
            optionalHeader.Debug,
            optionalHeader.Architecture,
            optionalHeader.GlobalPtr,
            optionalHeader.TLSTable,
            optionalHeader.LoadConfigTable,
            optionalHeader.BoundImport,
            optionalHeader.IAT,
            optionalHeader.DelayImportDescriptor,
            optionalHeader.CLRRuntimeHeader,
            optionalHeader.Reserved
        ];
    }

    public static byte[] ReadDirectoryData(uint virtualAddress, uint size,
        IReadOnlyCollection<SectionInformation> sections, string path)
    {
        if (sections == null || sections.Count == 0) throw new ArgumentException("Sections list is null or empty.");

        var section = sections.FirstOrDefault(s =>
                          virtualAddress >= s.VirtualAddress && virtualAddress < s.VirtualAddress + s.VirtualSize) ??
                      throw new InvalidDataException("Invalid virtual address for directory.");
        var offset = virtualAddress - section.VirtualAddress + section.PointerToRawData;

        var buffer = new byte[size];
        using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        fs.Seek(offset, SeekOrigin.Begin);
        var _ = fs.Read(buffer, 0, buffer.Length);

        return buffer;
    }

    public static string ReadNullTerminatedString(BinaryReader reader)
    {
        var bytes = new List<byte>();
        byte b;
        while ((b = reader.ReadByte()) != 0) bytes.Add(b);
        return Encoding.UTF8.GetString(bytes.ToArray());
    }

    public static string ReadNullTerminatedString(BinaryReader reader, Encoding? encoding)
    {
        var bytes = new List<byte>();
        byte b;
        while ((b = reader.ReadByte()) != 0) bytes.Add(b);
        return (encoding ?? Encoding.Default).GetString(bytes.ToArray());
    }

    public static long RvaToOffset(uint rva, List<SectionInformation> sections)
    {
        foreach (var section in sections)
            if (rva >= section.VirtualAddress && rva < section.VirtualAddress + section.SizeOfRawData)
                return section.PointerToRawData + (rva - section.VirtualAddress);
        throw new InvalidDataException("RVA out of section bounds");
    }

    public static string UndecorateFunctionName(string mangledName)
    {
        var undecoratedName = new StringBuilder(256);
        var result = UnDecorateSymbolName(mangledName, undecoratedName, (uint)undecoratedName.Capacity,
            UndnameComplete);
        return result != 0 ? undecoratedName.ToString() : mangledName;
    }

    public static bool IsPe64Format(BinaryReader reader)
    {
        reader.BaseStream.Seek(0x3C, SeekOrigin.Begin);
        var peHeaderOffset = reader.ReadUInt32();
        reader.BaseStream.Seek(peHeaderOffset, SeekOrigin.Begin);
        var signature = reader.ReadUInt32();
        if (signature != ImageNtSignature) throw new InvalidDataException("Invalid PE signature.");
        reader.BaseStream.Seek(peHeaderOffset + 0x18, SeekOrigin.Begin);
        var magic = reader.ReadUInt16();
        return magic == ImageNtOptionalHdr64Magic;
    }

    public static string CfgFlagsToString(uint flags)
    {
        var sb = new StringBuilder();
        if ((flags & 0x00000100) == 0x00000100) sb.Append("Instrumented, ");
        if ((flags & 0x00000200) == 0x00000200) sb.Append("Instrumented (CFW), ");
        if ((flags & 0x00000400) == 0x00000400) sb.Append("Function Table Present, ");
        if ((flags & 0x00000800) == 0x00000800) sb.Append("Security Cookie Unused, ");
        if ((flags & 0x00001000) == 0x00001000) sb.Append("Protect Delay-load IAT, ");
        if ((flags & 0x00002000) == 0x00002000) sb.Append("Delay-load IAT in its own section, ");
        if ((flags & 0x00004000) == 0x00004000) sb.Append("Export Suppression Info Present, ");
        if ((flags & 0x00008000) == 0x00008000) sb.Append("Enable Export Suppression, ");
        if ((flags & 0x00010000) == 0x00010000) sb.Append("Longjump Table Present, ");
        if ((flags & 0xF0000000) == 0xF0000000) sb.Append("Function Table Size Mask, ");
        return sb.ToString().TrimEnd(',', ' ') + $" (0x{flags:X})";
    }

    public static (DosHeaderInformation, FileHeaderInformation, OptionalInformation, GeneralInformation, List<SectionInformation>, List<DirectoryInformation>, List<ImportInformation>, List<ExportInformation>, LoadConfigInformation) ProcessPeFile(string path)
    {
        if (string.IsNullOrEmpty(path)) Environment.Exit(1);
        using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        using var reader = new BinaryReader(input: fs, Encoding.Default, leaveOpen: true);

        var dosHeader = FromBinaryReader<ImageDosHeader>(reader);
        var dh = GetDosHeader(dosHeader);

        var peSignature = GetPeSignature(fs, reader, dosHeader.e_lfanew);
        VerifySignature(peSignature);

        var fileHeader = FromBinaryReader<ImageFileHeader>(reader);
        var fh = GetFileHeader(fileHeader, peSignature);

        // Jumps back twice after reading the magic number
        var magic = GetMagic(fs, reader);

        dynamic optionalHeader;
        OptionalInformation oi;
        GeneralInformation gi;

        switch (magic)
        {
            case 0x10b:
                optionalHeader = FromBinaryReader<ImageOptionalHeader32>(reader);
                oi = GetOptionalHeader(optionalHeader);
                // Jumps back to the start of the section
                gi = GetGeneralInformation(fileHeader, optionalHeader, fs);
                break;

            case 0x20b:
                optionalHeader = FromBinaryReader<ImageOptionalHeader64>(reader);
                oi = GetOptionalHeader(optionalHeader);
                // Jumps back to the start of the section
                gi = GetGeneralInformation(fileHeader, optionalHeader, fs);
                break;

            default:
                throw new InvalidDataException("Unknown Optional Header format.");
        }

        fs.Seek(dosHeader.e_lfanew + 4 + Marshal.SizeOf(typeof(ImageFileHeader)) + fileHeader.SizeOfOptionalHeader, SeekOrigin.Begin);
        var sections = new List<SectionInformation>();
        for (var i = 0; i < fileHeader.NumberOfSections; i++)
        {
            var section = FromBinaryReader<ImageSectionHeader>(reader);
            var si = GetSectionInformation(section, fs.Name);
            sections.Add(si);
        }

        var directories = new List<DirectoryInformation>();
        var dataDirectories = GetDataDirectories(optionalHeader);
        for (var i = 0; i < dataDirectories.Length; i++)
        {
            var directory = dataDirectories[i];
            if (directory.VirtualAddress == 0 || directory.Size == 0) continue;
            var di = GetDirectoryInformation(i, directory, sections, path);
            directories.Add(di);
        }

        // Get the imports and exports
        var imports = GetImports(fs, optionalHeader.ImportTable, sections);
        var exports = GetExports(fs, optionalHeader.ExportTable, sections);

        var loadConfig = GetLoadConfig(fs, optionalHeader.LoadConfigTable, sections, IsPe64Format(reader));

        return (dh, fh, oi, gi, sections, directories, imports, exports, loadConfig);
    }

    public static bool IsPeFile(string filePath)
    {
        try
        {
            using var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            using var br = new BinaryReader(fs);
            if (br.ReadUInt16() != 0x5A4D)
                return false;

            fs.Seek(0x3C, SeekOrigin.Begin);
            var peHeaderOffset = br.ReadInt32();
            fs.Seek(peHeaderOffset, SeekOrigin.Begin);
            return br.ReadUInt32() == 0x00004550;
        }
        catch
        {
            return false;
        }
    }
}