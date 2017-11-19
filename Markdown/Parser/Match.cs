namespace Markdown.Parser
{
    public class Match : IMatchResult
    {
        public string Data { get; }
        public char? NextSymbol { get; }
        public char? PrevSymbol { get; }

        public Match(string data, char? prevSymbol, char? nextSymbol)
        {
            Data = data;
            PrevSymbol = prevSymbol;
            NextSymbol = nextSymbol;
        }
    }
}
