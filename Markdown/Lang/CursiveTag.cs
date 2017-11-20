using System.Collections.Generic;

namespace Markdown.Lang
{
    class CursiveTag : IToken
    {
        public string MdTag { get; }
        public string HtmlTag { get; }
        public bool HasClosingTag { get; }
        public List<IToken> Content { get; }
        
        public CursiveTag()
        {
            MdTag = "_";
            HtmlTag = "em";
            HasClosingTag = true;
            Content = new List<IToken>();
        }

        public bool IsCorrectSurroundingsForOpeningTag(char? prevSymbol, char? nextSymbol)
            => true;

        public bool IsCorrectSurroundingsForClosingTag(char? prevSymbol, char? nextSymbol)
            => true;

        public bool IsCorrectNesting(IToken parent) => true;
    }
}
