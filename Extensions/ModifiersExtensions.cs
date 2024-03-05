namespace VirtualKeys.Extensions;

public static class ModifiersExtensions
{
    public static List<Modifiers> GetKeyFlags(this Modifiers modifiers)
    {
        if (modifiers is Modifiers.None) 
            return new List<Modifiers>();
       
        return Enum.GetValues<Modifiers>()
            .Where(v => (v is not Modifiers.None and not Modifiers.NoRepeat) 
                        && modifiers.HasFlag(v))
            .ToList();
    }

    public static string ToDisplayName(this Modifiers modifiers)
    {
        return string.Join(" + ", GetKeyFlags(modifiers).Select(Enum.GetName));
    }
}