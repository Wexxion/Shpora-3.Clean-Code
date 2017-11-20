using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using FluentAssertions;
using Markdown.Parser;

namespace Markdown.Tests
{
    [TestFixture()]
    class Tokenizer_Should
    {
        private Tokenizer tokenizer;

        [SetUp]
        public void SetUp()
        {
            tokenizer = new Tokenizer();
            tokenizer.Add("_");
            tokenizer.Add("__");
            tokenizer.Build();
        }

        [TestCase("test", 1, ExpectedResult = new [] {"test"})]
        [TestCase("test1 test2", 1, ExpectedResult = new[] { "test1 test2" })]
        [TestCase("test _italic_", 4, ExpectedResult = new[] { "test ", "_", "italic", "_" })]
        [TestCase("__bold__", 3, ExpectedResult = new[] { "__", "bold", "__" })]
        [TestCase("__bold__ test", 4, ExpectedResult = new[] { "__", "bold", "__", " test" })]
        [TestCase("__a_b_c__", 7, ExpectedResult = new[] { "__", "a", "_", "b", "_", "c", "__" })]
        
        public IEnumerable<string> CorrectlyReturnTokens_OnSimpleTests(string md, int count)
        {
            var res = tokenizer.FindAll(md).ToArray();
            res.Should().HaveCount(count);
            return res.Select(x => x.Data);
        }
    }
}
