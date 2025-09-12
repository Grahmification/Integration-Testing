namespace WpfTreeViewTest.Models
{
    /// <summary>
    /// A node that supports children
    /// </summary>
    class CompositeNode : Node
    {
        public List<Node> Children { get; private set; } = [];
    }
}
