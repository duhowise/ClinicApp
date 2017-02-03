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
using MahApps.Metro.Controls.Dialogs;
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
            Closing += PharAddSupplier_Closing;
        }

        private async void PharAddSupplier_Closing(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SupplierName.Text) || string.IsNullOrWhiteSpace(Adress.Text)
                || string.IsNullOrWhiteSpace(Email.Text) || string.IsNullOrWhiteSpace(Phone.Text))
            {
               Hide();
            }
            else
            {
                MessageDialogResult response = await this.ShowMessageAsync("Do you really want to stop Adding new Supplier? \n" +
                                                                       " All your changes will be discarded", "Exit", MessageDialogStyle.AffirmativeAndNegative);
                if (response == MessageDialogResult.Affirmative)
                {
                    Hide();
                }
                else { e.Cancel = true; }
            }
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
        private async void Window_Closing(object sender, CancelEventArgs e)
        {

            
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

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SupplierName.Text)||string.IsNullOrWhiteSpace(Adress.Text) 
                ||string.IsNullOrWhiteSpace(Email.Text)||string.IsNullOrWhiteSpace(Phone.Text))
            {
                await this.ShowMessageAsync("Attention!", "All Fields Are Required");
            }
            else
            {
                if (!AddressValidation().IsMatch(Adress.Text))
                {
                    await this.ShowMessageAsync("Attention!", "Please check your address \n only p.o.box formats are accepted");

                    Adress.Foreground = Brushes.OrangeRed;

                }
                else if(!EmailValidation().IsMatch(Email.Text))
                {
                    await this.ShowMessageAsync("Attention!", "Please check your email address");

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
                          
                        await this.ShowMessageAsync("Attention!", $"{SupplierName.Text} Saved Successfully");

                        Clear(this);
                    
                    }
                    else
                    {
                        await this.ShowMessageAsync("Attention!", $"{SupplierName.Text} already exists");

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
