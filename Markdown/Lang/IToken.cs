using System.Collections.Generic;

namespace Markdown.Lang
{
    public interface IToken
    {
        string MdTag { get; }
        string HtmlTag { get; }
        bool HasClosingTag { get; }
        List<IToken> Content { get; }
        bool IsCorrectSurroundingsForOpeningTag(char? prevSymbol, char? nextSymbol);
        bool IsCorrectSurroundingsForClosingTag(char? prevSymbol, char? nextSymbol);
        bool IsCorrectNesting(IToken parent);
    }
}