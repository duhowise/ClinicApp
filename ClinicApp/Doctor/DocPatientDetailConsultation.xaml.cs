using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ClinicApp.Doctor
{
    /// <summary>
    /// Interaction logic for DocPatientDetailConsultation.xaml
    /// </summary>
    public partial class DocPatientDetailConsultation : UserControl
    {
        public DocPatientDetailConsultation()
        {
            InitializeComponent();
        }

        private void btnEditDiagnosis_Click(object sender, RoutedEventArgs e)
        {
            tbDiagnosis.IsEnabled = true;
        }

        private void btnClearDiagnosis_Click(object sender, RoutedEventArgs e)
        {
            var response = MessageBox.Show("please be careful, you might lose data. Do you really want to clear diagnosis?", "Exit",
                MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (response == MessageBoxResult.Yes)
                tbDiagnosis.Clear();
        }

        private void btnEditPrescription_Click(object sender, RoutedEventArgs e)
        {
            tbPrescriptions.IsEnabled = true;
        }

        private void btnClearPrescription_Click(object sender, RoutedEventArgs e)
        {
            var response = MessageBox.Show("please be careful, you might lose data. Do you really want to clear diagnosis?", "Exit",
               MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (response == MessageBoxResult.Yes)
                tbPrescriptions.Clear();
        }

        private void btnEditFinding_Click(object sender, RoutedEventArgs e)
        {
            tbFindings.IsEnabled = true;
        }

        private void btnClearFinding_Click(object sender, RoutedEventArgs e)
        {
            var response = MessageBox.Show("please be careful, you might lose data. Do you really want to clear diagnosis?", "Exit",
              MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (response == MessageBoxResult.Yes)
                tbFindings.Clear();
        }

        private void btnDiagnosisSave_Click(object sender, RoutedEventArgs e)
        {
            tbDiagnosis.IsEnabled = false;
        }

        private void btnPrescriptionsSave_Click(object sender, RoutedEventArgs e)
        {
            tbPrescriptions.IsEnabled = false;
        }

        private void btnFindingsSave_Click(object sender, RoutedEventArgs e)
        {
            tbFindings.IsEnabled = false;
        }
    }
}
