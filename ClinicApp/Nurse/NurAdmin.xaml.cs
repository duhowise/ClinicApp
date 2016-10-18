using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ClinicApp.Logic;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ClinicApp.Nurse
{
    /// <summary>
    /// Interaction logic for NurAdmin.xaml
    /// </summary>
    public partial class NurAdmin : MetroWindow
    {
        private MainContentView navigator;
        public NurAdmin()
        {
            InitializeComponent();
        }

        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            MessageDialogResult result = await this.ShowMessageAsync("Log Out", "This action will end the current session \n Do you wish to proceed ?", MessageDialogStyle.AffirmativeAndNegative);
            if (result == MessageDialogResult.Negative)
            {
                e.Cancel = false;

            }
            else
            {
                new MainWindow().Show();
                Hide();
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoginUserName.Content = MainWindow.FullName;
            navigator = new MainContentView(NavigateToView);
            navigator.NavigateToNurAdminDashboardControl();
        }

        private void NavigateToView(UserControl view)
        {
            MainArea.Content = view;
        }


        private void NewPatient_Click(object sender, RoutedEventArgs e)
        {
            new NurAddPatient().ShowDialog();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btNurConsulation_Click(object sender, RoutedEventArgs e)
        {
            //load patient list here for consultation..
        }

        private void btnNurUpdatePatient_Click(object sender, RoutedEventArgs e)
        {
            //load patient list here for update patient.. 
        }
    }
}
