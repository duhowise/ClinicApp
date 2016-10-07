using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using ClinicApp.Data;
using ClinicApp.Logic;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ClinicApp.Doctor
{
    /// <summary>
    /// Interaction logic for DocAdmin.xaml
    /// </summary>
    public partial class DocAdmin : MetroWindow
    {
      //OldPatient p = new OldPatient();
        public DocAdmin()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int a, b;
            LoginUserName.Content = CurrentUserLoggedInData.FirstName + " " + CurrentUserLoggedInData.LastName;
           
        }

        private async void Window_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            MessageDialogResult result = await this.ShowMessageAsync("Exit Application", "Do You really want to Exit?", MessageDialogStyle.AffirmativeAndNegative);
            if (result == MessageDialogResult.Negative)
            {
                e.Cancel = false;

            }
            else
            {
                CurrentUserLoggedInData.ClearUserData();
                new MainWindow().Show();
                Hide();
            }
        }

        private void btnConsultation_Click(object sender, RoutedEventArgs e)
        {
            new PatientDetailsForm().Show();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            Close();
        }
    }
}
