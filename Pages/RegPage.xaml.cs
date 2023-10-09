using Microsoft.IdentityModel.Tokens;
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

namespace WpfAppKino0410.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        Window ParentWindow { get; set; }
        public RegPage(Window parentWindow)
        {

            InitializeComponent();
            ParentWindow = parentWindow;
            LoginIssueTextBlock.Visibility = Visibility.Hidden;
            PasswordRepeatIssueTextBlock.Visibility = Visibility.Hidden;
            PasswordIssueTextBlock.Visibility = Visibility.Hidden;
            EmailIssueTextBlock.Visibility = Visibility.Hidden;
        }

        private async void RegButton_Click(object sender, RoutedEventArgs e)
        {

            LoginIssueTextBlock.Visibility = Visibility.Hidden;
            PasswordRepeatIssueTextBlock.Visibility = Visibility.Hidden;
            PasswordIssueTextBlock.Visibility = Visibility.Hidden;
            EmailIssueTextBlock.Visibility = Visibility.Hidden;

            if ((LoginTextBox.Text.Length >= 4)&&(LoginTextBox.Text.Length <= 16)&&(!LoginTextBox.Text.Contains(' ')))
            {
                if ((!EmailTextBox.Text.Trim().Contains(" "))&&(EmailTextBox.Text.Trim().Contains("@")))
                {
                    if ((PasswordTextBox.Password.Length >= 8)&&(PasswordTextBox.Password.Length <= 40))
                    {
                        CommonOperations db = new CommonOperations();

                        var userList = await db.GetAllAsync<User>();
                        var foundUser = userList.FirstOrDefault(u => u.UserName.ToLower() == LoginTextBox.Text.Trim().ToLower());
                        if (userList.Where(u=> u.Email.IsNullOrEmpty() == false).FirstOrDefault(u => u.Email.ToLower() == EmailTextBox.Text.Trim().ToLower()) == null)
                        {
                            if (userList.Where(u => u.UserName.IsNullOrEmpty() == false).FirstOrDefault(u => u.UserName.ToLower() == LoginTextBox.Text.Trim().ToLower()) == null)
                            {
                                if (PasswordRepeatTextBox.Password == PasswordTextBox.Password)
                                {
                                    if (PasswordRepeatTextBox.Password == PasswordTextBox.Password)
                                    {
                                        try
                                        {
                                            User newUser = new User() { UserName = LoginTextBox.Text.Trim(), Email = EmailTextBox.Text.Trim(), Password = PasswordTextBox.Password, RoleId = 1 };

                                            CommonOperations commonOperations1 = new CommonOperations();
                                            var createdUser = await commonOperations1.AddEntityAsync(newUser);
                                            if (createdUser != null)
                                            {
                                                MessageBox.Show("Регистрация успешна");
                                                Current.cUser = newUser;
                                            }
                                            else
                                            {
                                                throw new Exception();
                                            }
                                            WorkWindow workWindow = new WorkWindow();
                                            workWindow.Show();
                                            ParentWindow.Close();

                                        }
                                        catch
                                        {
                                            MessageBox.Show("Произошла ошибка");
                                        }
                                    }
                                    else
                                    {
                                        PasswordRepeatIssueTextBlock.Visibility= Visibility.Visible;
                                        MessageBox.Show("Пароли не совпадают");
                                    }
                                }
                                else
                                {
                                    PasswordIssueTextBlock.Visibility= Visibility.Visible;
                                    MessageBox.Show("Пароль должен быть не короче 8 символов");
                                }
                            }
                            else
                            {


                                MessageBox.Show("Пользователь с таким именем уже существует");

                            }
                        }
                        else
                        {



                            EmailIssueTextBlock.Visibility= Visibility.Visible;
                            MessageBox.Show("Эта почта занята");
                        }
                    }
                    else
                    {
                        PasswordIssueTextBlock.Visibility= Visibility.Visible;
                        MessageBox.Show("Паоль должен состоять минимум из 8 и максимум из 40 символов");
                    }

                }
                else
                {
                    EmailIssueTextBlock.Visibility= Visibility.Visible;
                    MessageBox.Show("Укажите действительную почту");


                }

            }
            else
            {
                LoginIssueTextBlock.Visibility = Visibility.Visible;
                MessageBox.Show("Логин должен состоять минимум из 4 и максимум из 16 символов. Пробелы не допускаются");
            }
        }
    }
}