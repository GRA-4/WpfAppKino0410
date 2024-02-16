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
using System.Windows.Shapes;
using WebApplicationKinoAPI0510;
using WpfAppKino0410.Pages;

namespace WpfAppKino0410
{
    /// <summary>
    /// Логика взаимодействия для WorkWindow.xaml
    /// </summary>
    public partial class WorkWindow : Window
    {
        public Page activePage = null;
        public OperationPage OperationPage = null;
        public TitlePage TitlePage;
        public UserPage UserPage;

        public WorkWindow()
        {
            InitializeComponent();
            MessageBox.Show(Current.cUser.UserName);
            OperationPage = new OperationPage(this);
            OperationFrame.Navigate(OperationPage);
            WorkTabControl.SelectedIndex = 4;


            TitlePage = new TitlePage();
            UserPage = new UserPage();

            TitleFrame.Navigate(TitlePage);
            UserFrame.Navigate(UserPage);

            //Еще фреймы
        }


        private void WorkTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OperationPage.AddButton.IsEnabled = true;
            OperationPage.UpdateButton.IsEnabled = true;
            OperationPage.ReadButton.IsEnabled = true;
            OperationPage.DeleteButton.IsEnabled = true;

            OperationPage.RefreshButton.IsEnabled = true;
            if (Current.cUser.RoleId == 1)
            {
                switch (WorkTabControl.SelectedIndex)
                {
                    case 0:
                        activePage = TitlePage;
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        activePage = UserPage;
                        break;

                        //кейсы еще
                }
            }
            else
            {
                switch (WorkTabControl.SelectedIndex)
                {
                    case 0:
                        activePage = TitlePage;
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        activePage = UserPage;
                        break;

                }
                switch (WorkTabControl.SelectedIndex)
                {
                    //кейсы еще
                }
            }
        }

            public void AddButton_Click(object sender, RoutedEventArgs e)
            {
                if (activePage == TitlePage)
                {
                    TitleDo titleOperation = new TitleDo(this);
                    titleOperation.Show();
                    this.IsEnabled = false;
                }
                //Елсе if
            }

            public void UpdateButton_Click(object sender, RoutedEventArgs e)
            {
                if (activePage == TitlePage)
                {
                    var title = TitlePage.GiveSelected();
                    TitleDo titleOperation = new TitleDo(this, title);
                    titleOperation.Show();
                    this.IsEnabled = false;
                }

                //Елсе if
            }

        public async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (activePage == TitlePage)
            {
                var title = TitlePage.GiveSelected();
                CommonOperations commonOperations = new CommonOperations();

                var toRemove = await commonOperations.GetByIdAsync<Title>(title.Id);
                var k = await commonOperations.RemoveEntityAsync<Title>(toRemove);
                Refresh();

            }
        }

        public void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            Refresh();


            //дальше
        }
        public void Refresh()
        {
            SearchTextBox.Text = string.Empty;

            TitlePage = new TitlePage();
            TitlePage._selectedTitle = null;

            UserPage = new UserPage();

            //Селектед
            TitleFrame.Navigate(TitlePage);

            UserFrame.Navigate(UserPage);

        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (activePage == TitlePage)
            {
                TitlePage = new TitlePage(SearchTextBox.Text.Trim().ToLower());
                TitleFrame.Navigate(TitlePage);
            }
        }

        public void ReadButton_Click(object sender, RoutedEventArgs e)
        {
            if (activePage == TitlePage)
            {
                var title = TitlePage.GiveSelected();
                try
                {
                    var ss = "";
                    foreach (Genre genre in title.Genres)
                    {
                        ss+= genre.GenreName+ "\t";
                    }
                    MessageBox.Show(title.TitleName+"\n"+title.TitleAdditionalName+"\n"+title.Description+"\n" + title.Date+"\n"+ss, title.Id.ToString());
                }
                catch { }


            }

        }
    }
}
