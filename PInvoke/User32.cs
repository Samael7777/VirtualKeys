using System.Runtime.InteropServices;

// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming
// ReSharper disable CommentTypo

namespace VirtualKeys.PInvoke;

internal static class User32
{
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    public static extern short VkKeyScan(char ch);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    public static extern int MapVirtualKey(int uCode, int uMapType);
}