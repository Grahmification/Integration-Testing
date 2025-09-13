using PropertyTools.Wpf;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfTreeViewTest.ViewModels;

namespace WpfTreeViewTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        void tree1_KeyDown(object sender, KeyEventArgs e)
        {
            if (sender is TreeListBox tree)
            {
                switch (e.Key)
                {
                    case Key.Enter:
                        AddItem(tree);
                        break;
                    case Key.Delete:
                        Delete(tree);
                        break;
                }
            }
        }

        private void AddItem(TreeListBox treeView)
        {
            if (treeView.SelectedValue is NodeVM node)
            {
                var child = node.AddChild();
                child.ExpandParents();
                treeView.SelectedItem = child;
                treeView.ScrollIntoView(child);
            }
        }

        private void Delete(TreeListBox treeView)
        {
            var idx = treeView.SelectedIndex;
            var deleteItems = new List<NodeVM>();

            // Get all selections, then delete
            foreach (NodeVM s in treeView.SelectedItems)
                deleteItems.Add(s);

            foreach (var s in deleteItems)
                s.Delete();

            treeView.SelectedIndex = idx < treeView.Items.Count ? idx : idx - 1;
        }
    }
}