using PEPG.Models.Native;

namespace PEPG.Models;

public class LoadConfigInformation
{
    public uint Size { get; set; }
    public uint TimeDateStamp { get; set; }
    public ushort MajorVersion { get; set; }
    public ushort MinorVersion { get; set; }
    public uint GlobalFlagsClear { get; set; }
    public uint GlobalFlagsSet { get; set; }
    public uint CriticalSectionDefaultTimeout { get; set; }
    public ulong DeCommitFreeBlockThreshold { get; set; }
    public ulong DeCommitTotalFreeThreshold { get; set; }
    public ulong LockPrefixTable { get; set; }
    public ulong MaximumAllocationSize { get; set; }
    public ulong VirtualMemoryThreshold { get; set; }
    public ulong ProcessAffinityMask { get; set; }
    public uint ProcessHeapFlags { get; set; }
    public ushort CsdVersion { get; set; }
    public ushort DependentLoadFlags { get; set; }
    public ulong EditList { get; set; }
    public ulong SecurityCookie { get; set; }
    public ulong SeHandlerTable { get; set; }
    public ulong SeHandlerCount { get; set; }
    public ulong GuardCfCheckFunctionPointer { get; set; }
    public ulong GuardCfDispatchFunctionPointer { get; set; }
    public ulong GuardCfFunctionTable { get; set; }
    public ulong GuardCfFunctionCount { get; set; }
    public string? GuardFlags { get; set; }
    public ImageLoadConfigCodeIntegrity CodeIntegrity { get; set; }
    public ulong GuardAddressTakenIatEntryTable { get; set; }
    public ulong GuardAddressTakenIatEntryCount { get; set; }
    public ulong GuardLongJumpTargetTable { get; set; }
    public ulong GuardLongJumpTargetCount { get; set; }
    public ulong DynamicValueRelocTable { get; set; }
    public ulong HybridMetadataPointer { get; set; }
    public ulong GuardRfFailureRoutine { get; set; }
    public ulong GuardRfFailureRoutineFunctionPointer { get; set; }
    public uint DynamicValueRelocTableOffset { get; set; }
    public ushort DynamicValueRelocTableSection { get; set; }
    public ulong GuardRfVerifyStackPointerFunctionPointer { get; set; }
    public uint HotPatchTableOffset { get; set; }
    public ulong EnclaveConfigurationPointer { get; set; }
    public ulong VolatileMetadataPointer { get; set; }
    public ulong GuardEhContinuationTable { get; set; }
    public ulong GuardEhContinuationTableEntryCount { get; set; }
    public ulong XfgCheckFunctionPointer { get; set; }
    public ulong XfgDispatchFunctionPointer { get; set; }
    public ulong XfgTableDispatchFunctionPointer { get; set; }
    public ulong CastGuardFailureMode { get; set; }
    public ulong GuardMemcpyFunctionPointer { get; set; }
}

