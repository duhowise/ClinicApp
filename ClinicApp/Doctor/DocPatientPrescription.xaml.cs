using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using ClinicModel;

namespace ClinicApp.Doctor
{
    /// <summary>
    /// Interaction logic for DocPatientPrescription.xaml
    /// </summary>
    public partial class DocPatientPrescription : Window
    {
        CMB cmb = new CMB();
        public DocPatientPrescription()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // TODO save patinent complaint
            if (string.IsNullOrEmpty(Complaint.Text) || string.IsNullOrEmpty(Prescription.Text))
            {
                cmb.Message = "All Fields Are Required";
                cmb.Show();
                //MessageBox.Show("All Feild Are Required", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                //new Patient().AddPatientComplaint(ProvidedId.Text,Complaint.Text, Prescription.Text);
                cmb.Message = "Patient Complaint Saved Successfully";
                cmb.Show();
                //MessageBox.Show("Drug saved Saved Successfully", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);
                Hide();
               
            }
        }

        private void Complaint_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ComplaintCancel_Click(object sender, RoutedEventArgs e)
        {


            var response = MessageBox.Show("Do you really want to close this window", "Exit",
                MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

            if (response == MessageBoxResult.No)
            {

            }
            else
            {
                Hide();
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            var response = MessageBox.Show("Do Really Want to Exit?", "Exit",
               MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

            if (response == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }

        private void ProvidedId_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
