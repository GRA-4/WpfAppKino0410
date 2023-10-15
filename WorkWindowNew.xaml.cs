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
            Loaded += OnWindowLoaded;
        }

        private async void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            await Current.LoadAllAsync();
            this.DataContext = new Current();
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string query = SearchTextBox.Text;
            if(query != string.Empty)
            {
                try
                {
                new Current();
                    var IETitles = Current.Titles
                        .Where(t =>
                            (t.TitleName != null && t.TitleName.Contains(query)) ||
                            (t.TitleAdditionalName != null && t.TitleAdditionalName.Contains(query))
                        );
                    var IEFaves = Current.Faves
                        .Where(t =>
                        (t.Title.TitleName != null && t.Title.TitleName.Contains(query)) ||
                        (t.Title.TitleAdditionalName != null && t.Title.TitleAdditionalName.Contains(query))
                        );

                    var IEUsers = Current.Users
                        .Where(u => u.UserName != null && u.UserName.Contains(query) == true);

                    var IEComments = Current.Comments
                        .Where(c =>
                            (c.TextContent != null && c.TextContent.Contains(query)) ||
                            (c.User.UserName != null && c.User.UserName.Contains(query)) ||
                            (c.Title.TitleName != null && c.Title.TitleName.Contains(query)) ||
                            (c.Title.TitleAdditionalName != null && c.Title.TitleAdditionalName.Contains(query))
                        );
                    
                Current.Titles = new ObservableCollection<Title>(IETitles);
                Current.Faves = new ObservableCollection<FaveList>(IEFaves);
                Current.Users = new ObservableCollection<User>(IEUsers);
                Current.Comments = new ObservableCollection<Comment>(IEComments);
                this.DataContext = new Current();
                }
                catch (Exception ex) { }

            }
            else
            {
                this.DataContext = new Current();
            }

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