using System;
using System.Collections.Generic;
using System.Linq;
using Markdown.Lang;
using Markdown.Parser;
using Markdown.SyntaxTree;

namespace Markdown
{
    public class Md
    {
        private readonly Tokenizer tokenizer;
        private readonly HtmlRenderer html;
        private readonly SyntaxTreeBuilder syntaxTree;

        public Md()
        {
            tokenizer = new Tokenizer();
            html = new HtmlRenderer();

            var tags = GetAllTags();
            foreach (var tag in tags.Keys)
                tokenizer.Add(tag);
            tokenizer.Build();
            var factory = new TagsFactory(tags);
            syntaxTree = new SyntaxTreeBuilder(factory);
        }

        private Dictionary<string, Func<IToken>> GetAllTags()
        {
            return UsefulThings
                .GetDefaultConstuctorsOf<IToken>()
                .ToDictionary(key => key().MdTag);
        }

        public string RenderToHtml(string markdown)
        {
            syntaxTree.Clear();
            foreach (var line in markdown.Split('\n'))
            {
                foreach (var matchResult in tokenizer.GetAllTokens(line))
                    syntaxTree.Append(matchResult);
                syntaxTree.CloseNotPairedTags();
            }
            return html.Render(syntaxTree.GetTree());
        }
    }
}