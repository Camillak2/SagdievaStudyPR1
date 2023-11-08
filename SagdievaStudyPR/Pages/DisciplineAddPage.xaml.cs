using SagdievaStudyPR.DB;
using System;
using System.Collections.Generic;
using System.Data.Common;
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

namespace SagdievaStudyPR.Pages
{
    /// <summary>
    /// Логика взаимодействия для DisciplineAddPage.xaml
    /// </summary>
    public partial class DisciplineAddPage : Page
    {
        public static List<Chair> chairs { get; set; }
        public static List<Discipline> disciplines { get; set; }

        Chair contextChair;
        public DisciplineAddPage(Chair chair)
        {
            InitializeComponent();
            contextChair = chair;
            InitializeDataInPage();
            this.DataContext = this;
        }
        private void InitializeDataInPage()
        {
            chairs = new List<Chair>(DBConnection.sagdievaStudyPREntities.Chair.Where(i => i.Name == contextChair.Name).ToList());
            DisciplinesLV.ItemsSource = DBConnection.sagdievaStudyPREntities.Discipline.Where(x => x.ID_Chair == contextChair.ID).ToList();
            ChairTB.Text = Convert.ToString(contextChair.Name);
            disciplines = new List<Discipline>(DBConnection.sagdievaStudyPREntities.Discipline.ToList());
        }
        private void Refresh() //Обновление листа
        {
            DisciplinesLV.ItemsSource = DBConnection.sagdievaStudyPREntities.Discipline.Where(i => i.ID_Chair == contextChair.ID).ToList();
        }
        private void AddBTN_Click(object sender, RoutedEventArgs e)
        {
            Discipline disc = new Discipline();

            if (DisciplinesCB.SelectedItem is Discipline discipline)
            {
                var d = DisciplinesCB.SelectedItem as Discipline;
                disc.Name = d.Name;
                disc.Volume = Convert.ToInt32(VolumeTB.Text);
                disc.ID_Chair = contextChair.ID;

                DBConnection.sagdievaStudyPREntities.Discipline.Add(disc);
                DBConnection.sagdievaStudyPREntities.SaveChanges();
                Refresh();
                InitializeDataInPage();
                //var error = string.Empty;
                //var validationContext = new ValidationContext(contextChair);
                //var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
                //var disciplina = DBConnection.practise320_KudashovaAnnaEntities.Discipline.FirstOrDefault(x => x.ID_Chair == contextChair.ID);
                ////if (disciplina != null && disciplina != contextChair)
                ////    error += "Такое уже есть";
                //if (Validator.TryValidateObject(contextChair, validationContext, results, true))
                //{
                //    foreach (var result in results)
                //    {
                //        error += $"{result.ErrorMessage}\n";
                //    }
                //}
                //if (!string.IsNullOrWhiteSpace(error))
                //{
                //    MessageBox.Show(error);
                //    return;
                //}
                //if (discipline == null)
                //    DBConnection.practise320_KudashovaAnnaEntities.Discipline.Add(discipline);
                //DBConnection.practise320_KudashovaAnnaEntities.SaveChanges();
            }
        }
        private void DeleteBTN_Click(object sender, RoutedEventArgs e)
        {
            if (DisciplinesLV.SelectedItem is Discipline discipline)
            {
                DBConnection.sagdievaStudyPREntities.Discipline.Remove(discipline);
                DBConnection.sagdievaStudyPREntities.SaveChanges();
                InitializeDataInPage();
                Refresh();
            }
        }
        private void ChangeBTN_Click(object sender, RoutedEventArgs e)
        {
            if (DisciplinesLV.SelectedItem is Discipline discipline)
            {
                DisciplinesLV.SelectedItem = null;
                NavigationService.Navigate(new EditDisciplinePage(discipline));
            }
        }
        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ChairPage());
        }
    }
}
