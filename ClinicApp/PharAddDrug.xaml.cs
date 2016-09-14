using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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
    /// Interaction logic for PharAddDrug.xaml
    /// </summary>
    public partial class PharAddDrug : Window
    { 
        CMB cmb =new CMB();
      
        public PharAddDrug()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //DialogResult dr= new DialogResult();

            //call confirmBox here

            var response = System.Windows.MessageBox.Show("Do you really want to Stop adding new drug", "Exit",
                MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

            if (response == MessageBoxResult.No)
            {
                e.Cancel = true;
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
                Close();
            }
        }

        private void AddNewSupplier_Click(object sender, RoutedEventArgs e)
        {
            PharAddSupplier ps= new PharAddSupplier();

            ps.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DrugName.Focus();
            Supplier.Items.Clear();
            using (
               SqlConnection connection =
                   new SqlConnection(ConfigurationManager.ConnectionStrings["ClinicConnection"].ConnectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                        string query = "select name from dbo.supplier";
                        var command = new SqlCommand(query, connection) { CommandType = CommandType.Text };
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Supplier.Items.Add(reader.GetString(0));
                        }
                        reader.Close();
                        connection.Close();
                    }

                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message.ToString(), "Error", MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);
                }

            }
        }

        private void Supplier_Initialized(object sender, EventArgs e)
        {
            Supplier.SelectedItem = "Select Supplier";
        }

        private void Supplier_Loaded(object sender, RoutedEventArgs e)
        {
            Supplier.Items.Clear();
            using (
               SqlConnection connection =
                   new SqlConnection(ConfigurationManager.ConnectionStrings["ClinicConnection"].ConnectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                        string query = "select name from dbo.supplier";
                        var command = new SqlCommand(query, connection) { CommandType = CommandType.Text };
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Supplier.Items.Add(reader.GetString(0));
                        }
                        reader.Close();
                        connection.Close();
                    }

                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message.ToString(), "Error", MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);
                }

            }
        }

        private void Supplier_DropDownOpened(object sender, EventArgs e)
        {
            Supplier.Items.Clear();
            using (
               SqlConnection connection =
                   new SqlConnection(ConfigurationManager.ConnectionStrings["ClinicConnection"].ConnectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                        string query = "select name from dbo.supplier";
                        var command = new SqlCommand(query, connection) { CommandType = CommandType.Text };
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Supplier.Items.Add(reader.GetString(0));
                        }
                        reader.Close();
                        connection.Close();
                    }

                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message.ToString(), "Error", MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);
                }

            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(DrugName.Text)|| string.IsNullOrEmpty(BoxQuantity.Text)|| string.IsNullOrEmpty(NumberInBox.Text)|| string.IsNullOrEmpty(TotalQuantity.Text)|| string.IsNullOrEmpty(Supplier.SelectedItem.ToString())|| string.IsNullOrEmpty(ExpiryDate.Text))
            {
                cmb.Message = "All Fields Are Required";
                cmb.Show();
                //MessageBox.Show("All Feild Are Required", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (Pharmacy.AddNewDrug(DrugName.Text, Convert.ToInt32(BoxQuantity.Text),
                    Convert.ToInt32(NumberInBox.Text),
                    Convert.ToInt32(TotalQuantity.Text), Supplier.SelectedItem.ToString(),
                    Convert.ToDateTime(ExpiryDate.SelectedDate.Value.ToShortDateString())))
                {
                    cmb.Message = "Drug Saved Successfully";
                    cmb.Show();
                    //MessageBox.Show("Drug saved Saved Successfully", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);
                    Hide();
                    new PharAddDrug().ShowDialog();
                }
                else
                {
                    cmb.Message = " ERROR!!! Drug Could not be Saved Successfully";
                    cmb.Show();
                }
            }
        }

        private void BoxQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
           
                        
        }
        
        //calculating total quantity on  textChanged
        private void NumberInBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int num;
            if (string.IsNullOrEmpty(BoxQuantity.Text) == false)
            {
                if (int.TryParse(BoxQuantity.Text, out num))
                {
                    TotalQuantity.Text =
                        Convert.ToString(Convert.ToInt64(BoxQuantity.Text)*Convert.ToInt64(NumberInBox.Text));
                }
                else
                {
                    cmb.Message = "Can't Have Letters";
                    cmb.Show();
                   // MessageBox.Show("Cant have letters", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

        }
        public void DrugNames()
        {
            AutoCompleteStringCollection nameSource = new AutoCompleteStringCollection();
            using (
                SqlConnection connection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["ClinicConnection"].ConnectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                        string query = "select name from dbo.drugs";
                        var command = new SqlCommand(query, connection) { CommandType = CommandType.Text };
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            nameSource.Add(reader.GetString(0));
                        }
                        reader.Close();
                        connection.Close();
                    }

                }
                catch (Exception exception)
                {
                    cmb.Message = exception.Message;
                    cmb.Show();
                   // MessageBox.Show(exception.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }

            }
           
        }

        private void DrugName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Window_Closed(object sender, EventArgs e)
        {
        }
    }
}
