using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using ClinicApp.Data;
using ClinicModel;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ClinicApp.Nurse
{
    /// <summary>
    /// Interaction logic for NurAddConsultation.xaml
    /// </summary>
    public partial class NurAddConsultation : MetroWindow
    {
        Patient patient=new Patient();
        public NurAddConsultation()
        {
            InitializeComponent();
            patient = NurSearchPatient.Patient;
            Consult.Content = $"Consultation- {patient.FulName().ToUpper()}";
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            var response = MessageBox.Show("Do you really want to close out of this window\n make sure your changes are saved", "Exit",
               MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (response == MessageBoxResult.Yes) { Hide(); } else { e.Cancel = true; }
        }

        private async void SaveConsultation_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(PatientSymptoms.Text) || string.IsNullOrWhiteSpace(PatientSigns.Text) ||
                string.IsNullOrWhiteSpace(PatientDiagnosis.Text) || string.IsNullOrWhiteSpace(PatientPrescirption.Text) ||
                string.IsNullOrWhiteSpace(PatientPrescirption.Text))
            {

                await this.ShowMessageAsync(@"Sorry 'bou that !", "All feilds with asterisk(*) are required");
            }
            else
            {
                new PatientRepository().AddNewConsultation(new Consultation
                {
                    BloodPressure = PatientBloodPressure.Text,
                    Signs = PatientSigns.Text,
                    Diagnosis = PatientDiagnosis.Text,
                    UserId = MainWindow.ID,
                    PatientId = patient.Id,
                    Weight = PatientWeight.Text,
                    Pulse =PatientBloodPulse.Text,
                    Respiration =PatientRespiration.Text,
                    Prescription = PatientPrescirption.Text,
                    Symptoms = PatientSymptoms.Text,
                    Temperature = PatientTemperature.Text
                   });
                await this.ShowMessageAsync("Success!", $"Added Consultation for {patient.FirstName} {patient.LastName.ToUpper()}");
                Util.Clear(this);
            }
        }
    }
}
