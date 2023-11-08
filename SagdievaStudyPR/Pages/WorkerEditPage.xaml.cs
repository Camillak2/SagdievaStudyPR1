using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
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
using Microsoft.Win32;
using SagdievaStudyPR.DB;

namespace SagdievaStudyPR.Pages
{
    /// <summary>
    /// Логика взаимодействия для WorkerEditPage.xaml
    /// </summary>
    public partial class WorkerEditPage : Page
    {
        public static List<Worker> workers { get; set; }
        public static List<Chair> chairs { get; set; }
        public static Worker work {get; set;}
        Worker contextWorker;

        public WorkerEditPage(Worker worker)
        {
            InitializeComponent();
            contextWorker = worker;
            workers = DBConnection.sagdievaStudyPREntities.Worker.ToList();
            chairs = DBConnection.sagdievaStudyPREntities.Chair.ToList();
            this.DataContext = this;
            work = worker;
            ChairCB.ItemsSource = DBConnection.sagdievaStudyPREntities.Chair.ToList();
        }
        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new WorkerPage());
        }
        private void AddPhotoBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "*.png|*.png|*.jpeg|*.jpeg|*.jpg|*.jpg"
            };
            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {
                work.Image = File.ReadAllBytes(openFileDialog.FileName);
                ImageWorker.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
            DBConnection.sagdievaStudyPREntities.SaveChanges();
        }
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var error = string.Empty;
            var validationContext = new ValidationContext(contextWorker);
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            if (!Validator.TryValidateObject(contextWorker, validationContext, results, true))
            {
                foreach (var result in results)
                {
                    error += $"{result.ErrorMessage}\n";
                }
            }
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error);
                return;
            }
            if (contextWorker.ID == 0)
                DBConnection.sagdievaStudyPREntities.Worker.Add(contextWorker);
            DBConnection.sagdievaStudyPREntities.SaveChanges();
            NavigationService.Navigate(new WorkerPage());
        }
        
    }
}