namespace PEPG.Helpers;

public class EntropyHelper
{
    public static double CalculateEntropy(byte[] data)
    {
        var counts = new int[256];
        foreach (var b in data) counts[b]++;

        var entropy = 0.0;
        for (var i = 0; i < 256; i++)
            if (counts[i] > 0)
                entropy -= (double)counts[i] / data.Length * Math.Log((double)counts[i] / data.Length, 2);
        
        return entropy;
    }
}