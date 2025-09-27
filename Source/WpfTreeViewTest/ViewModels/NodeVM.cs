using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using WpfTreeViewTest.Models;

namespace WpfTreeViewTest.ViewModels
{
    partial class NodeVM : ObservableObject
    {
        // Private variables 
        private readonly Node node = new();
        private ObservableCollection<NodeVM>? children = null;

        // Public properties
        public NodeVM? Parent { get; private set; } = null;
        public ObservableCollection<NodeVM>? Children { get { LoadChildren(); return children; } }

        public string Name 
        { 
            get => node.Name; 
            set => SetProperty(node.Name, value, node, (n, x) => n.Name = x);
        }

        [ObservableProperty]
        private bool isExpanded = false;

        [ObservableProperty]
        private bool isSelected = false;

        // Public Methods
        public NodeVM(Node Node, NodeVM? parent)
        {
            node = Node;
            Parent = parent;
            IsExpanded = true;
        }

        public NodeVM AddChild()
        {
            // Add the child to the model
            var newChild = new CompositeNode() { Name = "New node" };
            ((CompositeNode)node).Children.Add(newChild);
            
            // Add the child to the VM
            var vm = new NodeVM(newChild, this);
            Children?.Add(vm);

            return vm;
        }

        /// <summary>
        /// Deletes this node from the tree
        /// </summary>
        public void Delete()
        {
            Parent?.Children?.Remove(this);
            // NOTE: This doesn't delete from the model. Need to fix.
        }


        /// <summary>
        /// Iteratively expand all parents up to the root node, useful when adding a new node
        /// </summary>
        public void ExpandParents()
        {
            if (Parent != null)
            {
                Parent.ExpandParents();
                Parent.IsExpanded = true;
            }
        }

        // Private helpers
        private void LoadChildren()
        {
            if (children == null)
            {
                children = new ObservableCollection<NodeVM>();
                var cc = node as CompositeNode;
                if (cc != null)
                {
                    foreach (var child in cc.Children)
                    {
                        // Debug.WriteLine("Creating VM for " + child.Name);
                        children.Add(new NodeVM(child, this));
                        // Thread.Sleep(1);
                    }
                }
            }
        }
    }
}
