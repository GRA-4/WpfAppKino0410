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
using WebApplicationKinoAPI0510.Additional;
using WebApplicationKinoAPI0510;
using WpfAppKino0410.Pages;
using System.Windows.Navigation;
using System.Collections;

namespace WpfAppKino0410
{
    /// <summary>
    /// Логика взаимодействия для TitleWindow.xaml
    /// </summary>
    public partial class TitleWindow : Window
    {
        Window Parent;
        Title ThisTitle;

        public TitleWindow()
        {
            InitializeComponent();
        }
        public TitleWindow(Window parent, int titleId)
        {
            Parent = parent;
            InitializeComponent();
            FillAll(titleId);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Current.CurrentTitle = new Title();
            Parent.IsEnabled = true;
        }

        private async void FillAll(int titleId)
        {
            CommonOperations commonOperations = new CommonOperations();
            ThisTitle = await commonOperations.GetByIdAsync<Title>(titleId);
            if (ThisTitle != null)
            {
                var current1 = new Current();
                await Current.InitializeAsync(Current.CurrentUser.Id);
                Current.CurrentTitle = ThisTitle;
                this.DataContext = current1;

                if((ThisTitle.Votes != null)&&(ThisTitle.Votes.Count >0))
                {
                    int rating = 0;
                    foreach (var vote in ThisTitle.Votes)
                    {
                        if (vote.Rating != null)
                        {
                            rating += (int)vote.Rating;
                        }
                        
                    }
                    rating = rating/ThisTitle.Votes.Count;
                    RatingTextBlock.Text = rating.ToString();
                }
            }
        }
        private async void Vote()
        {
            CommonOperations commonOperations = new CommonOperations();
            User voteUser = await commonOperations.GetByIdAsync<User>(Current.CurrentUser.Id);
            Title voteTitle = await commonOperations.GetByIdAsync<Title>(ThisTitle.Id);
            if ((voteUser != null)&&(voteUser != null))
            {
                Vote vote = new Vote() { TitleId = commonOperations.GetByIdAsync<Title>(voteTitle.Id).Result.Id, UserId = commonOperations.GetByIdAsync<User>(voteUser.Id).Result.Id, Rating = ((int)RatingSlider.Value) };
                try
                {
                    var v = await commonOperations.AddEntityAsync<Vote>(vote);
                    if (v == null)
                    {
                        v = await commonOperations.UpdateEntityAsync<Vote>(vote);
                    }
                    await Current.UpdateDataContextAsync(this);
                }
                catch { }

                FillAll(ThisTitle.Id);
            }
        }


        private void VoteButton_Click(object sender, RoutedEventArgs e)
        {
            Vote();
        }

        private void ToCommentsButton_Click(object sender, RoutedEventArgs e)
        {
            CommentPage commentPage = new CommentPage(ThisTitle.Id, this);
            NavigationService navigationService = CommentFrame.NavigationService;
            navigationService.Navigate(commentPage);
        }
    }
}