namespace Markdown.Parser
{
    public class Mismatch : IMatchResult
    { 
        public string Data { get; }
        public Mismatch(string data) => Data = data;
    }
}
