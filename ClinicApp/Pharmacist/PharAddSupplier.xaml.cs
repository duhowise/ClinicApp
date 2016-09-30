using System;
using System.ComponentModel;
using System.Windows;
using ClinicApp.Logic;
using MahApps.Metro.Controls;

namespace ClinicApp.Pharmacist
{
    /// <summary>
    /// Interaction logic for PharAddSupplier.xaml
    /// </summary>
    public partial class PharAddSupplier : MetroWindow
    {
        CMB cmb = new CMB();
        public PharAddSupplier()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            var response = MessageBox.Show("Do you really want to close the Application", "Exit",
                 MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (response == MessageBoxResult.Yes)
                Hide();
           
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
            var response = MessageBox.Show("Do you really want to close this Window", "Exit",
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
