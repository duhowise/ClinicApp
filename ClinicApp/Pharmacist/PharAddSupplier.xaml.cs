using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using ClinicApp.Data;
using ClinicApp.Logic;
using ClinicModel;
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
        private void TextValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^a-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }private void EmailValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            var response = MessageBox.Show("Do you really want to close the window?", "Exit",
                 MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (response == MessageBoxResult.Yes)
                Hide();
           
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SupplierName.Text) 
                ||string.IsNullOrWhiteSpace(Adress.Text) 
                ||string.IsNullOrWhiteSpace(Email.Text) 
                ||string.IsNullOrWhiteSpace(Phone.Text))
            {
                cmb.Message = "All feilds are Required";
                cmb.Show();
            }
            else
            {
                new SupplierRepository().AddNewSupplier(new Supplier
                {
                    Name = SupplierName.Text,
                    Address = Adress.Text,
                    Email = Email.Text,
                    Phone = Phone.Text
                });
                    cmb.Message = $"Supplier {SupplierName} Saved Successfully";
                    cmb.Show();
               
                
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
