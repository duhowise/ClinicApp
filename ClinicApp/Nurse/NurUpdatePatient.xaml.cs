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
    /// Interaction logic for NurUpdatePatient.xaml
    /// </summary>
    public partial class NurUpdatePatient : MetroWindow
    {
        public NurUpdatePatient()
        {
            InitializeComponent();
            PatientDesignation.Items.Add("Staff");
            PatientDesignation.Items.Add("Student");
            PatientDesignation.Items.Add("Dependant");
        }

        public NurUpdatePatient(Patient updatepatient)
        {
            InitializeComponent();
            //todo update patient
            PatientFirstName.Text = updatepatient.FirstName;
            PatientLastName.Text = updatepatient.LastName;
            PatientDesignation.Text = updatepatient.Designation;
            PatientPhoneNumber.Text = updatepatient.PhoneNumber;
            PatientProvidedId.Text = updatepatient.ProvidedId;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            var response = MessageBox.Show("Do you really want to close out of this window\n make sure your changes are saved", "Exit",
               MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (response == MessageBoxResult.Yes) { Hide(); } else { e.Cancel = true; }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private  async void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PatientFirstName.Text) || string.IsNullOrWhiteSpace(PatientLastName.Text) ||
                string.IsNullOrWhiteSpace(PatientDesignation.Text) || string.IsNullOrWhiteSpace(PatientPhoneNumber.Text) ||
                string.IsNullOrWhiteSpace(PatientProvidedId.Text)
                )
            {
                await this.ShowMessageAsync("Sorry 'bou that !", "All details are required");
            }
            else
            {
                Patient patient = new Patient();
                patient.FirstName = PatientFirstName.Text;
                patient.LastName = PatientLastName.Text;
                patient.Gender = PatientGender.Text;
                patient.Designation = PatientDesignation.Text;
                patient.PhoneNumber = PatientPhoneNumber.Text;
                patient.ProvidedId = PatientProvidedId.Text;
                new PatientRepository().AddNewPatient(patient);

                await this.ShowMessageAsync("New patient added !", $"{PatientFirstName.Text + " " + PatientLastName.Text}");
                Util.Clear(this);
            }
        }
    }
}
