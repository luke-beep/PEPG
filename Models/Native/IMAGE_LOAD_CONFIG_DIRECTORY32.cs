using System.Runtime.InteropServices;

namespace PEPG.Models.Native;

[StructLayout(LayoutKind.Sequential)]
public struct ImageLoadConfigDirectory32
{
    public uint Size;
    public uint TimeDateStamp;
    public ushort MajorVersion;
    public ushort MinorVersion;
    public uint GlobalFlagsClear;
    public uint GlobalFlagsSet;
    public uint CriticalSectionDefaultTimeout;
    public uint DeCommitFreeBlockThreshold;
    public uint DeCommitTotalFreeThreshold;
    public uint LockPrefixTable; // VA
    public uint MaximumAllocationSize;
    public uint VirtualMemoryThreshold;
    public uint ProcessAffinityMask;
    public uint ProcessHeapFlags;
    public ushort CsdVersion;
    public ushort DependentLoadFlags;
    public uint EditList; // VA
    public uint SecurityCookie; // VA
    public uint SEHandlerTable; // VA
    public uint SEHandlerCount;
    public uint GuardCFCheckFunctionPointer; // VA
    public uint GuardCFDispatchFunctionPointer; // VA
    public uint GuardCFFunctionTable; // VA
    public uint GuardCFFunctionCount;
    public uint GuardFlags;
    public ImageLoadConfigCodeIntegrity CodeIntegrity;
    public uint GuardAddressTakenIatEntryTable; // VA
    public uint GuardAddressTakenIatEntryCount;
    public uint GuardLongJumpTargetTable; // VA
    public uint GuardLongJumpTargetCount;
    public uint DynamicValueRelocTable; // VA
    public uint HybridMetadataPointer; // VA
    public uint GuardRFFailureRoutine; // VA
    public uint GuardRFFailureRoutineFunctionPointer; // VA
    public uint DynamicValueRelocTableOffset;
    public ushort DynamicValueRelocTableSection;
    public ushort Reserved2;
    public uint GuardRFVerifyStackPointerFunctionPointer; // VA
    public uint HotPatchTableOffset;
    public uint Reserved3;
    public uint EnclaveConfigurationPointer; // VA
    public uint VolatileMetadataPointer; // VA
}
