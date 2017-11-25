using FluentAssertions;
using NUnit.Framework;

namespace Markdown.Tests
{
    [TestFixture]
    internal class Md_ShouldRender
    {
        [SetUp]
        public void SetUp()
        {
            md = new Md();
        }

        private Md md;

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


        [TestCase("test1 \\_not italic\\_ test2", ExpectedResult = "test1 \\_not italic\\_ test2",
            TestName = "Escaped italic")]
        [TestCase("test_12_3 test", ExpectedResult = "test_12_3 test", TestName = "Italic and numbers")]
        public string EscapedTagsProperly(string str)
        {
            return md.RenderToHtml(str);
        }

        [TestCase("a_ _italic_ b_", ExpectedResult = "a_ <em>italic</em> b_", TestName = "open not paired")]
        [TestCase("__a _b c", ExpectedResult = "__a _b c", TestName = "not paired")]
        [TestCase("a_ c_ b", ExpectedResult = "a_ c_ b", TestName = "2 close not paired")]
        [TestCase("_a __b c", ExpectedResult = "_a __b c", TestName = "2 open not paired")]
        [TestCase("_a _a c d_ e", ExpectedResult = "<em>a _a c d</em> e", TestName = "2 open 1 close")]
        [TestCase("_a __b d_ c", ExpectedResult = "<em>a __b d</em> c", TestName = "hard test")]
        [TestCase("_a __b __c d_ c", ExpectedResult = "<em>a __b __c d</em> c", TestName = "hard test v2")]
        public string CorrectlyParsPairedTagsWithoutEnd(string str)
        {
            return md.RenderToHtml(str);
        }

        [Test]
        [Timeout(800)]
        public void RenderToHtml_TimeoutTest()
        {
            const string str = "__1 _cursive_ 2__";
            const string expected = "<strong>1 <em>cursive</em> 2</strong>";
            for (var i = 0; i < 10000; i++)
                md.RenderToHtml(str).Should().BeEquivalentTo(expected);
        }
    }
}