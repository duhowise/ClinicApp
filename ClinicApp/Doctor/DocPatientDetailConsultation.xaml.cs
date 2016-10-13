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
        Consultation consultation = new Consultation();

        public DocPatientDetailConsultation()
        {
            InitializeComponent();
        }

        private void Card_Loaded(object sender, RoutedEventArgs e)
        {
            patient = PharSearchPatient.patient;
            PatientName.Content = (patient.FulName()).ToUpper();
            PatientDesignation.Content = $"Designation: {patient.Designation}";
            PatientTemperature.Content = $"Temperature :{consultation.Temperature} °C";
            PatientPulse.Content = $"Pulse :{consultation.Pulse} bpm";
            PatientWeight.Content = $"Weight :{consultation.Weight} kg";
            PatientBloodPressure.Content = $"Blood pressure : {consultation.BloodPressure}";
            PatientLastVisited.Content = $"Consultation Date: {consultation.Date.ToShortDateString()}";

        }
    }
}
