using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    /// Логика взаимодействия для EditDisciplinePage.xaml
    /// </summary>
    public partial class EditDisciplinePage : Page
    {
        public static List<Discipline> disciplines { get; set; }
        public static List<Chair> chairs { get; set; }
        public static Discipline disc { get; set; }
        Discipline contextdisc;
        public static Worker loggedUser;
        public EditDisciplinePage(Discipline discipline)
        {
            InitializeComponent();
            loggedUser = DBConnection.loginedUser;
            contextdisc = discipline;
            disc = discipline;
            DisciplineCB.ItemsSource = DBConnection.sagdievaStudyPREntities.Discipline.ToList();
            disciplines = new List<Discipline>(DBConnection.sagdievaStudyPREntities.Discipline.ToList());
            this.DataContext = this;
        }
        private void SaveBTN_Click(object sender, RoutedEventArgs e)
        {
            var error = string.Empty;
            var validationContext = new ValidationContext(contextdisc);
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

            if (Validator.TryValidateObject(contextdisc, validationContext, results, true))
            {
                foreach (var result in results)
                {
                    error += $"{result.ErrorMessage}\n";
                }
            }
            if (!string.IsNullOrWhiteSpace(error))
            {
                MessageBox.Show(error);
                return;
            }
            if (contextdisc.ID == 0)
                DBConnection.sagdievaStudyPREntities.Discipline.Add(contextdisc);
            DBConnection.sagdievaStudyPREntities.SaveChanges();
            NavigationService.GoBack();
        }
        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}