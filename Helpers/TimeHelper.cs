namespace PEPG.Helpers;

public class TimeHelper
{
    public static DateTime TimeStampToDateTime(uint timeStamp)
    {
        return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(timeStamp);
    }
}