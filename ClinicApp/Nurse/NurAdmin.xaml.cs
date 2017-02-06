using System.Windows;
using System.Windows.Controls;
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

        public static bool Patient { get; set; } = false;
        public static bool Consult { get; set; } = false;
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

        private void ExistingPatient_OnClick(object sender, RoutedEventArgs e)
        {
            Consult = true; 
            new NurSearchPatient().ShowDialog();
            Consult = false;
        }

        private void BtnNurUpdatePatient_OnClick(object sender, RoutedEventArgs e)
        {
            Patient = true;
            new NurSearchPatient().ShowDialog();
            Patient = false;

        }
    }
}
