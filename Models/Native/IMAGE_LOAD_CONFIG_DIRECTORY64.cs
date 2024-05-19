using System.Runtime.InteropServices;

namespace PEPG.Models.Native;

[StructLayout(LayoutKind.Sequential)]
public struct ImageLoadConfigDirectory64
{
    public uint Size;
    public uint TimeDateStamp;
    public ushort MajorVersion;
    public ushort MinorVersion;
    public uint GlobalFlagsClear;
    public uint GlobalFlagsSet;
    public uint CriticalSectionDefaultTimeout;
    public ulong DeCommitFreeBlockThreshold;
    public ulong DeCommitTotalFreeThreshold;
    public ulong LockPrefixTable; // VA
    public ulong MaximumAllocationSize;
    public ulong VirtualMemoryThreshold;
    public ulong ProcessAffinityMask;
    public uint ProcessHeapFlags;
    public ushort CsdVersion;
    public ushort DependentLoadFlags;
    public ulong EditList; // VA
    public ulong SecurityCookie; // VA
    public ulong SEHandlerTable; // VA
    public ulong SEHandlerCount;
    public ulong GuardCFCheckFunctionPointer; // VA
    public ulong GuardCFDispatchFunctionPointer; // VA
    public ulong GuardCFFunctionTable; // VA
    public ulong GuardCFFunctionCount;
    public uint GuardFlags;
    public ImageLoadConfigCodeIntegrity CodeIntegrity;
    public ulong GuardAddressTakenIatEntryTable; // VA
    public ulong GuardAddressTakenIatEntryCount;
    public ulong GuardLongJumpTargetTable; // VA
    public ulong GuardLongJumpTargetCount;
    public ulong DynamicValueRelocTable; // VA
    public ulong HybridMetadataPointer; // VA
    public ulong GuardRFFailureRoutine; // VA
    public ulong GuardRFFailureRoutineFunctionPointer; // VA
    public uint DynamicValueRelocTableOffset;
    public ushort DynamicValueRelocTableSection;
    public ushort Reserved2;
    public ulong GuardRFVerifyStackPointerFunctionPointer; // VA
    public uint HotPatchTableOffset;
    public uint Reserved3;
    public ulong EnclaveConfigurationPointer; // VA
    public ulong VolatileMetadataPointer; // VA
    public ulong GuardEHContinuationTable; // VA
    public ulong GuardEHContinuationTableEntryCount;
    public ulong XFGCheckFunctionPointer; // VA
    public ulong XFGDispatchFunctionPointer; // VA
    public ulong XFGTableDispatchFunctionPointer; // VA
    public ulong CastGuardFailureMode; // VA
    public ulong GuardMemcpyFunctionPointer; // VA
}
