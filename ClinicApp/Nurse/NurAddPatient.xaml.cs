using System;
using System.Windows;
using ClinicApp.Data;
using ClinicModel;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
namespace ClinicApp.Nurse
{
    /// <summary>
    /// Interaction logic for NurAddPatient.xaml
    /// </summary>
    public partial class NurAddPatient: MetroWindow
    {
        Patient patient=new Patient();
        public NurAddPatient()
        {
            InitializeComponent();
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PatientFirstName.Text)||string.IsNullOrWhiteSpace(PatientLastName.Text)||
                string.IsNullOrWhiteSpace(PatientDesignation.Text)||string.IsNullOrWhiteSpace(PatientPhoneNumber.Text)||
                string.IsNullOrWhiteSpace(PatientProvidedId.Text)
                )
            {
                await this.ShowMessageAsync("Sorry 'bou that !", "All details are required");
            }
            else
            {
                        patient.FirstName = PatientFirstName.Text;
                        patient.LastName = PatientLastName.Text;
                        patient.Designation = PatientDesignation.Text;
                        patient.PhoneNumber = PatientPhoneNumber.Text;
                        patient.ProvidedId = PatientProvidedId.Text;
                new PatientRepository().AddNewPatient(patient);
               
                await this.ShowMessageAsync("New patient added !", $"{PatientFirstName.Text+" "+PatientLastName.Text}");
                Util.Clear(this);
            }
            //patient=new PatientRepository().GetPatient(PatientFirstName.Text, PatientLastName.Text);
        }
        
        private async void SaveConsultation_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PatientSymptoms.Text)||string.IsNullOrWhiteSpace(PatientSigns.Text)||
                string.IsNullOrWhiteSpace(PatientDiagnosis.Text)||string.IsNullOrWhiteSpace(PatientPrescirption.Text)||
                string.IsNullOrWhiteSpace(PatientPrescirption.Text)
                )
            {

                    await this.ShowMessageAsync(@"Sorry 'bou that !", "All feilds with asterisk(*) are required");
            }
            else
            {
                new PatientRepository().AddNewConsultation(new Consultation
                {
                    BloodPressure = PatientBloodPressure.Text,
                    Signs = PatientSigns.Text,
                    Diagnosis =  PatientDiagnosis.Text,
                    PatientId = Convert.ToInt32(patient.Id),
                    Prescription = PatientPrescirption.Text,
                    Symptoms = PatientSymptoms.Text,
                    Temperature = PatientTemperature.Text
                });
                await this.ShowMessageAsync("Success!", $"Added Consultation for{patient.FirstName} {patient.LastName.ToUpper()}");
                   Util.Clear(this);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var response = MessageBox.Show("Do you really want to close the window?", "Exit",
                 MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (response == MessageBoxResult.Yes)
                Hide();
        }

    }
}
