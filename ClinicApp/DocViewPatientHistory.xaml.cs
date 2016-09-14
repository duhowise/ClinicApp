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

namespace ClinicApp
{
    /// <summary>
    /// Interaction logic for DocViewPatientHistory.xaml
    /// </summary>
    public partial class DocViewPatientHistory : Window
    {
        public string ProvidedId { get; set; }
        public DocViewPatientHistory()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ProvidedId = viewPatientWin.PatientList1[2];
            Patient patient = new Patient();
            if(patient.RetrieveHistory(ProvidedId)!=null)
            PatientHistory.ItemsSource = patient.RetrieveHistory(ProvidedId);
            else
            {
                CMB cmb = new CMB {Message = "There is no History for Selected Patient"};
                cmb.Show();
            }
        }


        private void RadGridView_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {

        }

        private void RadGridView_SelectionChanging(object sender, Telerik.Windows.Controls.SelectionChangingEventArgs e)
        {

        }

        private void RadGridView_Sorted(object sender, Telerik.Windows.Controls.GridViewSortedEventArgs e)
        {

        }

        private void RadGridView_Sorting(object sender, Telerik.Windows.Controls.GridViewSortingEventArgs e)
        {

        }
    }
}
