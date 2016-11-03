using System.Windows;

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


      
    }
}
