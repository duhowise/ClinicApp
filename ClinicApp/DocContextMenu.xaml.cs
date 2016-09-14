using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClinicApp
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
            var pateintComplaint = new DocPatientPrescription();
            pateintComplaint.ProvidedId.Text = viewPatientWin.PatientList1[2];
            pateintComplaint.ShowDialog();
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
            var updatePatient = new DocUpdatePatient();
            updatePatient.ShowDialog();
          
        }
    }
}
