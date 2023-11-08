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
    /// Логика взаимодействия для AddChairPage.xaml
    /// </summary>
    public partial class AddChairPage : Page
    {
        public static List<Chair> chairs { get; set; }
        public static List<Worker> workers { get; set; }
        public static List<Faculty> faculties { get; set; }

        public static Worker loggedUser;
        public static Chair chair = new Chair();
        public AddChairPage()
        {
            InitializeComponent();
            loggedUser = DBConnection.loginedUser;

            faculties = new List<Faculty>(DBConnection.sagdievaStudyPREntities.Faculty.ToList());
            workers = new List<Worker>(DBConnection.sagdievaStudyPREntities.Worker.ToList());
            chairs = DBConnection.sagdievaStudyPREntities.Chair.Where(x => x.ID == DBConnection.loginedUser.ID_Chair).ToList();
            this.DataContext = this;
        }
        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ChairPage());
        }
        private void SaveBTN_Click(object sender, RoutedEventArgs e)
        {
            var dcvd = IDTB.Text + " " + ChairTB.Text;

            if (MessageBox.Show(dcvd, "Проверьте корректность введенных данных", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
            {
                chair.ID = IDTB.Text.Trim();
                chair.Name = ChairTB.Text.Trim();
                
                var a = FacultyCB.SelectedItem as Faculty;
                chair.ID_Faculty = a.ID;

                DBConnection.sagdievaStudyPREntities.Chair.Add(chair);
                DBConnection.sagdievaStudyPREntities.SaveChanges();
            }
        }
    }
}
