using System.Collections.Generic;

namespace Markdown.Lang
{
    class EmptyTag : IToken
    {
        public string MdTag { get; } = null;
        public string HtmlTag { get; } = null;
        public bool HasClosingTag { get; } = false;
        public List<IToken> Children { get; set; } = new List<IToken>();
        public string Content { get; set; }
        public bool IsClosed { get; set; } = true;

        public EmptyTag(string content) => Content = content;

        public bool IsCorrectSurroundingsForOpeningTag(char? prevSymbol, char? nextSymbol) => true;

        public bool IsCorrectSurroundingsForClosingTag(char? prevSymbol, char? nextSymbol) => true;

        public bool IsCorrectNesting(IToken parent) => true;
    }
}
