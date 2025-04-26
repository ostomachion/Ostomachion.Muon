using Ostomachion.Muon.Serialization;

namespace Ostomachion.Muon.Ast;

public record class MuNode(string Name, params IReadOnlyList<MuNode> Children)
{
    public override string ToString() => MuNodeSerializer.Default.Serialize(this);

    public string ToString(MuNodeSerializerOptions options) => new MuNodeSerializer(options).Serialize(this);
}
