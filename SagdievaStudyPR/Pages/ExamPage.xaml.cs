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
    /// Логика взаимодействия для ExamPage.xaml
    /// </summary>
    public partial class ExamPage : Page
    {
        public static List<Exam> exams { get; set; }
        public static List<Discipline> disciplines { get; set; }
        public static List<Worker> workers { get; set; }
        public static Worker loggedUser;
        public ExamPage()
        {
            InitializeComponent();
            UserTB.Text = DBConnection.loginedUser.Fullname;
            exams = DBConnection.sagdievaStudyPREntities.Exam.Where(x => x.ID_Worker == DBConnection.loginedUser.ID).ToList();
            disciplines = DBConnection.sagdievaStudyPREntities.Discipline.ToList();
            workers = DBConnection.sagdievaStudyPREntities.Worker.ToList();
            loggedUser = DBConnection.loginedUser;
            this.DataContext = this;
        }
        private void Refresh()
        {
            ExamsLV.ItemsSource = exams;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
        private void ExamsLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ExamsLV.SelectedItem is Exam exam)
            {
                ExamsLV.SelectedItem = null;
                NavigationService.Navigate(new StudentsPage(exam));
            }
        }
        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationPage());
        }
    }
}
