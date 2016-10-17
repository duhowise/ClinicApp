using System.Windows;

namespace ClinicApp.Doctor
{
    /// <summary>
    /// Interaction logic for DocContextMenu.xaml
    /// </summary>
    public partial class DocContextMenu : Window
    {
        public DocContextMenu()
        {
            InitializeComponent();
        }

        private void miCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();

        }

        private void miDiagnose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void miComplaint_Click(object sender, RoutedEventArgs e)
        {   
            Close();
           // var pateintComplaint = new DocPatientPrescription();
            //pateintComplaint.ProvidedId.Text = viewPatientWin.PatientList1[2];
            //pateintComplaint.ShowDialog();
        }

        private void miHistory_Click(object sender, RoutedEventArgs e)
        {
            Close();
            var patientHistory=new DocViewPatientHistory();
            patientHistory.ShowDialog();
        }

        private void miUpdate_Click(object sender, RoutedEventArgs e)
        {
            Close();
            //change to updatePatient later
            //var updatePatient = new DocUpdatePatient();
            //updatePatient.ShowDialog();
          
        }
    }
}
