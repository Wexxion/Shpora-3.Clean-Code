using System.Collections.Generic;

namespace Markdown.Lang
{
    public interface IToken
    {
        string MdTag { get; }
        string HtmlTag { get; }
        bool HasClosingTag { get; }
        List<IToken> Children { get; }
        string Content { get; }
        bool IsClosed { get; set; }
        bool IsCorrectSurroundingsForOpeningTag(char? prevSymbol, char? nextSymbol);
        bool IsCorrectSurroundingsForClosingTag(char? prevSymbol, char? nextSymbol);
        bool IsCorrectNesting(IToken parent);
    }
}