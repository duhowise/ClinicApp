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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClinicApp.Data;
using ClinicApp.Pharmacist;
using ClinicModel;

namespace ClinicApp.Doctor
{
    /// <summary>
    /// Interaction logic for DocPatientDetailConsultation.xaml
    /// </summary>
    public partial class DocPatientDetailConsultation : UserControl
    {
        static Patient patient = new Patient();
        Consultation _consultation = new Consultation();

        public DocPatientDetailConsultation()
        {
            InitializeComponent();
            patient = PharSearchPatient.patient;
            _consultation = new PatientRepository().PatientHistory(patient);
        }

        private void Card_Loaded(object sender, RoutedEventArgs e)
        {
         
                PatientName.Content = (patient.FulName()).ToUpper();
                PatientDesignation.Content = $"Designation: {patient.Designation}";
                PatientTemperature.Content = $"Temperature :{_consultation?.Temperature} °C";
                PatientPulse.Content = $"Pulse :{_consultation?.Pulse} bpm";
                tbDiagnosis.Text = _consultation?.Diagnosis;
                tbPrescriptions.Text = _consultation?.Prescription;
                tbFindings.Text = _consultation?.Signs;
                PatientWeight.Content = $"Weight :{_consultation?.Weight} kg";
                PatientBloodPressure.Content = $"Blood pressure : {_consultation?.BloodPressure}";
                PatientLastVisited.Content = $"Consultation Date: {_consultation?.Date.ToShortDateString()}";
                PatientHistoryList.ItemsSource = new PatientRepository().AllPatientHistory(patient);
           
        }


        private void PatientHistoryList_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _consultation = null;
            _consultation=PatientHistoryList.SelectedItem as Consultation;
            Card_Loaded(sender,e);
        }

        private void btnDocEdit_Click(object sender, RoutedEventArgs e)
        {
            new DocEditWindow().ShowDialog();
        }
    }
}
