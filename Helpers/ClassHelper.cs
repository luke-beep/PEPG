using System.Runtime.InteropServices;
using System.Text;
using PEPG.Models;
using PEPG.Models.Native;
using static PEPG.Helpers.PeHelper;
using static PEPG.Helpers.HashHelper;
using static PEPG.Helpers.EntropyHelper;
using static PEPG.Helpers.TimeHelper;
using static PEPG.Helpers.BinaryHelper;

namespace PEPG.Helpers;

public class ClassHelper
{
    public static DosHeaderInformation GetDosHeader(ImageDosHeader dosHeader)
    {
        var dhi = new DosHeaderInformation
        {
            Magic = dosHeader.e_magic,
            BytesOnLastPage = dosHeader.e_cblp,
            PagesInFile = dosHeader.e_cp,
            Relocations = dosHeader.e_crlc,
            SizeOfHeader = dosHeader.e_cparhdr,
            MinimumExtraParagraphs = dosHeader.e_minalloc,
            MaximumExtraParagraphs = dosHeader.e_maxalloc,
            InitialSs = dosHeader.e_ss,
            InitialSp = dosHeader.e_sp,
            Checksum = dosHeader.e_csum,
            InitialIp = dosHeader.e_ip,
            InitialCs = dosHeader.e_cs,
            AddressOfRelocationTable = dosHeader.e_lfarlc,
            OverlayNumber = dosHeader.e_ovno,
            Oemid = dosHeader.e_oemid,
            OemInfo = dosHeader.e_oeminfo,
            AddressOfNewExeHeader = dosHeader.e_lfanew
        };
        return dhi;
    }

    public static FileHeaderInformation GetFileHeader(ImageFileHeader header, uint signature)
    {
        var fhi = new FileHeaderInformation
        {
            Signature = signature,
            Machine = header.Machine,
            NumberOfSections = header.NumberOfSections,
            TimeDateStamp = TimeStampToDateTime(header.TimeDateStamp),
            PointerToSymbolTable = header.PointerToSymbolTable,
            NumberOfSymbols = header.NumberOfSymbols,
            SizeOfOptionalHeader = header.SizeOfOptionalHeader,
            Characteristics = header.Characteristics
        };
        return fhi;
    }

    public static OptionalInformation GetOptionalHeader(ImageOptionalHeader32 header)
    {
        var oi = new OptionalInformation
        {
            Magic = header.Magic,
            MajorLinkerVersion = header.MajorLinkerVersion,
            MinorLinkerVersion = header.MinorLinkerVersion,
            SizeOfCode = header.SizeOfCode,
            SizeOfInitializedData = header.SizeOfInitializedData,
            SizeOfUninitializedData = header.SizeOfUninitializedData,
            AddressOfEntryPoint = header.AddressOfEntryPoint,
            BaseOfCode = header.BaseOfCode,
            ImageBase = header.ImageBase,
            SectionAlignment = header.SectionAlignment,
            FileAlignment = header.FileAlignment,
            MajorOperatingSystemVersion = header.MajorOperatingSystemVersion,
            MinorOperatingSystemVersion = header.MinorOperatingSystemVersion,
            MajorImageVersion = header.MajorImageVersion,
            MinorImageVersion = header.MinorImageVersion,
            MajorSubsystemVersion = header.MajorSubsystemVersion,
            MinorSubsystemVersion = header.MinorSubsystemVersion,
            Win32VersionValue = header.Win32VersionValue,
            SizeOfImage = header.SizeOfImage,
            SizeOfHeaders = header.SizeOfHeaders,
            CheckSum = header.CheckSum,
            Subsystem = header.Subsystem,
            DllCharacteristics = header.DllCharacteristics,
            SizeOfStackReserve = header.SizeOfStackReserve,
            SizeOfStackCommit = header.SizeOfStackCommit,
            SizeOfHeapReserve = header.SizeOfHeapReserve,
            SizeOfHeapCommit = header.SizeOfHeapCommit,
            LoaderFlags = header.LoaderFlags,
            NumberOfRvaAndSizes = header.NumberOfRvaAndSizes
        };
        return oi;
    }

    public static OptionalInformation GetOptionalHeader(ImageOptionalHeader64 header)
    {
        var oi = new OptionalInformation
        {
            Magic = header.Magic,
            MajorLinkerVersion = header.MajorLinkerVersion,
            MinorLinkerVersion = header.MinorLinkerVersion,
            SizeOfCode = header.SizeOfCode,
            SizeOfInitializedData = header.SizeOfInitializedData,
            SizeOfUninitializedData = header.SizeOfUninitializedData,
            AddressOfEntryPoint = header.AddressOfEntryPoint,
            BaseOfCode = header.BaseOfCode,
            ImageBase = header.ImageBase,
            SectionAlignment = header.SectionAlignment,
            FileAlignment = header.FileAlignment,
            MajorOperatingSystemVersion = header.MajorOperatingSystemVersion,
            MinorOperatingSystemVersion = header.MinorOperatingSystemVersion,
            MajorImageVersion = header.MajorImageVersion,
            MinorImageVersion = header.MinorImageVersion,
            MajorSubsystemVersion = header.MajorSubsystemVersion,
            MinorSubsystemVersion = header.MinorSubsystemVersion,
            Win32VersionValue = header.Win32VersionValue,
            SizeOfImage = header.SizeOfImage,
            SizeOfHeaders = header.SizeOfHeaders,
            CheckSum = header.CheckSum,
            Subsystem = header.Subsystem,
            DllCharacteristics = header.DllCharacteristics,
            SizeOfStackReserve = header.SizeOfStackReserve,
            SizeOfStackCommit = header.SizeOfStackCommit,
            SizeOfHeapReserve = header.SizeOfHeapReserve,
            SizeOfHeapCommit = header.SizeOfHeapCommit,
            LoaderFlags = header.LoaderFlags,
            NumberOfRvaAndSizes = header.NumberOfRvaAndSizes
        };
        return oi;
    }

    public static GeneralInformation GetGeneralInformation(ImageFileHeader fileHeader,
        ImageOptionalHeader32 optionalHeader, FileStream fs)
    {
        var targetMachine = GetMachineType(fileHeader.Machine);
        fs.Seek(0, SeekOrigin.Begin);
        var imageBytes = new byte[fs.Length];
        _ = fs.Read(imageBytes, 0, imageBytes.Length);
        var imageEntropy = CalculateEntropy(imageBytes);
        var headerSpare = fileHeader.SizeOfOptionalHeader;
        var subsystem = GetSubsystem(optionalHeader.Subsystem);
        var characteristics =
            $"{GetImageCharacteristicsDescription(fileHeader.Characteristics)}, {GetDllCharacteristicsDescription(optionalHeader.DllCharacteristics)}";
        var timeStamp = fileHeader.TimeDateStamp;

        var gi = new GeneralInformation
        {
            TargetMachine = targetMachine,
            ImageName = Path.GetFileName(fs.Name),
            ImagePath = fs.Name,
            TimeStamp = TimeStampToDateTime(timeStamp),
            ImageEntropy = imageEntropy,
            ImageBase = optionalHeader.ImageBase,
            ImageSize = File.ReadAllBytes(fs.Name).Length,
            EntryPoint = optionalHeader.AddressOfEntryPoint,
            HeaderChecksum = optionalHeader.CheckSum,
            HeaderSpare = headerSpare,
            Subsystem = subsystem,
            MajorSubsystemVersion = optionalHeader.MajorSubsystemVersion,
            MinorSubsystemVersion = optionalHeader.MinorSubsystemVersion,
            Characteristics = characteristics,
            CreationDate = File.GetLastAccessTimeUtc(fs.Name),
            ModificationDate = File.GetLastAccessTimeUtc(fs.Name),
            UpdatedDate = File.GetLastAccessTimeUtc(fs.Name)
        };
        return gi;
    }

    public static GeneralInformation GetGeneralInformation(ImageFileHeader fileHeader,
        ImageOptionalHeader64 optionalHeader, FileStream fs)
    {
        var targetMachine = GetMachineType(fileHeader.Machine);
        fs.Seek(0, SeekOrigin.Begin);
        var imageBytes = new byte[fs.Length];
        _ = fs.Read(imageBytes, 0, imageBytes.Length);
        var imageEntropy = CalculateEntropy(imageBytes);
        var headerSpare = fileHeader.SizeOfOptionalHeader;
        var subsystem = GetSubsystem(optionalHeader.Subsystem);
        var characteristics =
            $"{GetImageCharacteristicsDescription(fileHeader.Characteristics)}, {GetDllCharacteristicsDescription(optionalHeader.DllCharacteristics)}";
        var timeStamp = fileHeader.TimeDateStamp;

        var gi = new GeneralInformation
        {
            TargetMachine = targetMachine,
            ImageName = Path.GetFileName(fs.Name),
            ImagePath = fs.Name,
            TimeStamp = TimeStampToDateTime(timeStamp),
            ImageEntropy = imageEntropy,
            ImageBase = optionalHeader.ImageBase,
            ImageSize = File.ReadAllBytes(fs.Name).Length,
            EntryPoint = optionalHeader.AddressOfEntryPoint,
            HeaderChecksum = optionalHeader.CheckSum,
            HeaderSpare = headerSpare,
            Subsystem = subsystem,
            MajorSubsystemVersion = optionalHeader.MajorSubsystemVersion,
            MinorSubsystemVersion = optionalHeader.MinorSubsystemVersion,
            Characteristics = characteristics,
            CreationDate = File.GetCreationTime(fs.Name),
            ModificationDate = File.GetLastWriteTime(fs.Name),
            UpdatedDate = File.GetLastAccessTime(fs.Name)
        };
        return gi;
    }

    public static SectionInformation GetSectionInformation(ImageSectionHeader section, string path)
    {
        var buffer = new byte[section.SizeOfRawData];
        Array.Copy(File.ReadAllBytes(path), section.PointerToRawData, buffer, 0, section.SizeOfRawData);
        var si = new SectionInformation
        {
            Name = Encoding.ASCII.GetString(section.Name).TrimEnd('\0'),
            VirtualSize = section.VirtualSize,
            VirtualAddress = section.VirtualAddress,
            SizeOfRawData = section.SizeOfRawData,
            PointerToRawData = section.PointerToRawData,
            PointerToRelocations = section.PointerToRelocations,
            PointerToLinenumbers = section.PointerToLinenumbers,
            NumberOfRelocations = section.NumberOfRelocations,
            NumberOfLinenumbers = section.NumberOfLinenumbers,
            Characteristics = GetSectionCharacteristics(section.Characteristics),
            Hash = CalculateMd5Hash(buffer),
            Entropy = CalculateEntropy(buffer)
        };

        return si;
    }

    public static DirectoryInformation GetDirectoryInformation(int index, ImageDataDirectory directory,
        List<SectionInformation> sections, string path)
    {
        var directoryType = GetDirectoryType(index);
        var virtualAddress = directory.VirtualAddress;
        var size = directory.Size;

        var data = ReadDirectoryData(virtualAddress, size, sections, path);
        var entropy = CalculateEntropy(data);
        var hash = CalculateMd5Hash(data);

        var section = sections.FirstOrDefault(s =>
            virtualAddress >= s.VirtualAddress && virtualAddress < s.VirtualAddress + s.VirtualSize)?.Name;
        if (section !=
            null)
            return new DirectoryInformation
            {
                Name = directoryType,
                VirtualAddress = virtualAddress,
                Size = size,
                Entropy = entropy,
                Section = section,
                Hash = hash
            };
        throw new InvalidDataException("Invalid virtual address for directory.");
    }

    public static List<ImportInformation> GetImports(FileStream fs, ImageDataDirectory importDirectory,
        List<SectionInformation> sections)
    {
        var imports = new List<ImportInformation>();

        if (importDirectory.VirtualAddress == 0 || importDirectory.Size == 0)
        {
            return imports;
        }

        long offset;
        try
        {
            offset = RvaToOffset(importDirectory.VirtualAddress, sections);
        }
        catch (InvalidDataException)
        {
            return imports;
        }

        using var reader = new BinaryReader(fs, Encoding.Default, true);
        var isPe64 = IsPe64Format(reader);

        fs.Seek(offset, SeekOrigin.Begin);

        var descriptorSize = Marshal.SizeOf(typeof(ImageImportDirectory));
        while (true)
        {
            var currentOffset = fs.Position;
            var importDirectoryStruct = FromBinaryReader<ImageImportDirectory>(reader);

            if (importDirectoryStruct is { Name: 0, ImportAddressTable: 0 }) break;

            long nameOffset;
            try
            {
                nameOffset = RvaToOffset(importDirectoryStruct.Name, sections);
                
            }
            catch (InvalidDataException ex)
            {
                fs.Seek(currentOffset + descriptorSize, SeekOrigin.Begin);
                continue;
            }

            fs.Seek(nameOffset, SeekOrigin.Begin);
            var dllName = ReadNullTerminatedString(reader);


            long importLookupTableOffset;
            try
            {
                importLookupTableOffset = RvaToOffset(importDirectoryStruct.ImportLookupTable, sections);
            }
            catch (InvalidDataException ex)
            {
                
                fs.Seek(currentOffset + descriptorSize, SeekOrigin.Begin);
                continue;
            }

            fs.Seek(importLookupTableOffset, SeekOrigin.Begin);
            var importLookupTable = new List<uint>();
            while (true)
            {
                var importLookup = isPe64 ? reader.ReadUInt64() : reader.ReadUInt32();
                if (importLookup == 0) break;
                importLookupTable.Add((uint)importLookup);
                
            }

            foreach (var iltEntry in importLookupTable)
            {
                // Bitmask is 31 for pe32 and 63 for pe64
                if ((iltEntry & (isPe64 ? 0x8000000000000000 : 0x80000000)) != 0)
                {
                    continue;
                }

                long hintNameOffset;
                try
                {
                    hintNameOffset = RvaToOffset(iltEntry, sections);
                    
                }
                catch (InvalidDataException ex)
                {
                    continue;
                }

                fs.Seek(hintNameOffset, SeekOrigin.Begin);
                var hint = reader.ReadUInt16();
                var functionName = ReadNullTerminatedString(reader);
                var undecoratedFunctionName = UndecorateFunctionName(functionName);

                imports.Add(new ImportInformation
                {
                    DllName = dllName,
                    Name = functionName,
                    UndecoratedName = undecoratedFunctionName,
                    Rva = iltEntry & 0x7FFFFFFF,
                    Hint = hint
                });
            }

            offset += descriptorSize;
            fs.Seek(offset, SeekOrigin.Begin);
        }

        return imports;
    }

    public static List<ExportInformation> GetExports(FileStream fs, ImageDataDirectory exportDirectory,
        List<SectionInformation> sections)
    {
        if (exportDirectory.VirtualAddress == 0 || exportDirectory.Size == 0)
        {
            return [];
        }

        var exports = new List<ExportInformation>();

        long offset;
        try
        {
            offset = RvaToOffset(exportDirectory.VirtualAddress, sections);
        }
        catch (InvalidDataException)
        {
            
            return [];
        }

        using var reader = new BinaryReader(fs, Encoding.Default, true);
        fs.Seek(offset, SeekOrigin.Begin);

        var exportDirectoryStruct = FromBinaryReader<ImageExportDirectory>(reader);

        long nameOffset;
        try
        {
            nameOffset = RvaToOffset(exportDirectoryStruct.Name, sections);
        }
        catch (InvalidDataException)
        {
            
            return [];
        }

        fs.Seek(nameOffset, SeekOrigin.Begin);
        var dllName = ReadNullTerminatedString(reader);
        

        long functionsOffset, namesOffset, ordinalsOffset;
        try
        {
            functionsOffset = RvaToOffset(exportDirectoryStruct.AddressOfFunctions, sections);
            namesOffset = RvaToOffset(exportDirectoryStruct.AddressOfNames, sections);
            ordinalsOffset = RvaToOffset(exportDirectoryStruct.AddressOfNameOrdinals, sections);
        }
        catch (InvalidDataException ex)
        {
            
            return [];
        }

        fs.Seek(namesOffset, SeekOrigin.Begin);
        var nameRvAs = new uint[exportDirectoryStruct.NumberOfNames];
        for (var i = 0; i < nameRvAs.Length; i++)
        {
            nameRvAs[i] = reader.ReadUInt32();
        }

        fs.Seek(ordinalsOffset, SeekOrigin.Begin);
        var ordinals = new ushort[exportDirectoryStruct.NumberOfNames];
        for (var i = 0; i < ordinals.Length; i++) ordinals[i] = reader.ReadUInt16();

        fs.Seek(functionsOffset, SeekOrigin.Begin);
        var functionRvAs = new uint[exportDirectoryStruct.NumberOfFunctions];
        for (var i = 0; i < functionRvAs.Length; i++)
        {
            functionRvAs[i] = reader.ReadUInt32();
        }

        for (var i = 0; i < nameRvAs.Length; i++)
        {
            long functionNameOffset;
            try
            {
                functionNameOffset = RvaToOffset(nameRvAs[i], sections);
            }
            catch (InvalidDataException)
            {
                
                continue;
            }

            fs.Seek(functionNameOffset, SeekOrigin.Begin);
            var functionName = ReadNullTerminatedString(reader);

            var functionOrdinal = exportDirectoryStruct.Base + ordinals[i];
            var functionRva = functionRvAs[ordinals[i]];
            long functionAddress;

            try
            {
                functionAddress = RvaToOffset(functionRva, sections);
            }
            catch (InvalidDataException)
            {
                
                continue;
            }

            exports.Add(new ExportInformation
            {
                DllName = dllName,
                Name = functionName,
                Ordinal = functionOrdinal,
                Rva = functionRva,
                Address = functionAddress
            });
        }

        return exports;
    }

    public static LoadConfigInformation? GetLoadConfig(FileStream fs, ImageDataDirectory loadConfigDirectory,
        List<SectionInformation> sections, bool isPe64)
    {
        long offset;
        try
        {
            offset = RvaToOffset(loadConfigDirectory.VirtualAddress, sections);
        }
        catch (InvalidDataException)
        {
            return null;
        }

        fs.Seek(offset, SeekOrigin.Begin);
        using var reader = new BinaryReader(fs, Encoding.Default, true);

        if (isPe64)
        {
            var loadConfig64 = FromBinaryReader<ImageLoadConfigDirectory64>(reader);
            return GetLoadConfigInformation(loadConfig64);
        }

        var loadConfig32 = FromBinaryReader<ImageLoadConfigDirectory32>(reader);
        return GetLoadConfigInformation(loadConfig32);
    }

    private static LoadConfigInformation GetLoadConfigInformation(ImageLoadConfigDirectory32 loadConfig)
    {
        return new LoadConfigInformation
        {
            Size = loadConfig.Size,
            TimeDateStamp = loadConfig.TimeDateStamp,
            MajorVersion = loadConfig.MajorVersion,
            MinorVersion = loadConfig.MinorVersion,
            GlobalFlagsClear = loadConfig.GlobalFlagsClear,
            GlobalFlagsSet = loadConfig.GlobalFlagsSet,
            CriticalSectionDefaultTimeout = loadConfig.CriticalSectionDefaultTimeout,
            DeCommitFreeBlockThreshold = loadConfig.DeCommitFreeBlockThreshold,
            DeCommitTotalFreeThreshold = loadConfig.DeCommitTotalFreeThreshold,
            LockPrefixTable = loadConfig.LockPrefixTable,
            MaximumAllocationSize = loadConfig.MaximumAllocationSize,
            VirtualMemoryThreshold = loadConfig.VirtualMemoryThreshold,
            ProcessAffinityMask = loadConfig.ProcessAffinityMask,
            ProcessHeapFlags = loadConfig.ProcessHeapFlags,
            CsdVersion = loadConfig.CsdVersion,
            DependentLoadFlags = loadConfig.DependentLoadFlags,
            EditList = loadConfig.EditList,
            SecurityCookie = loadConfig.SecurityCookie,
            SeHandlerTable = loadConfig.SEHandlerTable,
            SeHandlerCount = loadConfig.SEHandlerCount,
            GuardCfCheckFunctionPointer = loadConfig.GuardCFCheckFunctionPointer,
            GuardCfDispatchFunctionPointer = loadConfig.GuardCFDispatchFunctionPointer,
            GuardCfFunctionTable = loadConfig.GuardCFFunctionTable,
            GuardCfFunctionCount = loadConfig.GuardCFFunctionCount,
            GuardFlags = CfgFlagsToString(loadConfig.GuardFlags),
            CodeIntegrity = loadConfig.CodeIntegrity,
            GuardAddressTakenIatEntryTable = loadConfig.GuardAddressTakenIatEntryTable,
            GuardAddressTakenIatEntryCount = loadConfig.GuardAddressTakenIatEntryCount,
            GuardLongJumpTargetTable = loadConfig.GuardLongJumpTargetTable,
            GuardLongJumpTargetCount = loadConfig.GuardLongJumpTargetCount,
            DynamicValueRelocTable = loadConfig.DynamicValueRelocTable,
            HybridMetadataPointer = loadConfig.HybridMetadataPointer,
            GuardRfFailureRoutine = loadConfig.GuardRFFailureRoutine,
            GuardRfFailureRoutineFunctionPointer = loadConfig.GuardRFFailureRoutineFunctionPointer,
            DynamicValueRelocTableOffset = loadConfig.DynamicValueRelocTableOffset,
            DynamicValueRelocTableSection = loadConfig.DynamicValueRelocTableSection,
            GuardRfVerifyStackPointerFunctionPointer = loadConfig.GuardRFVerifyStackPointerFunctionPointer,
            HotPatchTableOffset = loadConfig.HotPatchTableOffset,
            EnclaveConfigurationPointer = loadConfig.EnclaveConfigurationPointer,
            VolatileMetadataPointer = loadConfig.VolatileMetadataPointer,
            GuardEhContinuationTable = 0,
            GuardEhContinuationTableEntryCount = 0,
            XfgCheckFunctionPointer = 0,
            XfgDispatchFunctionPointer = 0,
            XfgTableDispatchFunctionPointer = 0,
            CastGuardFailureMode = 0,
            GuardMemcpyFunctionPointer = 0
        };
    }

    private static LoadConfigInformation GetLoadConfigInformation(ImageLoadConfigDirectory64 loadConfig)
    {
        return new LoadConfigInformation
        {
            Size = loadConfig.Size,
            TimeDateStamp = loadConfig.TimeDateStamp,
            MajorVersion = loadConfig.MajorVersion,
            MinorVersion = loadConfig.MinorVersion,
            GlobalFlagsClear = loadConfig.GlobalFlagsClear,
            GlobalFlagsSet = loadConfig.GlobalFlagsSet,
            CriticalSectionDefaultTimeout = loadConfig.CriticalSectionDefaultTimeout,
            DeCommitFreeBlockThreshold = loadConfig.DeCommitFreeBlockThreshold,
            DeCommitTotalFreeThreshold = loadConfig.DeCommitTotalFreeThreshold,
            LockPrefixTable = loadConfig.LockPrefixTable,
            MaximumAllocationSize = loadConfig.MaximumAllocationSize,
            VirtualMemoryThreshold = loadConfig.VirtualMemoryThreshold,
            ProcessAffinityMask = loadConfig.ProcessAffinityMask,
            ProcessHeapFlags = loadConfig.ProcessHeapFlags,
            CsdVersion = loadConfig.CsdVersion,
            DependentLoadFlags = loadConfig.DependentLoadFlags,
            EditList = loadConfig.EditList,
            SecurityCookie = loadConfig.SecurityCookie,
            SeHandlerTable = loadConfig.SEHandlerTable,
            SeHandlerCount = loadConfig.SEHandlerCount,
            GuardCfCheckFunctionPointer = loadConfig.GuardCFCheckFunctionPointer,
            GuardCfDispatchFunctionPointer = loadConfig.GuardCFDispatchFunctionPointer,
            GuardCfFunctionTable = loadConfig.GuardCFFunctionTable,
            GuardCfFunctionCount = loadConfig.GuardCFFunctionCount,
            GuardFlags = CfgFlagsToString(loadConfig.GuardFlags),
            CodeIntegrity = loadConfig.CodeIntegrity,
            GuardAddressTakenIatEntryTable = loadConfig.GuardAddressTakenIatEntryTable,
            GuardAddressTakenIatEntryCount = loadConfig.GuardAddressTakenIatEntryCount,
            GuardLongJumpTargetTable = loadConfig.GuardLongJumpTargetTable,
            GuardLongJumpTargetCount = loadConfig.GuardLongJumpTargetCount,
            DynamicValueRelocTable = loadConfig.DynamicValueRelocTable,
            HybridMetadataPointer = loadConfig.HybridMetadataPointer,
            GuardRfFailureRoutine = loadConfig.GuardRFFailureRoutine,
            GuardRfFailureRoutineFunctionPointer = loadConfig.GuardRFFailureRoutineFunctionPointer,
            DynamicValueRelocTableOffset = loadConfig.DynamicValueRelocTableOffset,
            DynamicValueRelocTableSection = loadConfig.DynamicValueRelocTableSection,
            GuardRfVerifyStackPointerFunctionPointer = loadConfig.GuardRFVerifyStackPointerFunctionPointer,
            HotPatchTableOffset = loadConfig.HotPatchTableOffset,
            EnclaveConfigurationPointer = loadConfig.EnclaveConfigurationPointer,
            VolatileMetadataPointer = loadConfig.VolatileMetadataPointer,
            GuardEhContinuationTable = loadConfig.GuardEHContinuationTable,
            GuardEhContinuationTableEntryCount = loadConfig.GuardEHContinuationTableEntryCount,
            XfgCheckFunctionPointer = loadConfig.XFGCheckFunctionPointer,
            XfgDispatchFunctionPointer = loadConfig.XFGDispatchFunctionPointer,
            XfgTableDispatchFunctionPointer = loadConfig.XFGTableDispatchFunctionPointer,
            CastGuardFailureMode = loadConfig.CastGuardFailureMode,
            GuardMemcpyFunctionPointer = loadConfig.GuardMemcpyFunctionPointer
        };
    }
}