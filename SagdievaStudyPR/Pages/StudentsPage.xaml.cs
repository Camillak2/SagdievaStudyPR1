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
    /// Логика взаимодействия для StudentsPage.xaml
    /// </summary>
    public partial class StudentsPage : Page
    {
        public static List<Student> students { get; set; }
        public static List<Discipline> disciplines { get; set; }
        public static List<Exam> exams { get; set; }
        Exam contextExam;
        public StudentsPage(Exam exam)
        {
            InitializeComponent();
            contextExam = exam;
            InitializeDataInPage();
            this.DataContext = this;
        }
        private void InitializeDataInPage()
        {
            StudentCB.ItemsSource = DBConnection.sagdievaStudyPREntities.Student.ToList();
            students = new List<Student>(DBConnection.sagdievaStudyPREntities.Student).ToList();
            disciplines = new List<Discipline>(DBConnection.sagdievaStudyPREntities.Discipline.ToList());
            exams = new List<Exam>(DBConnection.sagdievaStudyPREntities.Exam.Where(x => x.Date == contextExam.Date && x.Discipline.ID == contextExam.ID_Discipline).ToList());
        }
        private void StudentAddBTN_Click(object sender, RoutedEventArgs e)
        {
            string mark = "2";
            var TBmark = MarkCB.SelectedValue as TextBlock;
            if (TBmark != null)
                mark = TBmark.Text;
            if (StudentCB.SelectedItem is Student student)
            {
                var exam = contextExam;
                exam.Student = student;
                exam.Mark = int.Parse(mark);
                var studentInList = exams.FirstOrDefault(x => x.ID_Student == student.ID);
                if (studentInList != null)
                {
                    MessageBox.Show("Такой студент уже есть в экзамене");
                    return;
                }
                DBConnection.sagdievaStudyPREntities.Exam.Add(exam);
                DBConnection.sagdievaStudyPREntities.SaveChanges();
                InitializeDataInPage();
            }
        }
        private void StudentDeleteBTN_Click(object sender, RoutedEventArgs e)
        {
            if (StudentLV.SelectedItem is Exam exam)
            {
                DBConnection.sagdievaStudyPREntities.Exam.Remove(exam);
                DBConnection.sagdievaStudyPREntities.SaveChanges();
                InitializeDataInPage();
            }
        }
        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ExamPage());
        }
    }
}
