﻿using System;
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
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        Window ParentWindow { get; set; }
        public AuthPage(Window parentWindow)
        {
            InitializeComponent();
            ParentWindow=parentWindow;
        }
        private int Tryings = 0;
        public Window par;

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LogginIssueTextBlock.Visibility = Visibility.Hidden;
            PasswordIssueTextBlock.Visibility = Visibility.Hidden;
            AuthIssueTextBlock.Visibility = Visibility.Hidden;

            User userToFind = new User() { UserName = LoginTextBox.Text.Trim(), Password =  PasswordTextBox.Password };
            if (Tryings >5)
            {
                MessageBox.Show("Слишком много попыток");
                LoginButton.IsEnabled = false;
            }
            else
            {
                if (userToFind.UserName == null)
                {
                    LogginIssueTextBlock.Visibility = Visibility.Visible;
                }
                else
                {
                    if (userToFind.Password == null)
                    {
                        PasswordIssueTextBlock.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        CommonOperations commonOperations = new CommonOperations();
                        var foundUsers = await commonOperations.GetAllAsync<User>();
                        var foundUser = foundUsers.FirstOrDefault(u => u.UserName.ToLower() == userToFind.UserName.ToLower());
                        if (foundUser == null)
                        {
                            AuthIssueTextBlock.Visibility = Visibility.Visible;
                            Tryings++;
                        }
                        else
                        {

                            if (foundUser.Password == userToFind.Password)
                            {
                                MessageBox.Show("Авторизация успешна");
                                Current.cUser = foundUser;
                                WorkWindowNew workWindow = new WorkWindowNew();
                                workWindow.Show();
                                ParentWindow.Close();
                            }
                            else
                            {
                                PasswordIssueTextBlock.Visibility= Visibility.Visible;
                            }


                        }
                    }
                }
            }
        }
    }
}
