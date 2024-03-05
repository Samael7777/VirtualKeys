using VirtualKeys.PInvoke;

// ReSharper disable IdentifierTypo

namespace VirtualKeys;

public static class VirtualKeyConverter
{
    // ReSharper disable once InconsistentNaming
    private const int MAPVK_VK_TO_CHAR = 2;

    public static (Key Key, bool ShiftState) FromChar(char ch)
    {
            var result = User32.VkKeyScan(ch);
            var key = (Key)(result & 0xFF);
            var shift = (result & 0xFF00) > 0;
            return (key, shift);
    }

    public static char ToChar(Key key)
    {
        if (key == Key.NOKEY) return (char)0;

        var charCode = User32.MapVirtualKey((int)key, MAPVK_VK_TO_CHAR);
        return (char)(charCode & 0xFF);
    }
}