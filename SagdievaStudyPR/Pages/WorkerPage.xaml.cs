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
    /// Логика взаимодействия для WorkerPage.xaml
    /// </summary>
    public partial class WorkerPage : Page
    {
        public static List<Worker> workers { get; set; }
        public static Worker loggedUser;
        public WorkerPage()
        {
            InitializeComponent();
            UserTB.Text = DBConnection.loginedUser.Fullname;
            workers = DBConnection.sagdievaStudyPREntities.Worker.ToList();
            loggedUser = DBConnection.loginedUser;
            this.DataContext = this;
            Refresh();
        }
        private void Refresh()
        {
            WorkersLV.ItemsSource = DBConnection.sagdievaStudyPREntities.Worker.ToList();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationPage());
        }
        private void AddWorkerBTN_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new WorkerAddPage());
        }

        private void DeleteWorkerBTN_Click(object sender, RoutedEventArgs e)
        {
            if (WorkersLV.SelectedItem is Worker work)
            {
                DBConnection.sagdievaStudyPREntities.Worker.Remove(work);
                DBConnection.sagdievaStudyPREntities.SaveChanges();
            }
            Refresh();
        }
        private void EditWorkerBTN_Click(object sender, RoutedEventArgs e)
        {
            if (WorkersLV.SelectedItem is Worker work)
            {
                WorkersLV.SelectedItem = null;
                NavigationService.Navigate(new WorkerEditPage(work));
            }
        }
    }
}