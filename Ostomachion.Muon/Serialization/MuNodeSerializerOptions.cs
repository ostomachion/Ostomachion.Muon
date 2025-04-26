namespace Ostomachion.Muon.Serialization;

public readonly record struct MuNodeSerializerOptions()
{
    public static readonly MuNodeSerializerOptions Default = new();

    public bool UseSpacesForIndentation { get; init; } = true;
    public int CharactersPerIndentationLevel {
        get;
        init
        {
            ArgumentOutOfRangeException.ThrowIfNegative(value, nameof(value));
            field = value;
        }
    } = 4;

    public string Indentation => new(UseSpacesForIndentation ? ' ' : '\t', CharactersPerIndentationLevel);
}
