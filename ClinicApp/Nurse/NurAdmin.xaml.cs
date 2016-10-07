using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ClinicApp.Nurse
{
    /// <summary>
    /// Interaction logic for NurAdmin.xaml
    /// </summary>
    public partial class NurAdmin : MetroWindow
    {
        public NurAdmin()
        {
            InitializeComponent();
        }

        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoginUserName.Content = CurrentUserLoggedInData.FirstName + " " + CurrentUserLoggedInData.LastName;
        }


        private void NewPatient_Click(object sender, RoutedEventArgs e)
        {
            new NurAddPatient().ShowDialog();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
