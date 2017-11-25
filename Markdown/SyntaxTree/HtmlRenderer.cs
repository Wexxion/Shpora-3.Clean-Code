using System;
using System.Linq;
using System.Text;
using Markdown.Lang;
using NUnit.Framework.Constraints;

namespace Markdown.SyntaxTree
{
    class HtmlRenderer
    {
        public string Render(Tree<IToken, string> syntaxTree)
        {
            var sb = new StringBuilder();
            foreach (var child in syntaxTree.Children)
                sb.Append(RecursiveDFS(child));
            return sb.ToString();
        }

        private string RecursiveDFS(Tree<IToken, string> node)
        {
            if (node.IsLeaf)
                return node.Content;
            var tag = node.Value;
            var tagContent = string.Join("", node.Children.Select(RecursiveDFS));
            return tag.IsClosed ? UsefulThings.ConverToHtml(tag.HtmlTag, tagContent) 
                : $"{tag.MdTag}{tagContent}";
        }
    }
}