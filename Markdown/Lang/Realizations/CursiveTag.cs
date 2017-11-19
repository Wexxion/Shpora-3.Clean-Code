using System.Collections.Generic;

namespace Markdown.Lang.Realizations
{
    class CursiveTag : IToken
    {
        public TagInfo MarkInfo { get; }
        public TagInfo HtmlInfo { get; }
        public List<IToken> Content { get; }

        public CursiveTag()
        {
            MarkInfo = new TagInfo("_", true);
            HtmlInfo = new TagInfo("en", true);
            Content = new List<IToken>();
        }
        public string Convert()
        {
            throw new System.NotImplementedException();
        }

        public bool NextSymbolIsCorrect(char symbol)
        {
            throw new System.NotImplementedException();
        }
    }
}
