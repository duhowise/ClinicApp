using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ClinicApp.Logic;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ClinicApp.Pharmacist
{
    /// <summary>
    /// Interaction logic for PharAdmin.xaml
    /// </summary>
    public partial class PharAdmin : MetroWindow
    {
       
        
        private MainContentView navigator;



        public PharAdmin()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            this.DataContext = new MainContentView(NavigateToView);
             navigator = new MainContentView(NavigateToView);
            navigator.NavigateTopharAdminDashboardControl();
        }

        private void NavigateToView(UserControl view)
        {
            MainArea.Content = view;
        }

     

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           
            new PharAddDrug().ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int a, b;
            ViewPatient.cMenu = 0;
            LoginUserName.Content =MainWindow.FullName;
            //lbTotalDrugs.Content = a = drug.TotalDrugsInStock("Drugs");
            //lbDispensedDrugs.Content = b = drug.TotalDrugsInStock("PrescribedDrugs");
            //lbAvailableDrugs.Content = a - b;
            //frequentdataGrid.ItemsSource = Drug.LoadFrequentDrugs().DefaultView;
        }


        //private void DrugHistory_Click(object sender, RoutedEventArgs e)
        //{
        //    new PharViewDrugHistory().ShowDialog();
        //}


     

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

    
        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAddDrug_Click(object sender, RoutedEventArgs e)
        {
            new PharAddDrug().ShowDialog();
        }

        private void btnAddSupplier_Click(object sender, RoutedEventArgs e)
        {
            new PharAddSupplier().ShowDialog();
        }

        private void btnViewReport_Click(object sender, RoutedEventArgs e)
        {
            //stockTakingReportOne repotOne = new stockTakingReportOne();
        }

        private void btnDispenseDrug_Click(object sender, RoutedEventArgs e)
        {
            new PharSearchPatient().ShowDialog();
        }

        private void btnViewDrug_Click(object sender, RoutedEventArgs e)
        {
            var viewDrug = new viewDrugWin();
            viewDrug.ShowDialog();
        }
    }
}
