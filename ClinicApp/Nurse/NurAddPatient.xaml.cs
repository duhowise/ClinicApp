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
            PatientDesignation.Items.Add("Vice President");
            PatientDesignation.Items.Add("Web Developer");
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
                new PatientRepository().AddNewPatient(new Patient
                {
                    FirstName = PatientFirstName.Text,
                    LastName = PatientLastName.Text,
                    Designation = PatientDesignation.Text,
                    PhoneNumber = PatientPhoneNumber.Text,
                    ProvidedId = PatientProvidedId.Text
                });
                await this.ShowMessageAsync("New patient added !", $"{PatientFirstName.Text+" "+PatientLastName.Text}");
                Util.Clear(this);
            }
            patient=new PatientRepository().GetPatient(PatientFirstName.Text, PatientLastName.Text);
        }
        
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PatientTemperature.Text)||string.IsNullOrWhiteSpace(PatientBloodPressure.Text)||
                string.IsNullOrWhiteSpace(PatientSymptoms.Text)||string.IsNullOrWhiteSpace(PatientComplaint.Text)||
                string.IsNullOrWhiteSpace(PatientDiagnosis.Text)||string.IsNullOrWhiteSpace(PatientPrescirption.Text)||
                string.IsNullOrWhiteSpace(PatientPrescirption.Text)
                ){

                await this.ShowMessageAsync("Sorry 'bou that !", "All details are required");

                //    PatientTemperature
                //     PatientBloodPressure
                //PatientComplaint
                //PatientSymptoms
                //PatientDiagnosis
                //PatientPrescirption
            }
            else
            {
                new PatientRepository().AddNewConsultation(new Consultation
                {
                    BloodPressure = PatientBloodPressure.Text,
                    Complaint = PatientComplaint.Text,
                    Diagnosis =  PatientDiagnosis.Text,
                    PatientId = patient.Id,
                    Prescription = PatientPrescirption.Text,
                    Symptoms = PatientSymptoms.Text,
                    Temperature = PatientTemperature.Text
                });
                await this.ShowMessageAsync("Success!", $"Added Consultation for{patient.FirstName} {patient.LastName.ToUpper()}");
                   Util.Clear(this);
            }
        }
    }
}
