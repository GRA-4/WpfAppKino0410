using Castle.Components.DictionaryAdapter.Xml;
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
            await Current.UpdateDataContextAsync(this);
            //var current1 = new Current();
            //await Current.InitializeAsync(Current.CurrentUser.Id);
            //this.DataContext = new Current();
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string query = SearchTextBox.Text;
            if(query != string.Empty)
            {
                try
                {
                    ////var current = new Current();
                    await Current.InitializeAsync();
                    //await Current.UpdateDataContextAsync(this);
                    var IETitles = Current.Titles
                        .Where(t =>
                            (t.TitleName != null && t.TitleName.Contains(query)) ||
                            (t.TitleAdditionalName != null && t.TitleAdditionalName.Contains(query))||
                            (t.Genres.Any(g => g.GenreName != null && g.GenreName.Contains(query))));
                    var IEFaves = Current.Faves
                        .Where(f =>
                        (f.Title.TitleName != null
                         && f.Title.TitleName.Contains(query)) ||
                        (f.Title.TitleAdditionalName != null && f.Title.TitleAdditionalName.Contains(query))||
                        (f.Title.Genres.Any(g => g.GenreName != null && g.GenreName.Contains(query)))
                        );

                    var IEUsers = Current.Users
                        .Where(u => 
                        (u.UserName != null && u.UserName.Contains(query) == true)||
                        (u.Role.RoleName != null && u.Role.RoleName.Contains(query) == true));

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
                    var current1 = new Current();
                    //await Current.InitializeAsync(Current.CurrentUser.Id);
                    this.DataContext = current1;
                    //await Current.UpdateDataContextAsync(this);
                }
                catch (Exception ex) { }

            }
            else
            {
                //var current1 = new Current();
                //await Current.InitializeAsync(Current.CurrentUser.Id);
                //this.DataContext = current1;
                await Current.UpdateDataContextAsync(this);
            }

        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
        private async void Refresh()
        {
            SearchTextBox.Clear();
            //var current1 = new Current();
            //await Current.InitializeAsync(Current.CurrentUser.Id);
            //this.DataContext = current1;
            await Current.UpdateDataContextAsync(this);
        }






        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Current.CurrentUser = new User();
            this.Close();
            
        }

        private void TitlesListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Title selectedTitle = TitlesListView.SelectedItem as Title;
            if (selectedTitle != null)
            {
                TitleWindow titleWindow = new TitleWindow(this, selectedTitle.Id);
                titleWindow.Show();
                this.IsEnabled=false;
            }
        }
    }
}