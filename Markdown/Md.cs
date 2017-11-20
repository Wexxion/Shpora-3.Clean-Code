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
        private readonly SyntaxTreeBuilder builder;

        public Md()
        {
            tokenizer = new Tokenizer();
            html = new HtmlRenderer();

            var tags = GetAllTags();
            foreach (var tag in tags.Keys)
                tokenizer.Add(tag);
            tokenizer.Build();

            builder = new SyntaxTreeBuilder(tags);
        }

        private Dictionary<string, Func<IToken>> GetAllTags()
        {
            return UsefulThings
                .GetDefaultConstuctorsOf<IToken>()
                .ToDictionary(key => key().MdTag);
        }

        public string RenderToHtml(string markdown)
        {
            foreach (var line in markdown.Split('\n'))
            {
                var newLine = true;
                foreach (var matchResult in tokenizer.FindAll(line))
                {
                    builder.Append(matchResult);
                    if (newLine)
                    {
                        builder.CloseNotPairedTags();
                        newLine = false;
                    }
                }
            }
            return html.Render(builder.Tree);
        }
    }
}