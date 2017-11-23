namespace Markdown.Parser
{
    public class Match : IMatchResult
    {
        public string Content { get; }
        public char? NextSymbol { get; }
        public char? PrevSymbol { get; }

        public Match(string content, char? prevSymbol, char? nextSymbol)
        {
            Content = content;
            PrevSymbol = prevSymbol;
            NextSymbol = nextSymbol;
        }
    }
}
