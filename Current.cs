using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using WebApplicationKinoAPI0510;
using WebApplicationKinoAPI0510.Additional;
using WebApplicationKinoAPI0510.Models;

namespace WpfAppKino0410
{
    public class Current
    {
        public static ObservableCollection<Title> Titles { get; set; } = new ObservableCollection<Title>();
        public static ObservableCollection<FaveList> Faves { get; set; } = new ObservableCollection<FaveList>();
        public static ObservableCollection<Genre> Genres { get; set; } = new ObservableCollection<Genre>();
        public static User CurrentUser { get; set; } = new User();
        public static ObservableCollection<Comment> Comments { get; set; } = new ObservableCollection<Comment>();
        public static ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();

        public static Title CurrentTitle { get; set; } = new Title();

        public Current()
        {
            var s = LoadAllAsync();
        }


        public static async Task LoadAllAsync(int userId = 1)
        {
            CommonOperations commonOperations = new CommonOperations();
            CurrentUser = await commonOperations.GetByIdAsync<User>(userId);
            CurrentTitle = new Title();
            var IETitles = await commonOperations.GetAllAsync<Title>();
            Titles = new ObservableCollection<Title>(IETitles);
            var FavesAll = await commonOperations.GetAllAsync<FaveList>();
            Faves = new ObservableCollection<FaveList>(FavesAll.Where(f => f.UserId == userId));
            var IEGenres = await commonOperations.GetAllAsync<Genre>();
            Genres = new ObservableCollection<Genre>(IEGenres);
            var IEComments = await commonOperations.GetAllAsync<Comment>();
            Comments = new ObservableCollection<Comment>(IEComments);
            var IEUsers = await commonOperations.GetAllAsync<User>();
            Users = new ObservableCollection<User>(IEUsers);
        }

        public static User cUser { get; set; }
        public static Page cPage { get; set; }
    }
}
