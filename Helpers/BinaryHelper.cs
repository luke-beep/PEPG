using System.Runtime.InteropServices;

namespace PEPG.Helpers;

public class BinaryHelper
{
    /// <summary>
    /// Returns a managed structure object with the data from the binary reader.
    /// </summary>
    /// <tyPEPGaram name="T"></tyPEPGaram>
    /// <param name="reader"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static T FromBinaryReader<T>(BinaryReader reader) where T : struct
    {
        var bytes = reader.ReadBytes(Marshal.SizeOf(typeof(T)));
        var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
        try
        {
            return (T)(Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T)) ?? throw new InvalidOperationException());
        }
        finally
        {
            handle.Free();
        }
    }
}