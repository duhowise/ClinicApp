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
using ClinicApp.Logic;

namespace ClinicApp
{
    /// <summary>
    /// Interaction logic for PharDispenseDrug.xaml
    /// </summary>
    public partial class PharDispenseDrug : Window
    {
        CMB cmb = new CMB();
        Drug availableDrug = new Drug();
        public PharDispenseDrug()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // TODO save patinent complaint
            if (string.IsNullOrEmpty(DispenseProvidedId.Text) || string.IsNullOrEmpty(DispenseDrugName.DisplayMemberPath) || string.IsNullOrEmpty(DispenseDrugQuantity.Text))
            {
                cmb.Message = "All Fields Are Required";
                cmb.Show();
                //MessageBox.Show("All Feild Are Required", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {

                new Pharmacy().DispenseDrug(DispenseProvidedId.Text, DispenseDrugName.SearchText, DispenseDrugQuantity.Text, CurrentUserLoggedInData.Id);

                //MessageBox.Show("Drug saved Saved Successfully", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DispenseDrugName.SearchText = "";
                this.DispenseDrugQuantity.Text = "";


            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {

            var response = System.Windows.MessageBox.Show("Do you really want to close this window", "Exit",
                MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

            if (response == MessageBoxResult.No)
            {

            }
            else
            {
                Hide();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DispenseProvidedId.Text = ViewPatient.PatientList1[2];
        }

        private void DispenseDrugName_LostFocus(object sender, RoutedEventArgs e)
        {
            //compute availables here
            if (!string.IsNullOrEmpty(this.DispenseDrugName.SearchText))
            {
                lbRemainingDrugs.Content = availableDrug.GetDrugRemaining(DispenseDrugName.SearchText) + "";
            }

        }

        private void DispenseDrugQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            int num;
            if (!string.IsNullOrEmpty(DispenseDrugQuantity.Text))
            {
                if (int.TryParse(DispenseDrugQuantity.Text, out num))
                {
                    if (availableDrug.GetDrugRemaining(DispenseDrugName.SearchText) > Convert.ToInt32(DispenseDrugQuantity.Text))
                    {
                        lbRemainingDrugs.Content =
                            Convert.ToInt32(availableDrug.GetDrugRemaining(DispenseDrugName.SearchText)) -
                            Convert.ToInt32(DispenseDrugQuantity.Text) + "";
                    }
                    else
                    {
                        cmb.Message = "Drugs available is less than \nthe quantity specified";
                        cmb.Show();
                    }
                }
                else
                {
                    cmb.Message = "Cant have letters";
                    cmb.Show();
                    //MessageBox.Show("Cant have letters", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                lbRemainingDrugs.Content = availableDrug.GetDrugRemaining("") + "";
            }
        }
    }
}
