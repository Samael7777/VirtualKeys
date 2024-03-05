

// ReSharper disable SwitchStatementHandlesSomeKnownEnumValuesWithDefault

#pragma warning disable CS8509 // The switch expression does not handle all possible values of its input type (it is not exhaustive).
namespace VirtualKeys.Extensions;

public static class KeyExtensions
{
    private static readonly Key[] Modifiers =
    {
        Key.RCONTROL,
        Key.LCONTROL,
        Key.CONTROL,
        Key.RSHIFT,
        Key.LSHIFT,
        Key.SHIFT,
        Key.LWIN,
        Key.RWIN,
        Key.INV_WIN,
        Key.RALT,
        Key.LALT,
        Key.INV_ALT
    };

    private static readonly Key[] SideInvariants =
    {
        Key.SHIFT,
        Key.INV_ALT,
        Key.CONTROL,
        Key.INV_WIN
    };

    public static Modifiers ToModifiers(this Key key)
    {
        if (!key.IsModifier())
            throw new ArgumentException("Only modifiers can be converted to Modifiers.", nameof(key));
        var sideInvariant = key.ToSideInvariant();
        return sideInvariant switch
        {
            Key.SHIFT => VirtualKeys.Modifiers.Shift,
            Key.INV_ALT => VirtualKeys.Modifiers.Alt,
            Key.CONTROL => VirtualKeys.Modifiers.Control,
            Key.INV_WIN => VirtualKeys.Modifiers.Win
        };
    }

    public static bool IsModifier(this Key key) => Modifiers.Contains(key);

    public static bool IsSideInvariant(this Key key) => SideInvariants.Contains(key);

    public static Key ToSideInvariant(this Key key)
    {
        Key result;
        switch (key)
        {
            case Key.RALT:
            case Key.LALT:
                result = Key.INV_ALT;
                break;
            case Key.LWIN:
            case Key.RWIN:
                result = Key.INV_WIN;
                break;
            case Key.LCONTROL:
            case Key.RCONTROL:
                result = Key.CONTROL;
                break;
            case Key.RSHIFT:
            case Key.LSHIFT:
                result = Key.SHIFT;
                break;
            default:
                result = key;
                break;
        }
        return result;
    }

    public static (Key Right, Key Left) FromInvariant(this Key key)
    {
        return key switch
        {
            Key.INV_ALT => (Key.RALT, Key.LALT),
            Key.SHIFT => (Key.RSHIFT, Key.LSHIFT),
            Key.CONTROL => (Key.RCONTROL, Key.LCONTROL),
            Key.INV_WIN => (Key.RWIN, Key.LWIN),
            _ => (key, key)
        };
    }

    public static string DisplayName(this Key key)
    {
        return key switch
        {
            Key.RCONTROL => "RControl",
            Key.LCONTROL => "LControl",
            Key.CONTROL => "Control",
            Key.RSHIFT => "RShift",
            Key.LSHIFT => "LShift",
            Key.SHIFT => "Shift",
            Key.LWIN => "LWin",
            Key.RWIN => "RWin",
            Key.INV_WIN => "Win",
            Key.RALT => "RAlt",
            Key.LALT => "LAlt",
            Key.INV_ALT => "Alt",
            _ => VirtualKeyConverter.ToChar(key).ToString()
        };
    } 
}