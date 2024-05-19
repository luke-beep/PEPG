namespace PEPG.Helpers;

public class NumberHelper
{
    public static uint SwapEndianness(uint value)
    {
        return (value & 0x000000FFU) << 24 | (value & 0x0000FF00U) << 8 | (value & 0x00FF0000U) >> 8 | (value & 0xFF000000U) >> 24;
    }

    public static ushort SwapEndianness(ushort value)
    {
        return (ushort)((value & 0x00FF) << 8 | (value & 0xFF00) >> 8);
    }

}