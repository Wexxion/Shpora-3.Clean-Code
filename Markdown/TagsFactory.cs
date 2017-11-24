using System;
using System.Collections.Generic;
using Markdown.Lang;

namespace Markdown
{
    class TagsFactory
    {
        private readonly Dictionary<string, Func<IToken>> tagCreator;
        public TagsFactory(Dictionary<string, Func<IToken>> tagCreator) 
            => this.tagCreator = tagCreator;

        public IToken Create(string content) => tagCreator.ContainsKey(content) ?
            tagCreator[content].Invoke() : new EmptyTag(content);
    }
}
