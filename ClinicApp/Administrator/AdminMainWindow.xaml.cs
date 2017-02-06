using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace ClinicApp.Administrator
{
    /// <summary>
    /// Interaction logic for AdminMainWindow.xaml
    /// </summary>
    public partial class AdminMainWindow : MetroWindow
    {
        public AdminMainWindow()
        {
            InitializeComponent();
            LoginUserName.Content = $"{MainWindow.FullName}";
        }

        private void Navigate(Object page)
        {
            try
            {
                adminPagesHolder.NavigationService.Navigate(page);
            }
            catch (NullReferenceException exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void btnCreateAccount_Click(object sender, RoutedEventArgs e)
        {
                Navigate(new AdminCreateAccountPage());
        }

        private void btnViewUsers_Click(object sender, RoutedEventArgs e)
        {
            Navigate(new AdminViewUsersPage());
          
        }

        private void btnAddRoles_Click(object sender, RoutedEventArgs e)
        {
            Navigate(new AdminAddRolePage());
        }
    }
}
