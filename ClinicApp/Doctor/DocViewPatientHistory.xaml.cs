using System.Windows;
using Telerik.Windows.Controls;

namespace ClinicApp.Doctor
{
    /// <summary>
    /// Interaction logic for DocViewPatientHistory.xaml
    /// </summary>
    public partial class DocViewPatientHistory : Window
    {
        public string ProvidedId { get; set; }
        public DocViewPatientHistory()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //ProvidedId = viewPatientWin.PatientList1[2];
            //Patient patient = new Patient();
            //if(new OldPatient.RetrieveHistory(ProvidedId)!=null)
            //PatientHistory.ItemsSource = patient.RetrieveHistory(ProvidedId);
            //else
            //{
            //    CMB cmb = new CMB {Message = "There is no History for Selected Patient"};
            //    cmb.Show();
            //}
        }


        private void RadGridView_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {

        }

        private void RadGridView_SelectionChanging(object sender, SelectionChangingEventArgs e)
        {

        }

        private void RadGridView_Sorted(object sender, GridViewSortedEventArgs e)
        {

        }

        private void RadGridView_Sorting(object sender, GridViewSortingEventArgs e)
        {

        }
    }
}
