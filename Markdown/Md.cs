using System.Collections.Generic;

namespace Markdown
{
	public class Md
	{
	    private Dictionary<string, string> tags;
	    private HTMLRenderer renderer;
	    private MdAnalyzer analyzer;
	    public Md(Dictionary<string, string> tags)
	    {
	        this.tags = tags;
            renderer = new HTMLRenderer();
            analyzer = new MdAnalyzer();
	    }
		public string RenderToHtml(string markdown)
		{
			return markdown; 
		}
	}
}