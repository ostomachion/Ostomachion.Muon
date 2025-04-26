using Ostomachion.Muon.Ast;

namespace Ostomachion.Muon.Tests;

public class AstTests
{
    [SetUp]
    public void Setup() { }

    [Test]
    public void EmptyNode_ToString()
    {
        var node = new MuNode("foo");
        var expected = "foo;";
        var actual = node.ToString();
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void SingleChild_ToString()
    {
        var node = new MuNode("foo", new MuNode("bar"));
        var expected = "foo bar;";
        var actual = node.ToString();
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void TwoChildren_ToString()
    {
        var node = new MuNode("foo", new MuNode("bar"), new MuNode("baz"));
        var expected = """
            foo {
                bar;
                baz;
            }
            """.ReplaceLineEndings();

        var actual = node.ToString().ReplaceLineEndings();
        Assert.That(actual, Is.EqualTo(expected));
    }
}
