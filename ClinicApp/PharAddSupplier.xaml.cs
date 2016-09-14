using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ClinicApp.Logic;
using MessageBox = System.Windows.MessageBox;

namespace ClinicApp
{
    /// <summary>
    /// Interaction logic for PharAddSupplier.xaml
    /// </summary>
    public partial class PharAddSupplier : Window
    {
        CMB cmb = new CMB();
        public PharAddSupplier()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var response = System.Windows.MessageBox.Show("Do you really want to close the Application", "Exit",
                 MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (response == MessageBoxResult.Yes)
            {
                Hide();
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (SupplierName.Text==""||Adress.Text=="" ||Phone.Text=="")
            {
                MessageBox.Show("All Feild Are Required","Info",MessageBoxButton.OK,MessageBoxImage.Information);
                SupplierName.Focus();
            }
            else
            {
                if (new Pharmacy().AddSupplier(SupplierName.Text, Adress.Text, Phone.Text))
                {
                    //MessageBox.Show("Data successfully saved", "Success", MessageBoxButton.OK,
                    //    MessageBoxImage.Information);
                    Hide();
                    cmb.Message = "Supplier Saved Successfully";
                    cmb.Show();
                }
                else
                {
                    cmb.Message = " ERROR!!! Supplier Could not be Saved Successfully";
                    cmb.Show();
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SupplierName.Focus();
            CurrentUserLoggedInData.IsLoaded = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            var response = System.Windows.MessageBox.Show("Do you really want to close this Window", "Exit",
                 MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (response==MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
        }
    }
}
