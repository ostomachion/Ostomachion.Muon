using System.Diagnostics.CodeAnalysis;
using Ostomachion.Muon.Ast;

namespace Ostomachion.Muon.Serialization;

public partial class MuNodeConsumer(MuNode node)
{
    private string? _name = node.Name;

    public bool TryConsumeName([NotNullWhen(true)] out string? name)
    {
        name = _name;
        _name = null;
        return name is not null;
    }

    public int ChildCount { get; } = node.Children.Count;

    public IEnumerator<MuNodeConsumer> ChildEnumerator { get; } = node.Children.Select(x => new MuNodeConsumer(x)).GetEnumerator();
}
