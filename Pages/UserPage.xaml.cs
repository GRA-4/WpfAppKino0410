using System.Windows.Controls;

namespace WpfAppKino0410.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        public UserPage()
        {
            InitializeComponent();
            UserNameTextBlock.Text = Current.cUser.UserName;
            //UserRoleTextBlock.Text = Current.cUser.Role.RoleName;
            UserEmailTextBlock.Text = Current.cUser.Email;
        }

    }
}
