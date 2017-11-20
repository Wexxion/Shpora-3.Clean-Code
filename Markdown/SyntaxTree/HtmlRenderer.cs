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
                RecursiveEnumeration(rootToken, sb);
            return sb.ToString();
        }

        public static void RecursiveEnumeration(IToken currenToken, StringBuilder sb)
        {
            if (currenToken is TagContent)
                sb.Append(((TagContent)currenToken).Data);
            else
            {
                var contentSb = new StringBuilder();
                foreach (var token in currenToken.Content)
                    RecursiveEnumeration(token, contentSb);
                sb.Append(UsefulThings.ConverToHtml(currenToken.HtmlTag, contentSb.ToString()));
            }
        }
    }
}