using System.Collections.Generic;

namespace Markdown.Lang
{
    public interface IToken
    {
        TagInfo MarkInfo { get; }
        TagInfo HtmlInfo { get; }
        List<IToken> Content { get; }
        string Convert();
        bool NextSymbolIsCorrect(char symbol);
    }

    public static class TokenExtesions
    {
        public static string ConvertToHtml(this IToken token, string content)
        {
            return $"<{token.HtmlInfo.Tag}>{content}</{token.HtmlInfo.Tag}>";
        }
    }
}