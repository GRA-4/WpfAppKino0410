using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppKino0410.Pages
{
    /// <summary>
    /// Логика взаимодействия для OperationPage.xaml
    /// </summary>
    public partial class OperationPage : Page
    {
        WorkWindow Parent;
        public OperationPage(WorkWindow parent)
        {
            InitializeComponent();
            Parent = parent;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Parent.AddButton_Click(sender, e);
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Parent.UpdateButton_Click(sender, e);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Parent.DeleteButton_Click(sender, e);
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            Parent.RefreshButton_Click(sender, e);
        }

        private void ReadButton_Click(object sender, RoutedEventArgs e)
        {
            Parent.ReadButton_Click(sender, e);
        }
    }
}
