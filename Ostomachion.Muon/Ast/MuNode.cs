namespace Ostomachion.Muon.Ast;

public record class MuNode(string Name, params IReadOnlyList<MuNode> Children);
