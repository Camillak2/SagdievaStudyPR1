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
using SagdievaStudyPR.DB;

namespace SagdievaStudyPR.Pages
{
    /// <summary>
    /// Логика взаимодействия для ChairPage.xaml
    /// </summary>
    public partial class ChairPage : Page
    {
        public static List<Chair> chairs { get; set; }
        public static List<Faculty> faculties { get; set; }
        public static List<Worker> workers { get; set; }
        public static Worker loggedUser;
        public ChairPage()
        {
            InitializeComponent();
            ChairsLV.ItemsSource = DBConnection.sagdievaStudyPREntities.Chair.Where(x => x.ID == DBConnection.loginedUser.ID_Chair).ToList();
            UserTB.Text = DBConnection.loginedUser.Fullname;
            loggedUser = DBConnection.loginedUser;
            this.DataContext = this;
        }
        private void ChairsLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ChairsLV.SelectedItem is Chair chair)
            {
                ChairsLV.SelectedItem = null;
                NavigationService.Navigate(new DisciplineAddPage(chair));
            }
        }
        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationPage());
        }
        private void AddBTN_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddChairPage());
        }
    }
}
