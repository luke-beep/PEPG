using System.Runtime.InteropServices;
using System.Text;

namespace PEPG.Helpers;

public class NativeHelper
{
    [DllImport("DbgHelp.dll", CharSet = CharSet.Ansi)]
    public static extern uint UnDecorateSymbolName(
        string name,
        StringBuilder outputString,
        uint maxStringLength,
        uint flags);

    public const uint UndnameComplete = 0x0000;  
    public const uint UndnameNameOnly = 0x1000;  
}