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
