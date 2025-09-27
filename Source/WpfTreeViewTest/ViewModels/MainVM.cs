using System.Collections;
using WpfTreeViewTest.Models;

namespace WpfTreeViewTest.ViewModels
{
    internal class MainVM
    {
        // Private variables
        private readonly CompositeNode Model = new() { Name = "Item" };
        private int Count = 0;

        // Properties
        public NodeVM RootNode { get; set; }
        public IEnumerable Root { get { yield return RootNode; } }
        public string Title => $"TreeListBox (N={Count})";

        // Public Methods
        public MainVM()
        {
            RootNode = new NodeVM(Model, null);
            AddRecursive(Model, 3, 2);
        }

        // Private Helpers
        private void AddRecursive(CompositeNode model, int n, int levels)
        {
            for (int i = 0; i < n; i++)
            {
                var childNode = new CompositeNode { Name = model.Name + (char)('A' + i) };
                model.Children.Add(childNode);
                Count++;

                if (levels > 0)
                {
                    AddRecursive(childNode, n, levels - 1);
                }
            }
        }
    }
}
