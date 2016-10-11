using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using ClinicApp.Data;
using ClinicApp.Logic;
using ClinicApp.Pharmacist;
using MahApps.Metro;
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
            LoginUserName.Content=MainWindow.FullName;
            //lbTotalDrugs.Content = a = new DrugRepository().TotalDrugsQuantity("Drugs");
            ////lbRegisteredPatients.Content = p.TotalRegisteredPatient();
            //    b = new DrugRepository().TotalDrugsQuantity("DispensedDrugs");
            // lbAvailableDrugs.Content = a - b;
        }

        private async void Window_Closing(object sender, CancelEventArgs e)
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

        private void btnConsultation_Click(object sender, RoutedEventArgs e)
        {
            new PharSearchPatient().ShowDialog();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            Close();
        }
    }
}
