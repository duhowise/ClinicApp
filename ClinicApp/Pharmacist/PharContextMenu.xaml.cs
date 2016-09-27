using System.Windows;

namespace ClinicApp.Pharmacist
{
    /// <summary>
    /// Interaction logic for PharContextMenu.xaml
    /// </summary>
    public partial class PharContextMenu : Window
    {
        public PharContextMenu()
        {
            InitializeComponent();
        }

        private void miCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void miUpdate_Click(object sender, RoutedEventArgs e)
        {
            Close();

            var dispense= new PharDispenseDrugWin();
            dispense.ShowDialog();
            //var dispenseDrug = new PharDispenseDrug();
            //dispenseDrug.ShowDialog();
        }
    }
}
