using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Xml.Linq;
using WebApplicationKinoAPI0510;
using WebApplicationKinoAPI0510.Additional;
using WebApplicationKinoAPI0510.Models;

namespace WpfAppKino0410
{
    public class Current
    {
        private static bool isInitialized = false;
        private static bool isInitializing = false;
        private static object initializationLock = new object();

        public static ObservableCollection<Title> Titles { get; set; } = new ObservableCollection<Title>();
        public static ObservableCollection<FaveList> Faves { get; set; } = new ObservableCollection<FaveList>();
        public static ObservableCollection<Genre> Genres { get; set; } = new ObservableCollection<Genre>();
        public static User CurrentUser { get; set; } = new User();
        public static ObservableCollection<Comment> Comments { get; set; } = new ObservableCollection<Comment>();
        public static ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();
        //public Title CTitle { get; set; }

        public static Title CurrentTitle { get; set; } = new Title();

        //Current()
        //{
        //    InitializeAsync().Wait();
        //}

        //Current(int userId)
        //{
        //    InitializeAsync(userId).Wait();
        //}

        public Current()
        {
            //InitializeAsync().Wait();
        }

        public static async Task UpdateDataContextAsync(Window window)
        {
            var current1 = new Current();
            await Current.InitializeAsync(Current.CurrentUser.Id);
            window.DataContext = current1;
        }




        public static async Task EnsureInitializedAsync(int userId = 0)
        {
            if (!isInitialized)
            {
                // Мы еще не начали инициализацию, начнем ее
                if (!isInitializing)
                {
                    lock (initializationLock)
                    {
                        if (!isInitializing)
                        {
                            isInitializing = true;
                        }
                    }

                    if (isInitializing)
                    {
                        await LoadAllAsync(userId);
                        isInitialized = true;

                        lock (initializationLock)
                        {
                            isInitializing = false;
                        }
                    }
                }
                else
                {
                    // Если инициализация уже выполняется в другом потоке, подождем ее завершения
                    while (!isInitialized)
                    {
                        await Task.Delay(100);
                    }
                }
            }
            else
            {
                // Данные уже инициализированы, можно выполнить обновление
                await LoadAllAsync(userId);
            }
        }


        public static async Task InitializeAsync(int userId = 0)
        {
            CommonOperations commonOperations = new CommonOperations();
            if (Current.CurrentUser.Id == 0)
            {
                Current.CurrentUser = await commonOperations.GetByIdAsync<User>(userId);
            }
            else
            {
                Current.CurrentUser = await commonOperations.GetByIdAsync<User>(CurrentUser.Id);
            }
            Current.CurrentTitle = new Title();
            var IETitles = await commonOperations.GetAllAsync<Title>();
            Current.Titles = new ObservableCollection<Title>(IETitles);
            var FavesAll = await commonOperations.GetAllAsync<FaveList>();
            Current.Faves = new ObservableCollection<FaveList>(FavesAll.Where(f => f.UserId == userId));
            var IEGenres = await commonOperations.GetAllAsync<Genre>();
            Current.Genres = new ObservableCollection<Genre>(IEGenres);
            var IEComments = await commonOperations.GetAllAsync<Comment>();
            Current.Comments = new ObservableCollection<Comment>(IEComments);
            var IEUsers = await commonOperations.GetAllAsync<User>();
            Current.Users = new ObservableCollection<User>(IEUsers);
        }





        //    private async Task InitializeAsync(int userId = 0)
        //{
        //    await LoadAllAsync(userId);
        //    // Здесь можно выполнять какие-либо дополнительные действия после загрузки данных
        //}

        public static async Task LoadAllAsync(int userId = 0)
        {


            CommonOperations commonOperations = new CommonOperations();
            if (Current.CurrentUser.Id == 0)
            {
                Current.CurrentUser = await commonOperations.GetByIdAsync<User>(userId);
            }
            else
            {
                Current.CurrentUser = await commonOperations.GetByIdAsync<User>(CurrentUser.Id);
            }
            Current.CurrentTitle = new Title();
            var IETitles = await commonOperations.GetAllAsync<Title>();
            Current.Titles = new ObservableCollection<Title>(IETitles);
            var FavesAll = await commonOperations.GetAllAsync<FaveList>();
            Current.Faves = new ObservableCollection<FaveList>(FavesAll.Where(f => f.UserId == userId));
            var IEGenres = await commonOperations.GetAllAsync<Genre>();
            Current.Genres = new ObservableCollection<Genre>(IEGenres);
            var IEComments = await commonOperations.GetAllAsync<Comment>();
            Current.Comments = new ObservableCollection<Comment>(IEComments);
            var IEUsers = await commonOperations.GetAllAsync<User>();
            Current.Users = new ObservableCollection<User>(IEUsers);
        }

    }

}

//public void CurrentT(int das)
//{
//    Role roleAdmin = new Role() { Id = 1, RoleName="Admin" };
//    Role roleUser = new Role() { Id = 2, RoleName="User" };

//    User user1 = new User()
//    {
//        Id = 1,
//        UserName = "UserUser1",
//        Email = "User@example.com",
//        ImageUrl = "https://www.gravatar.com/avatar/deb25307e4d5bef9582fe8650d65fcaa?s=64&d=identicon&r=PG",
//        Role = roleAdmin,
//        Password = "dsadsa"
//    };

//    Comment comment1 = new Comment()
//    {
//        Id = 1,
//        Date = new DateTime(2011, 11, 8, 16, 8, 2),
//        TextContent="assdasdasdasdasdasdasdasd2011",
//        User=CurrentUser,
//        Title =new Title()
//        {
//            Id = 1,
//            TitleName = "123asdsad",
//            TitleAdditionalName = "sad22222",
//            Description = "dkassf  djosajdoas jdoasjdapsodjaosdjasdjaspdsaodjaspodjaspodjapsdooj",
//            Date = 2003,
//            ImageUrl= "https://www.gravatar.com/avatar/deb25307e4d5bef9582fe8650d65fcaa?s=64&d=identicon&r=PG",
//            Genres = Genres,
//            Comments = Comments
//        }
//    };


//    Comment comment2 = new Comment()
//    {
//        Id = 7,
//        Date = new DateTime(2014, 04, 21, 12, 34, 12),
//        TextContent="assdasdasdasdasdasdasdasd2014",
//        User=CurrentUser,
//        Title =new Title()
//        {
//            Id = 1,
//            TitleName = "123asdsad",
//            TitleAdditionalName = "sad22222",
//            Description = "dkassf  djosajdoas jdoasjdapsodjaosdjasdjaspdsaodjaspodjaspodjapsdooj",
//            Date = 2003,
//            ImageUrl= "https://www.gravatar.com/avatar/deb25307e4d5bef9582fe8650d65fcaa?s=64&d=identicon&r=PG",
//            Genres = Genres,
//            Comments = Comments
//        }
//    };
//    Comment comment3 = new Comment()
//    {
//        Id = 5,
//        Date = new DateTime(2021, 06, 04, 3, 32, 6),
//        TextContent="assdasdasdasdasdasdasdasd2021",
//        User=CurrentUser,
//        Title =new Title()
//        {
//            Id = 1,
//            TitleName = "123asdsad",
//            TitleAdditionalName = "sad22222",
//            Description = "dkassf  djosajdoas jdoasjdapsodjaosdjasdjaspdsaodjaspodjaspodjapsdooj",
//            Date = 2003,
//            ImageUrl= "https://www.gravatar.com/avatar/deb25307e4d5bef9582fe8650d65fcaa?s=64&d=identicon&r=PG",
//            Genres = Genres,
//            Comments = Comments
//        }
//    };
//    Comments.Add(comment1);
//    Comments.Add(comment2);
//    Comments.Add(comment3);


//    Genres = new ObservableCollection<Genre>() { new Genre() { Id = 1, GenreName = "Comedy" }, new Genre() { Id = 1, GenreName = "Action" }, new Genre() { Id = 1, GenreName = "Adventure" }
//    };
//    Title title1 = new Title()
//    {
//        Id = 1,
//        TitleName = "123asdsad",
//        TitleAdditionalName = "sad22222",
//        Description = "dkassf  djosajdoas jdoasjdapsodjaosdjasdjaspdsaodjaspodjaspodjapsdooj",
//        Date = 2003,
//        ImageUrl= "https://www.gravatar.com/avatar/deb25307e4d5bef9582fe8650d65fcaa?s=64&d=identicon&r=PG",
//        Genres = Genres,
//        Comments = Comments
//    };

//    Titles.Add(title1);

//    CurrentUser.Comments = Comments;
//    CurrentTitle.Comments = Comments;

//    Users = new ObservableCollection<User>
//            {
//                new User()
//    {
//        Id = 2,
//                    Email = "UserUserUser2@example.com",
//                    ImageUrl ="https://www.gravatar.com/avatar/deb25307e4d5bef9582fe8650d65fcaa?s=64&d=identicon&r=PG",
//                    Password = "dasd",
//                    Role = roleUser,
//                    UserName = "UserUser2"
//                },
//                new User()
//    {
//        Id = 6,
//                    Email = "UserUserUser6@example.com",
//                    ImageUrl ="https://www.gravatar.com/avatar/deb25307e4d5bef9582fe8650d65fcaa?s=64&d=identicon&r=PG",
//                    Password = "dasd",
//                    Role = roleUser,
//                    UserName = "UserUser6"
//                },
//                CurrentUser,
//                CurrentUser,
//                CurrentUser
//            };
//}
