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
using WebApplicationKinoAPI0510;
using WebApplicationKinoAPI0510.Additional;

namespace WpfAppKino0410.Pages
{
    /// <summary>
    /// Логика взаимодействия для CommentPage.xaml
    /// </summary>
    public partial class CommentPage : Page
    {
        Title ThisTitle = new Title();
        public CommentPage(int TitleId, TitleWindow parentTitleWindow)
        {
            ThisTitle.Id = TitleId;
            InitializeComponent();

            LoadCommentsAsync();
        }
        public async Task LoadCommentsAsync()
        {
            CommonOperations commonOperations = new CommonOperations();

            ThisTitle = await commonOperations.GetByIdAsync<Title>(ThisTitle.Id);
            DataContext = ThisTitle;
        }
    }
}
