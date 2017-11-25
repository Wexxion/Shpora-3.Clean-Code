using System.Collections.Generic;

namespace Markdown.Lang
{
    class CursiveTag : IToken
    {
        public string MdTag { get; }
        public string HtmlTag { get; }
        public bool HasClosingTag { get; }
        public List<IToken> Children { get; }
        public bool IsClosed { get; set; }

        public CursiveTag()
        {
            MdTag = "_";
            HtmlTag = "em";
            HasClosingTag = true;
            Children = new List<IToken>();
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

        public bool IsCorrectNesting(IToken parent) => true;
    }
}
