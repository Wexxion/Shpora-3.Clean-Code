using System.Collections.Generic;

namespace Markdown.SyntaxTree
{
    class Tree<TValue>
    {
        public TValue Value { get; }
        public List<Tree<TValue>> Children { get; } = new List<Tree<TValue>>();
        public Tree<TValue> Parent { get; private set; }
        public Tree(TValue value) => Value = value;
        public Tree() { }


        public Tree<TValue> Add(TValue value) => Add(new Tree<TValue>(value));
        public Tree<TValue> Add(Tree<TValue> value)
        {
            value.Parent = this;
            Children.Add(value);
            return value;
        }

        public List<TValue> Enumerate()
        {
            var values = new List<TValue>();
            Enumerate(values);
            return values;
        }
        private void Enumerate(List<TValue> visitor)
        {
            visitor.Add(Value);
            foreach (var child in Children)
                child.Enumerate(visitor);
        }

        public override string ToString() => Value.ToString();
    }
}
