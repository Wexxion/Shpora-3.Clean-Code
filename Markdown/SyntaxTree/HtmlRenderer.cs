using System;
using System.Linq;
using System.Text;
using Markdown.Lang;
using NUnit.Framework.Constraints;

namespace Markdown.SyntaxTree
{
    class HtmlRenderer
    {
        public string Render(Tree<IToken> syntaxTree)
        {
            var sb = new StringBuilder();
            var rootTokens = syntaxTree.Children.Select(x => x.Value).ToArray();
            foreach (var rootToken in rootTokens)
                sb.Append(rootToken.ConverToHtml());
            return sb.ToString();
        }
    }
}