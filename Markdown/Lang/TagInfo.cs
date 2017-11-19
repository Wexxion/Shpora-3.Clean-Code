namespace Markdown.Lang
{
    public class TagInfo
    {
        public TagInfo(string tag, bool hasClosingTag)
        {
            Tag = tag;
            ClosingTag = hasClosingTag;
        }

        public string Tag { get; }
        public bool ClosingTag { get; }
    }
}