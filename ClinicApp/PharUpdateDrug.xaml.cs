using System;
using System.Windows;
using System.Windows.Controls;
using ClinicApp.Logic;

namespace ClinicApp
{

    /// <summary>
    /// Interaction logic for PharUpdateDrug.xaml
    /// </summary>
    public partial class PharUpdateDrug : Window
    {
        CMB cmb = new CMB();
        //public object DrugList { get; private set; }

        public PharUpdateDrug()
        {
            InitializeComponent();
        }



        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(DrugName.Text) || string.IsNullOrEmpty(BoxQuantity.Text) ||
                string.IsNullOrEmpty(NumberInBox.Text) || string.IsNullOrEmpty(TotalQuantity.Text))
            {
                cmb.Message = "All Feild Are Required";
                cmb.Show();
               // MessageBox.Show("All Feild Are Required", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Pharmacy.UpdateDrugNewStock(DrugName.Text, Convert.ToInt32(BoxQuantity.Text),
                    Convert.ToInt32(NumberInBox.Text), Convert.ToInt32(TotalQuantity.Text));
                cmb.Message = "New Stock added Successfully";
                cmb.Show();
               // MessageBox.Show("New Stock added Successfully", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);
               
                
                this.Hide();
               
            }
        }

        private void NumberInBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int num;
            if (string.IsNullOrEmpty(BoxQuantity.Text) == false)
            {
                if (int.TryParse(BoxQuantity.Text, out num))
                {
                    TotalQuantity.Text =
                        Convert.ToInt32(BoxQuantity.Text)*Convert.ToInt32(NumberInBox.Text) + "";
                }
                else
                {
                    cmb.Message = "Cant have letters";
                    cmb.Show();
                    //MessageBox.Show("Cant have letters", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
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
                Close();
            }
        }
    }
}
