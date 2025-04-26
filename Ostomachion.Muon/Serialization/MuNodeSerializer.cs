using System.Text;
using Ostomachion.Muon.Ast;

namespace Ostomachion.Muon.Serialization;

public class MuNodeSerializer(MuNodeSerializerOptions options)
{
    public static readonly MuNodeSerializer Default = new(MuNodeSerializerOptions.Default);

    public MuNodeSerializer() : this(MuNodeSerializerOptions.Default) { }

    public string Serialize(MuNode node)
    {
        var stringBuilder = new StringBuilder();
        AddToStringBuilder(node, stringBuilder, currentIndentation: "");
        return stringBuilder.ToString();
    }

    private void AddToStringBuilder(MuNode node, StringBuilder stringBuilder, string currentIndentation)
    {
        stringBuilder.Append(currentIndentation);
        stringBuilder.Append(node.Name);
        if (node.Children.Count == 0)
        {
            stringBuilder.Append(';');
        }
        else if (node.Children.Count == 1)
        {
            stringBuilder.Append(' ');
            AddToStringBuilder(node.Children[0], stringBuilder, currentIndentation);
        }
        else
        {
            stringBuilder.Append(" {");
            stringBuilder.AppendLine();
            foreach (var child in node.Children)
            {
                AddToStringBuilder(child, stringBuilder, currentIndentation + options.Indentation);
                stringBuilder.AppendLine();
            }

            stringBuilder.Append(currentIndentation);
            stringBuilder.Append('}');
        }
    }
}
