﻿using SagdievaStudyPR.DB;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Policy;
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
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public static List<Worker> workers { get; set; }
        public AuthorizationPage()
        {
            InitializeComponent();
        }
        private void EnterBTN_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTB.Text.Trim();
            string password = PasswordTB.Password.Trim();

            workers = new List<Worker>(DBConnection.sagdievaStudyPREntities.Worker.ToList());
            var currentUser = workers.FirstOrDefault(i => i.Login == login && i.Password == password);
            DBConnection.loginedUser = currentUser;
            if (currentUser != null)
            {
                if (currentUser.Position == "преподаватель")
                    NavigationService.Navigate(new ExamPage());
                else if (currentUser.Position == "зав. кафедрой")
                    NavigationService.Navigate(new ChairPage());
                else if (currentUser.Position == "инженер")
                    NavigationService.Navigate(new WorkerPage());
            }
            else
                MessageBox.Show("Неверный логин или пароль. Попробуйте снова.");
        }

        private void GuestBTN_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DisciplinePage());
        }

        private void QRCodeBTN_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new QRCodePage());
        }
    }
}