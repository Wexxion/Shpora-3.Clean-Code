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
            return UsefulThings.IsCorrectPrevSymbolForOpeningTag(prevSymbol)
                   && UsefulThings.IsCorrectNextSymbolForOpeningTag(nextSymbol);
        }

        public bool IsCorrectSurroundingsForClosingTag(char? prevSymbol, char? nextSymbol)
        {
            return UsefulThings.IsCorrectPrevSymbolForClosingTag(prevSymbol)
                   && UsefulThings.IsCorrectNextSymbolForClosingTag(nextSymbol);
        }

        public bool IsCorrectNesting(IToken parent) => !(parent is CursiveTag);
    }
}