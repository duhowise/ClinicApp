using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ClinicApp.Data;
using ClinicModel;
using MahApps.Metro.Controls;
using MessageBox = System.Windows.MessageBox;

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
        }
        private Regex EmailValidation()
        {
         return new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
        }
        private Regex AddressValidation()
        {
            return new Regex(@"(?i)\b(?:p(?:ost)?\.?\s*[o0](?:ffice)?\.?\s*b(?:[o0]x)?|b[o0]x)");
        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            var response = MessageBox.Show("Do you really want to close the window?", "Exit",
                 MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (response == MessageBoxResult.Yes)
                Hide();
           
        }

        public void Clear(Visual myMainWindow)
        {
            int childrenCount = VisualTreeHelper.GetChildrenCount(myMainWindow);
            for (int i = 0; i < childrenCount; i++)
            {
                var visualChild = (Visual)VisualTreeHelper.GetChild(myMainWindow, i);
                if (visualChild is TextBox)
                {
                    TextBox tb = (TextBox)visualChild;
                    tb.Text="";
                }
                Clear(visualChild);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SupplierName.Text)||string.IsNullOrWhiteSpace(Adress.Text) 
                ||string.IsNullOrWhiteSpace(Email.Text)||string.IsNullOrWhiteSpace(Phone.Text))
            {
                cmb.Message = "All feilds are Required";
                cmb.Show();
            }
            else
            {
                if (!AddressValidation().IsMatch(Adress.Text))
                {
                    MessageBox.Show("Please check your address");
                    Adress.Foreground = Brushes.OrangeRed;

                }
                else if(!EmailValidation().IsMatch(Email.Text))
                {
                    MessageBox.Show("Please check your  email");
                    Email.Foreground = Brushes.OrangeRed;
                }
                else
                {
                    var suppplier = new Supplier();
                    suppplier.Name = SupplierName.Text;
                    suppplier.Address = Adress.Text;
                    suppplier.Email = Email.Text;
                    suppplier.Phone = Phone.Text;

                    List<Supplier> suppliers=(List<Supplier>)new SupplierRepository().GetAllSuppliers();
                    var result = suppliers.FindAll(s=>s.Name.Equals(suppplier.Name));
                   
                    if (result.Count==0)
                    {
                            new SupplierRepository().AddNewSupplier(suppplier);
                            cmb.Message = $"Supplier {SupplierName.Text} Saved Successfully";
                            cmb.Show();
                            Clear(this);
                    
                    }
                    else
                    {
                        MessageBox.Show($"{SupplierName.Text} already exists");
                    }
                    
                }


            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SupplierName.Focus();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
          Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
        }
    }
}
