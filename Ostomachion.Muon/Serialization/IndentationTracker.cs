namespace Ostomachion.Muon.Serialization;

public partial class MuNodeSerializer
{
    private ref struct IndentationTracker
    {
        private ReadOnlySpan<char> _buffer;
        private readonly char _character;
        private readonly int _charactersPerIndentationLevel;

        public int Level { get; private set; }

        public IndentationTracker(MuNodeSerializerOptions options)
        {
            _character = options.UseSpacesForIndentation ? ' ' : '\t';
            _charactersPerIndentationLevel = options.CharactersPerIndentationLevel;
            _buffer = new string(_character, _charactersPerIndentationLevel);
        }

        public ReadOnlySpan<char> GetIndentation()
        {
            var count = Level * _charactersPerIndentationLevel;
            if (count > _buffer.Length)
            {
                Grow(count);
            }

            return _buffer[..count];
        }

        public void Indent() => Level++;

        public void Unindent() => Level--;

        private void Grow(int minimumLength)
        {
            int newLength = Int32.Max(_buffer.Length * 2, minimumLength);
            _buffer = new string(_character, newLength);
        }
    }
}
