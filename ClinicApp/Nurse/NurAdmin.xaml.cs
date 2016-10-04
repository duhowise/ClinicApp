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
