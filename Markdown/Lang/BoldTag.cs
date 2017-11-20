using System.Collections.Generic;

namespace Markdown.Lang
{
    class BoldTag : IToken
    {
        public string MdTag { get; }
        public string HtmlTag { get; }
        public bool HasClosingTag { get; }
        public List<IToken> Content { get; }

        public BoldTag()
        {
            MdTag = "__";
            HtmlTag = "strong";
            HasClosingTag = true;
            Content = new List<IToken>();
        }

        public bool IsCorrectSurroundingsForOpeningTag(char? prevSymbol, char? nextSymbol)
            => true;

        public bool IsCorrectSurroundingsForClosingTag(char? prevSymbol, char? nextSymbol)
            => true;

        public bool IsCorrectNesting(IToken parent) => !(parent is CursiveTag);
    }
}
