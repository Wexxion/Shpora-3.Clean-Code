using System.Collections.Generic;

namespace Markdown.Lang
{
    class TagContent : IToken
    {
        public string MdTag { get; } = null;
        public string HtmlTag { get; } = null;
        public bool HasClosingTag { get; } = false;
        public List<IToken> Content { get; set; } = null;

        public readonly string Data;
        public TagContent(string content) => Data = content;

        public bool IsCorrectSurroundingsForOpeningTag(char? prevSymbol, char? nextSymbol)
            => true;
        public bool IsCorrectSurroundingsForClosingTag(char? prevSymbol, char? nextSymbol)
            => true;
        public bool IsCorrectNesting(IToken parent) => true;
    }
}
