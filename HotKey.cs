using System.Text;
using VirtualKeys.Extensions;

namespace VirtualKeys;

public class HotKey : IEquatable<HotKey>
{
    public static HotKey Empty { get; } = new (Modifiers.None, Key.NOKEY);

    public HotKey(Modifiers modifiers, Key key)
    {
        if (key.IsModifier())
        {
            modifiers |= key.ToModifiers();
            key = Key.NOKEY;
        }

        Modifiers = modifiers;
        Key = key;
    }

    public Modifiers Modifiers { get; }
    public Key Key { get; }
    public bool IsEmpty => Modifiers == Modifiers.None && Key == Key.NOKEY;

    #region Equals

    public bool Equals(HotKey? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Modifiers.GetKeyFlags().SequenceEqual(other.Modifiers.GetKeyFlags()) 
               && Key == other.Key;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == this.GetType() && Equals((HotKey)obj);
    }

    public static bool operator ==(HotKey? left, HotKey? right) => left?.Equals(right) ?? false;

    public static bool operator !=(HotKey? left, HotKey? right) => !(left == right);

    public override int GetHashCode()
    {
        return HashCode.Combine((int)Modifiers, (int)Key);
    }

    #endregion

    public override string ToString()
    {
        if (IsEmpty) return "(None)";

        var sb = new StringBuilder();
        if (Modifiers != Modifiers.None)
            sb.Append(Modifiers.ToDisplayName());
        if (Key != Key.NOKEY)
        {
            sb.Append(" + ");
            sb.Append(VirtualKeyConverter.ToChar(Key));
        }

        sb.Append(Modifiers.HasFlag(Modifiers.NoRepeat) ? " (No repeatable)" : " (Repeatable)");
        return sb.ToString();
    }
}