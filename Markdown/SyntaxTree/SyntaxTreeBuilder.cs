using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Markdown.Lang;
using Markdown.Parser;

namespace Markdown.SyntaxTree
{
    class SyntaxTreeBuilder
    {
        private readonly TagsFactory tagsFactory;
        private Stack<IToken> stack;
        private Tree<IToken> root;

        private Tree<IToken> current;

        public SyntaxTreeBuilder(TagsFactory factory)
        {
            tagsFactory = factory;
            Clear();
        }

        public Tree<IToken> GetTree() => root;
        public void Clear()
        {
            root = new Tree<IToken>();
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
                    SetNewCurrentAndAddTag(match, nesting: false);
                else
                    current.AppendContent(new TagContent(matchResult.Content));
            }
            else
            {
                if (matchResult is Match match)
                {   
                    if (stack.Peek().MdTag == matchResult.Content)
                        CloseCurrentTag(match);
                    else
                        SetNewCurrentAndAddTag(match, nesting: true);
                }
                else
                    current.Value.Content.Add(new TagContent(matchResult.Content));
            }
        }

        private void CloseCurrentTag(Match matchResult)
        {
            var tag = tagsFactory.GetTagFromContent(matchResult.Content);

            if (!(tag.IsCorrectSurroundingsForClosingTag(matchResult.PrevSymbol, matchResult.NextSymbol)
                && tag.IsCorrectNesting(current.Parent.Value)))
                current.AppendContent(new TagContent(matchResult.Content));
            else
            {
                current.Value.IsClosed = true;
                current = current.Parent;
                stack.Pop();
            }
        }

        private void SetNewCurrentAndAddTag(Match matchResult, bool nesting)
        {
            var tag = tagsFactory.GetTagFromContent(matchResult.Content);
            if (nesting && !tag.IsCorrectNesting(current.Value))
            {
                tag = new TagContent(matchResult.Content);
                current.Value.Content.Add(tag);
                return;
            }
            if (!tag.IsCorrectSurroundingsForOpeningTag(matchResult.PrevSymbol, matchResult.NextSymbol))
            {
                current.AppendContent(new TagContent(matchResult.Content));
                return;
            }
                
            if (nesting)
            {
                stack.Push(tag);
                current.Value.Content.Add(tag);
                current = current.AppendContent(tag);
            }
            else
            {
                stack.Push(tag);
                current = current.AppendContent(tag);
            }
            
        }
    }
}