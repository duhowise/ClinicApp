using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;

namespace ClinicApp.Nurse
{
    /// <summary>
    /// Interaction logic for NurAdmin.xaml
    /// </summary>
    public partial class NurAdmin : MetroWindow
    {
        public NurAdmin()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void NewPatient_Click(object sender, RoutedEventArgs e)
        {
            new NurAddPatient().ShowDialog();
        }
    }
}
