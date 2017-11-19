using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Markdown.Lang;
using Markdown.Parser;

namespace Markdown
{
	public class Md
	{
        private readonly Tokenizer tokenizer = new Tokenizer();
        private readonly HtmlRenderer renderer = new HtmlRenderer();
	    private Stack<IToken> stack;
	    private Dictionary<string, Func<IToken>> tags;
	    public Md()
	    {
	        tags = GetAllTags();
	        foreach (var tag in tags.Keys)
	            tokenizer.Add(tag);
	    }

	    private Dictionary<string, Func<IToken>> GetAllTags()
	    {
	        return UsefulThings
                .GetDefaultConstuctorsOf<IToken>()
	            .ToDictionary(key => key().MarkInfo.Tag);
	    }

        public string RenderToHtml(string markdown)
		{
		    var syntaxTree = new List<IToken>();

            foreach (var matchResult in tokenizer.FindAll(markdown))
		        syntaxTree.Add(GetNextToken(matchResult));

		    return renderer.Render(syntaxTree);
		}

	    private IToken GetNextToken(IMatchResult result)
	    {
	        throw new NotImplementedException();
	    }
	}
}