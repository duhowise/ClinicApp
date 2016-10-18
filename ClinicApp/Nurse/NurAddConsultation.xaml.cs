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
using MahApps.Metro.Controls;

namespace ClinicApp.Nurse
{
    /// <summary>
    /// Interaction logic for NurAddConsultation.xaml
    /// </summary>
    public partial class NurAddConsultation : MetroWindow
    {
        public NurAddConsultation()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            var response = MessageBox.Show("Do you really want to close out of this window\n make sure your changes are saved", "Exit",
               MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (response == MessageBoxResult.Yes) { Hide(); } else { e.Cancel = true; }
        }

        private void SaveConsultation_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
