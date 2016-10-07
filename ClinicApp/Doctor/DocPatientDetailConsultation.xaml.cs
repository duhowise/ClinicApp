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
    }
}
