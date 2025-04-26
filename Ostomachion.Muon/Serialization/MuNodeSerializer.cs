using System.Text;
using Ostomachion.Muon.Ast;

namespace Ostomachion.Muon.Serialization;

public partial class MuNodeSerializer(MuNodeSerializerOptions options)
{
    public static readonly MuNodeSerializer Default = new(MuNodeSerializerOptions.Default);

    public string _indentation = new(options.UseSpacesForIndentation ? ' ' : '\t', options.CharactersPerIndentationLevel);

    public MuNodeSerializer() : this(MuNodeSerializerOptions.Default) { }

    public string Serialize(MuNode node)
    {
        var stringBuilder = new StringBuilder();
        var indentationTracker = new IndentationTracker(options);
        var continueOnSameLine = true;
        var stack = new Stack<MuNodeConsumer>();
        stack.Push(new MuNodeConsumer(node));

        while (stack.Count > 0)
        {
            var consumer = stack.Peek();
            if (consumer.TryConsumeName(out var name))
            {
                if (!continueOnSameLine)
                {
                    stringBuilder.AppendLine();
                    stringBuilder.Append(indentationTracker.GetIndentation());
                }

                stringBuilder.Append(name);
                continueOnSameLine = consumer.ChildCount == 1;

                if (consumer.ChildCount > 1)
                {
                    stringBuilder.Append( " {");
                    indentationTracker.Indent();
                }
                else if (consumer.ChildCount == 1)
                {
                    stringBuilder.Append(' ');
                }
            }
            else if (consumer.ChildEnumerator.MoveNext())
            {
                stack.Push(consumer.ChildEnumerator.Current);
            }
            else
            {
                var parent = stack.Pop();
                if (parent.ChildCount == 0)
                {
                    stringBuilder.Append(';');
                }
                else if (parent.ChildCount > 1)
                {
                    indentationTracker.Unindent();
                    stringBuilder.AppendLine();
                    stringBuilder.Append(indentationTracker.GetIndentation());
                    stringBuilder.Append('}');
                }
            }
        }

        return stringBuilder.ToString();
    }
}
