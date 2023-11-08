using Microsoft.Win32;
using SagdievaStudyPR.DB;
using System;
using System.Collections.Generic;
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

namespace SagdievaStudyPR.Pages
{
    /// <summary>
    /// Логика взаимодействия для WorkerAddPage.xaml
    /// </summary>
    public partial class WorkerAddPage : Page
    {
        public static List<Worker> workers { get; set; }
        public static List<Chair> chairs { get; set; }
        public static Worker worker = new Worker();
        public WorkerAddPage()
        {
            InitializeComponent();
            workers = DBConnection.sagdievaStudyPREntities.Worker.ToList();
            chairs = DBConnection.sagdievaStudyPREntities.Chair.ToList();
            this.DataContext = this;
        }
        private void AddPhotoBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "*.png|*.png|*.jpeg|*.jpeg|*.jpg|*.jpg"
            };
            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {
                worker.Image = File.ReadAllBytes(openFileDialog.FileName);
                ImageWorker.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }
        private void AddWorkerBtn_Click(object sender, RoutedEventArgs e)
        {
            var dcvd = FullnameTB.Text + " " + PositionTB.Text;

            if (MessageBox.Show(dcvd, "Проверьте корректность введенных данных", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
            {
                worker.Fullname = FullnameTB.Text.Trim();
                worker.Position = PositionTB.Text.Trim();
                worker.Salary = Decimal.Parse(SalaryTB.Text.Trim());
                var a = ChairCB.SelectedItem as Chair;
                worker.ID_Chair = a.ID;

                DBConnection.sagdievaStudyPREntities.Worker.Add(worker);
                DBConnection.sagdievaStudyPREntities.SaveChanges();
                NavigationService.Navigate(new WorkerPage());
            }
        }
        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new WorkerPage());
        }
    }
}