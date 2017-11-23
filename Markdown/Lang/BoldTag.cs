using System.Collections.Generic;

namespace Markdown.Lang
{
    class BoldTag : IToken
    {
        public string MdTag { get; }
        public string HtmlTag { get; }
        public bool HasClosingTag { get; }
        public List<IToken> Content { get; }
        public bool IsClosed { get; set; }

        public BoldTag()
        {
            MdTag = "__";
            HtmlTag = "strong";
            HasClosingTag = true;
            Content = new List<IToken>();
        }

        public bool IsCorrectSurroundingsForOpeningTag(char? prevSymbol, char? nextSymbol)
        {
            var prevIsIncorrect = false;
            var nextIsIncorrect = false;
            if (prevSymbol.HasValue)
                prevIsIncorrect = prevSymbol.IsEscaped() || !char.IsWhiteSpace(prevSymbol.Value);
            if (nextSymbol.HasValue)
                nextIsIncorrect = char.IsWhiteSpace(nextSymbol.Value);
            return !(prevIsIncorrect || nextIsIncorrect);
        }

        public bool IsCorrectSurroundingsForClosingTag(char? prevSymbol, char? nextSymbol)
        {
            var prevIsIncorrect = false;
            var nextIsIncorrect = false;
            if (prevSymbol.HasValue)
                prevIsIncorrect = prevSymbol.IsEscaped() || char.IsWhiteSpace(prevSymbol.Value);
            if (nextSymbol.HasValue)
                nextIsIncorrect = !char.IsWhiteSpace(nextSymbol.Value);
            return !(prevIsIncorrect || nextIsIncorrect);
        }

        public bool IsCorrectNesting(IToken parent) => !(parent is CursiveTag);
    }
}
