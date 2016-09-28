using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using ClinicApp.Logic;

namespace ClinicApp.Doctor
{
    /// <summary>
    /// Interaction logic for DocAddPatient.xaml
    /// </summary>
    public partial class DocAddPatient : Window
    {
       CMB cmb = new CMB();
        public DocAddPatient()
        {
            InitializeComponent();
        }


        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(FirstName.Text) || string.IsNullOrEmpty(LastName.Text) || string.IsNullOrEmpty(ProvidedId.Text) || string.IsNullOrEmpty(Phone.Text) || string.IsNullOrEmpty(Designation.SelectedItem.ToString()))
            {
                cmb.Message = "All Feild Are Required";
                cmb.Show();
                //MessageBox.Show("All Feild Are Required", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Pharmacy.AddNewPatient(FirstName.Text, LastName.Text, ProvidedId.Text, Designation.Text, Phone.Text);
                cmb.Message = "Patient Addes Successfully";
                cmb.Show();
                //MessageBox.Show("Patient Addes Successfully", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);
                Hide();
            }
        }

        private void Phone_TextChanged(object sender, TextChangedEventArgs e)
        {
            int num;
            if (string.IsNullOrEmpty(Phone.Text) == false)
            {
                if (!int.TryParse(Phone.Text, out num))
                {
                    cmb.Message = "Cant have letters";
                    cmb.Show();
                    //MessageBox.Show("Cant have letters", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                } 
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FirstName.Focus();
            for (double i = 0.0; i < 10.0; i += 0.02)
            {
                if (this.Opacity < 100)
                {
                    this.Opacity += 0.1;
                    Thread.Sleep(10);
                }
            }
          
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            var response = MessageBox.Show("Do you really want to close this window", "Exit",
               MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

            if (response == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            CurrentUserLoggedInData.IsLoaded = false;
         
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {

            var response = MessageBox.Show("Do you really want to close this window", "Exit",
                MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

            if (response == MessageBoxResult.No)
            {

            }
            else
            {
                Hide();
            }
        }
    }
}
