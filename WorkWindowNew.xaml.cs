using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using WebApplicationKinoAPI0510;
using WebApplicationKinoAPI0510.Additional;
using WebApplicationKinoAPI0510.Models;
using WpfAppKino0410.Pages;

namespace WpfAppKino0410
{
    /// <summary>
    /// Логика взаимодействия для WorkWindowNew.xaml
    /// </summary>
    public partial class WorkWindowNew : Window
    {
        public WorkWindowNew()
        {
            InitializeComponent();
            Refresh();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
        private void Refresh()
        {
            this.DataContext = new Current();
        }
    }
}