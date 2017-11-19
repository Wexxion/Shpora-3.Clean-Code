using System.Collections.Generic;

namespace Markdown.Lang.Realizations
{
    class BoldTag : IToken
    {
        public TagInfo MarkInfo { get; }
        public TagInfo HtmlInfo { get; }
        public List<IToken> Content { get; }

        public BoldTag()
        {
            MarkInfo = new TagInfo("__", true);
            HtmlInfo = new TagInfo("strong", true);
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
