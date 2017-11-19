using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markdown.Lang.Realizations
{
    class TagContent : IToken
    {
        public TagInfo MarkInfo { get; } = null;
        public TagInfo HtmlInfo { get; } = null;
        public List<IToken> Content { get; } = null;
        private readonly string data;
        public TagContent(string content) => data = content;
        public string Convert() => data;
        public bool NextSymbolIsCorrect(char symbol) => true;
    }
}
