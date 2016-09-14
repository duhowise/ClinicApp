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
using ClinicApp.Logic;

namespace ClinicApp
{
    /// <summary>
    /// Interaction logic for DocUpdatePatient.xaml
    /// </summary>
    public partial class DocUpdatePatient : Window
    {
        CMB cmb = new CMB();
        public DocUpdatePatient()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Pharmacy.UpdatePatient();
            if (string.IsNullOrEmpty(UpdateFirstName.Text) || string.IsNullOrEmpty(UpdateLastName.Text) ||
               string.IsNullOrEmpty(UpdateProvidedId.Text) || string.IsNullOrEmpty(UpdateDesignation.Text) || string.IsNullOrEmpty(UpdatePhone.Text))
            {
                cmb.Message = "All Feild Are Required";
                cmb.Show();
               // MessageBox.Show("All Feild Are Required", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Pharmacy.UpdatePatient(UpdateFirstName.Text, UpdateLastName.Text, UpdateProvidedId.Text,UpdateDesignation.Text,UpdatePhone.Text);
                cmb.Message = "Patient data updated Successfully";
                cmb.Show();
                //MessageBox.Show("Patient data updated Successfully", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);


                this.Hide();

            }
        }

        private void UpdateCancel_Click(object sender, RoutedEventArgs e)
        {


            var response = System.Windows.MessageBox.Show("Do you really want to close this window", "Exit",
                MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

            if (response == MessageBoxResult.No)
            {

            }
            else
            {
                Hide();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateFirstName.Text = viewPatientWin.PatientList1[0];
            UpdateLastName.Text = viewPatientWin.PatientList1[1];
            UpdateProvidedId.Text = viewPatientWin. PatientList1[2];
            UpdatePhone.Text = viewPatientWin. PatientList1[4];
            UpdateDesignation.Text = viewPatientWin.PatientList1[3];
        }
    }
}
