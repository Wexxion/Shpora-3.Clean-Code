using System.Collections.Generic;
using System.Linq;
using Markdown.Lang;
using Markdown.Parser;

namespace Markdown.SyntaxTree
{
    internal class SyntaxTreeBuilder
    {
        private readonly TagsFactory tagsFactory;

        private Tree<IToken, string> current;
        private Tree<IToken, string> root;
        private Stack<IToken> stack;

        public SyntaxTreeBuilder(TagsFactory factory)
        {
            tagsFactory = factory;
            Clear();
        }

        public Tree<IToken, string> GetTree() => root;

        public void Clear()
        {
            root = new Tree<IToken, string>();
            stack = new Stack<IToken>();
            current = root;
        }

        public void CloseNotPairedTags()
        {
            var stackList = stack.ToList();

            var toRemove = stack.Where(token => !token.HasClosingTag);
            foreach (var token in toRemove)
            {
                stackList.Remove(token);
                token.IsClosed = true;
                if (current.Value == token)
                    current = current.Parent;
            }

            stack.Clear();
            stackList.Reverse();

            foreach (var token in stackList)
                stack.Push(token);
        }

        public void Append(IMatchResult matchResult)
        {
            if (stack.Count == 0)
            {
                if (matchResult is Match match)
                    SetNewCurrentAndAddTag(match, false);
                else
                    current.AddContent(matchResult.Content);
            }
            else
            {
                if (matchResult is Match match)
                    if (stack.Peek().MdTag == matchResult.Content)
                        CloseCurrentTag(match);
                    else
                        SetNewCurrentAndAddTag(match, true);
                else
                    current.AddContent(matchResult.Content);
            }
        }

        private void CloseCurrentTag(Match matchResult)
        {
            var tag = tagsFactory.Create(matchResult.Content);

            if (!(tag.IsCorrectSurroundingsForClosingTag(matchResult.PrevSymbol, matchResult.NextSymbol)
                  && tag.IsCorrectNesting(current.Parent.Value)))
            {
                current.AddContent(matchResult.Content);
            }
            else
            {
                current.Value.IsClosed = true;
                current = current.Parent;
                stack.Pop();
            }
        }

        private void SetNewCurrentAndAddTag(Match matchResult, bool nesting)
        {
            var tag = tagsFactory.Create(matchResult.Content);
            if (nesting && !tag.IsCorrectNesting(current.Value))
            {
                current.AddContent(matchResult.Content);
                return;
            }
            if (!tag.IsCorrectSurroundingsForOpeningTag(matchResult.PrevSymbol, matchResult.NextSymbol))
            {
                current.AddContent(matchResult.Content);
                return;
            }

            if (nesting)
            {
                stack.Push(tag);
                current.Value.Children.Add(tag);
                current = current.AddChild(tag);
            }
            else
            {
                stack.Push(tag);
                current = current.AddChild(tag);
            }
        }
    }
}