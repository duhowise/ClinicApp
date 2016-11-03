using System;
using System.ComponentModel;
using System.Windows;
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

        public static bool IsRaised { get; set; } = false;
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
                    PatientId =Convert.ToInt32(patient.Id), 
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
