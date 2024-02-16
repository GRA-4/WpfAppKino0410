using System.Collections.Generic;
using System.Windows.Controls;
using WebApplicationKinoAPI0510;

namespace WpfAppKino0410.Pages
{
    /// <summary>
    /// Логика взаимодействия для TitlePage.xaml
    /// </summary>
    public partial class TitlePage : Page
    {
        public Title _selectedTitle = null;
        public IEnumerable<Title> _titles = new List<Title>();

        public TitlePage()
        {
            
            InitializeComponent();
            Load();
            //TitleListView.ItemsSource = _posts;
        }
        public TitlePage(string query)
        {

            InitializeComponent();
            LoadByName(query);
            //TitleListView.ItemsSource = _posts;
        }

        public async void LoadByName(string query)
        {
            try
            {
CommonOperations commonOperations = new CommonOperations();
            _titles = await commonOperations.GetAllByFieldContainsAsync<Title>(t => t.TitleName, query);
            TitleListView.ItemsSource = _titles;
            }
            catch
            {

            }
            
        }
        public async void Load()
        {
            CommonOperations commonOperations = new CommonOperations();
            _titles = await commonOperations.GetAllAsync<Title>();
            TitleListView.ItemsSource = _titles;
        }
        public Title? GiveSelected()
        {
            if (TitleListView.SelectedItem != null)
            {
                _selectedTitle = (Title)TitleListView.SelectedItem;

                return _selectedTitle;
            }
            else { return null; }
        }


    }
}
