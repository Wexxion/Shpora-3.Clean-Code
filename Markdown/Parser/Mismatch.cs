namespace Markdown.Parser
{
    public class Mismatch : IMatchResult
    { 
        public string Content { get; }
        public Mismatch(string data) => Content = data;
    }
}
