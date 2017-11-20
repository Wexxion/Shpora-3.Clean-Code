using FluentAssertions;
using NUnit.Framework;

namespace Markdown.Tests
{
    [TestFixture]
    class Md_ShouldRender
    {
        private Md md;
        [SetUp]
        public void SetUp() => md = new Md();

        [TestCase("test1 _cursive_ test2", ExpectedResult = "test1 <em>cursive</em> test2", 
            TestName = "cursive")]
        [TestCase("test1 __bold__ test2", ExpectedResult = "test1 <strong>bold</strong> test2", 
            TestName = "bold")]
        public string WhenNoNestedTags(string str)
        {
            return md.RenderToHtml(str);
        }


        [TestCase("__1 _cursive_ 2__", ExpectedResult = "<strong>1 <em>cursive</em> 2</strong>",
            TestName = "cursive in bold")]
        [TestCase("_1 __bold__ 2_", ExpectedResult = "<em>1 __bold__ 2</em>", 
            TestName = "bold in cursive")]
        public string WhenNestedTags(string markdown)
        {
            return md.RenderToHtml(markdown);
        }
    }
}
