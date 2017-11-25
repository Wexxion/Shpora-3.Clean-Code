using System.Collections.Generic;

namespace Markdown.SyntaxTree
{
    class Tree<TNode, TContentType>
    {
        public TNode Value { get; }
        public List<Tree<TNode, TContentType>> Children { get; } = new List<Tree<TNode, TContentType>>();
        public Tree<TNode, TContentType> Parent { get; private set; }
        public TContentType Content { get; }
        public bool IsLeaf => Children.Count == 0;
        public Tree(TNode value) => Value = value;
        public Tree(TContentType content) => Content = content;
        public Tree() { }

        public void AddContent(TContentType content) 
            => AddChild(new Tree<TNode, TContentType>(content));

        public Tree<TNode, TContentType> AddChild(TNode value) 
            => AddChild(new Tree<TNode, TContentType>(value));

        public Tree<TNode, TContentType> AddChild(Tree<TNode, TContentType> value)
        {
            value.Parent = this;
            Children.Add(value);
            return value;
        }

        public List<TNode> Enumerate()
        {
            var values = new List<TNode>();
            Enumerate(values);
            return values;
        }
        private void Enumerate(List<TNode> visitor)
        {
            visitor.Add(Value);
            foreach (var child in Children)
                child.Enumerate(visitor);
        }

        public override string ToString() => Value.ToString();
    }
}
