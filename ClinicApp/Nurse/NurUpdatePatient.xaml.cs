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
        Patient patient = new Patient();
        public NurUpdatePatient()
        {
            InitializeComponent();
            
        }

        public NurUpdatePatient(Patient updatepatient)
        {
            InitializeComponent();
            patient.Id = updatepatient.Id;
            PatientFirstName.Text = updatepatient.FirstName;
           // PatientGender.Items.Add(updatepatient.Gender);
            PatientDesignation.Items.Add(updatepatient.Designation);
            PatientGender.SelectedItem = $"{updatepatient.Gender}";
            PatientLastName.Text = updatepatient.LastName;
            PatientDesignation.SelectedItem = $"{updatepatient.Designation}";
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
            PatientDesignation.Items.Add("Staff");
            PatientDesignation.Items.Add("Student");
            PatientDesignation.Items.Add("Dependant");
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
               
                patient.FirstName = PatientFirstName.Text.Trim();
                patient.LastName = PatientLastName.Text.Trim();
                patient.Gender = PatientGender.SelectionBoxItem.ToString();
                patient.Designation = PatientDesignation.SelectedItem.ToString().Trim();
                patient.PhoneNumber = PatientPhoneNumber.Text.Trim();
                patient.ProvidedId = PatientProvidedId.Text.Trim();
                if (patient.Id.HasValue)
                {
                    new PatientRepository().UpdatePatient(patient);

                }
                else
                {
                    new PatientRepository().AddNewPatient(patient);
                }
                patient = null;
                await this.ShowMessageAsync("Successfully saved ", $"{PatientFirstName.Text + " " + PatientLastName.Text}");
                Util.Clear(this);
            }
        }
    }
}
