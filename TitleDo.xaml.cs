using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WebApplicationKinoAPI0510;

namespace WpfAppKino0410
{
    /// <summary>
    /// Логика взаимодействия для TitleDo.xaml
    /// </summary>
    public partial class TitleDo : Window
    {
       
        User User = Current.cUser;
        WorkWindow Parent;


        Title NewTitle = new Title();
        Title oldTitle;


        IEnumerable<Genre> thisGenres = new List<Genre>();


        string ImagePath = "https://brilliant24.ru/files/cat/template_01.png";

        public TitleDo(WorkWindow parent)
        {
            //esli dobavlenie
            InitializeComponent();



            Parent = parent;

            DateSlider.Minimum = 1900;
            DateSlider.Maximum = DateTime.Now.Year;
            LoadGenres();
        }

        public TitleDo(WorkWindow parent, Title title)
        {
            //eslo obnovlenie
            InitializeComponent();
            Parent = parent;

            DateSlider.Minimum = 1900;
            DateSlider.Maximum = DateTime.Now.Year;

            //raskidat polya po elementam
            NewTitle = title;
            oldTitle = title;
            DataContext = NewTitle;
            LoadGenres();


            TitleToFields();

        }

        public TitleDo(WorkWindow parent, Title title, int e)
        {
            //eslo obnovlenie
            InitializeComponent();
            Parent = parent;

            ViewStackPanel.IsEnabled=false;

            DateSlider.Minimum = 1900;
            DateSlider.Maximum = DateTime.Now.Year;

            //raskidat polya po elementam
            NewTitle = title;
            DataContext = NewTitle;
            LoadGenres();

            TitleToFields();

        }


        public void TitleToFields()
        {
            if (NewTitle != null)
            {
                GenresTextBox.Text = string.Empty;
                foreach (Genre n in NewTitle.Genres)
                {
                    GenresTextBox.Text += n.GenreName + "  ";
                }
                DescriptionTextBox.Text = NewTitle.Description;
                NameTitleTextBox.Text = NewTitle.TitleName;
                DateSlider.Value = (double)NewTitle.Date;
                ImageUrlTextBox.Text = NewTitle.ImageUrl;
                ImageUrlImage.DataContext = NewTitle;
            }
            else
            {
                GenresTextBox.Text = string.Empty;
                DescriptionTextBox.Text = string.Empty;
                NameTitleTextBox.Text = string.Empty;
                DateSlider.Value = DateSlider.Minimum;
                ImageUrlTextBox.Text = string.Empty;
                ImageUrlImage.DataContext = string.Empty;
            }
        }
        public async void LoadGenres()
        {
            CommonOperations  commonOperations = new CommonOperations();
           
                GenresComboBox.ItemsSource = await commonOperations.GetAllAsync<Genre>();
            

        }
        

        private void Window_Closed(object sender, EventArgs e)
        {
            Parent.IsEnabled = true;
        }

        private void ImageUrlTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            NewTitle.ImageUrl = ImageUrlTextBox.Text;
            ImageUrlImage.DataContext = NewTitle;
        }

        private async void AddGenre_Click(object sender, RoutedEventArgs e)
        {
            CommonOperations commonOperations= new CommonOperations();
            var allGenres = await commonOperations.GetAllAsync<Genre>();
            var ge = (Genre)GenresComboBox.SelectedItem;
            if(GenresComboBox.SelectedItem != null)
            {
                if ((NewTitle.Genres.FirstOrDefault(g => g.Id == ge.Id)  == null))
                {
                    NewTitle.Genres.Add(allGenres.FirstOrDefault(g => g.Id == ge.Id));
                    GenresTextBox.Text = null;
                    foreach (Genre n in NewTitle.Genres)
                    {
                        GenresTextBox.Text += n.GenreName + "  ";
                    }
                }
            }
        }

        private void RemoveGenre_Click(object sender, RoutedEventArgs e)
        {
            if (NewTitle.Genres.Count > 0)
            {
                NewTitle.Genres.Remove(NewTitle.Genres.Last());
                GenresTextBox.Text = null;
                foreach (Genre n in NewTitle.Genres)
                {
                    GenresTextBox.Text += n.GenreName + "  ";
                }
            }
        }

        private  async void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            NewTitle.TitleName = NameTitleTextBox.Text;
            NewTitle.ImageUrl = ImageUrlTextBox.Text;
            NewTitle.Description = DescriptionTextBox.Text;

            NewTitle.Date = Convert.ToInt32(DateSlider.Value);
            
            if ((NewTitle.TitleName != string.Empty))
            {
                if (oldTitle != null)
                {
                    CommonOperations commonOperations = new CommonOperations();
                    var l = await commonOperations.UpdateEntityAsync<Title>(NewTitle);
                    Parent.IsEnabled = true;
                    this.Close();
                }
                else
                {
                    CreateTitle();
                }


                //MessageBox.Show(NewTitle.PostId.ToString() + "\t", NewTitle.NameTitle.ToString() + "\t" + NewTitle.Date.ToString());
            }
            else
            {
                MessageBox.Show("Введите название");
            }
        }

        private async void CreateTitle()
        {
            CommonOperations commonOperations = new CommonOperations();
            var allGenres = await commonOperations.GetAllAsync<Genre>();

            var newList = new List<Genre>();
            foreach (var genre in NewTitle.Genres)
            {
                newList.Add(allGenres.FirstOrDefault(g => g.Id == genre.Id));
            }
            NewTitle.Genres = newList;
            var createdTitle = await commonOperations.AddEntityAsync<Title>(NewTitle);

            Parent.IsEnabled = true;
            this.Close();

            ////kinoOperations
            //// db.Titles.Add(NewTitle);
            ////db.SaveChanges();

            //var k = await kinoOperations.CreateTitleAsync(kinoOperations._context, NewTitle);
            //MessageBox.Show(k.NameTitle + " создан");

        }


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Window_Closed(sender, e);
            this.Close();

        }
    }
}
