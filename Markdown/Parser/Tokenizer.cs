using System;
using System.Collections;
using System.Collections.Generic;

namespace Markdown.Parser
{
    public class Tokenizer
    {
        private readonly Node root = new Node();

        public void Add(string word)
        {
            var node = root;
            foreach (var c in word)
                node = node[c] ?? (node[c] = new Node(c, node));
        }

        public IEnumerable<IMatchResult> FindAll(string text)
        {
            if (string.IsNullOrEmpty(text)) throw new ArgumentException();
            var node = root;
            var startIndex = 0;
            var endIndex = 0;
            var mismatch = false;

            for (var i = 0; i < text.Length; i++)
            {
                while (node[text[i]] == null && node != root)
                    node = node.Fail;
                node = node[text[i]] ?? root;

                if (node == root)
                {
                    mismatch = true;
                    endIndex = i + 1;
                    if (i != text.Length - 1)
                        continue;
                }

                if (mismatch)
                {
                    yield return new Mismatch(text.Substring(startIndex, endIndex - startIndex));
                    mismatch = false;
                }
                else if (text.IsCorrectIndex(i + 1) && node[text[i + 1]] == null || i == text.Length - 1)
                {
                    var prevIndex = i - node.Pattern.Length;
                    var nextIndex = i + 1;
                    var prev = text.IsCorrectIndex(prevIndex) ? text[prevIndex] : (char?)null;
                    var next = text.IsCorrectIndex(nextIndex) ? text[nextIndex] : (char?)null;
                    yield return new Match(node.Pattern, prev, next);
                    startIndex = i + 1;
                }
                    
            }
        }

        public void Build()
        {
            var queue = new Queue<Node>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                foreach (var child in node)
                    queue.Enqueue(child);

                node.Fail = FindCurrentFail(node);
                node.Pattern = GetCurrentPattern(node);
            }
        }

        private string GetCurrentPattern(Node current)
        {
            if (current == root)
                return null;
            return current.Parent.Pattern + current.Value;
        }

        private Node FindCurrentFail(Node current)
        {
            if (current == root)
                return root;

            var fail = current.Parent.Fail;

            while (fail[current.Value] == null && fail != root)
                fail = fail.Fail;

            var res = fail[current.Value] ?? root;
            if (res == current) res = root;
            return res;
        }

        private class Node : IEnumerable<Node>
        {
            private readonly Dictionary<char, Node> children = new Dictionary<char, Node>();
            public char Value { get; }
            public Node Parent { get; }
            public Node Fail { get; set; }
            public string Pattern { get; set; }

            public Node()
            {
            }

            public Node(char value, Node parent)
            {
                Value = value;
                Parent = parent;
            }

            public Node this[char c]
            {
                get => children.ContainsKey(c) ? children[c] : null;
                set => children[c] = value;
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            public IEnumerator<Node> GetEnumerator() => children.Values.GetEnumerator();
            public override string ToString() => Pattern;
        }
    }
}