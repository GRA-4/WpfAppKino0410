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
using WpfAppKino0410.Pages;

namespace WpfAppKino0410
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

        private void GoAuthButton_Click(object sender, RoutedEventArgs e)
        {
            AuthPage authPage = new AuthPage(this);

            // Получите доступ к NavigationService из Frame с именем "Start"
            NavigationService navigationService = Start.NavigationService;

            // Перейдите на страницу AuthPage
            navigationService.Navigate(authPage);
        }

        private void GoRegButton_Click(object sender, RoutedEventArgs e)
        {
            RegPage regPage = new RegPage(this);

            // Получите доступ к NavigationService из Frame с именем "Start"
            NavigationService navigationService = Start.NavigationService;

            // Перейдите на страницу AuthPage
            navigationService.Navigate(regPage);
        }

    }
}
