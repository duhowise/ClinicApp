using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using ClinicApp.Logic;

namespace ClinicApp
{
    public partial class PharDispenseDrugWin : Form
    {
        CMB cmb = new CMB();
        Drug availableDrug = new Drug();
        public PharDispenseDrugWin()
        {
            InitializeComponent();
        }
        public void DrugAutoCompleteSource()
        {
            var searchCollection= new AutoCompleteStringCollection();
            using (
                SqlConnection connection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["ClinicConnection"].ConnectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                        string query = "select Name from Drugs";
                        var command = new SqlCommand(query, connection);
                        var datareader = command.ExecuteReader();
                        while (datareader.Read())
                        {
                            searchCollection.Add(datareader.GetString(0));
                        }
                        textBoxDrugName.AutoCompleteCustomSource = searchCollection;
                        datareader.Close();
                        connection.Close();
                    }
                }
                catch (Exception exception)
                {
                    cmb.Message = exception.Message;
                    cmb.Show();
                }
               
            }
        }
        private void PharDispenseDrugWin_Load(object sender, EventArgs e)
        {
            textBoxProvidedId.Text = viewPatientWin.PatientList1[2];
           DrugAutoCompleteSource();

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxProvidedId.Text) || string.IsNullOrEmpty(textBoxDrugName.Text) || string.IsNullOrEmpty(textBoxQuantity.Text))
            {
                cmb.Message = "All Fields Are Required";
                cmb.Show();
                //MessageBox.Show("All Feild Are Required", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {

                new Pharmacy().DispenseDrug(textBoxProvidedId.Text, textBoxDrugName.Text, textBoxQuantity.Text, CurrentUserLoggedInData.Id);

                //MessageBox.Show("Drug saved Saved Successfully", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);
                this.textBoxDrugName.Text = "";
                this.textBoxQuantity.Text = "";


            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
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

        private void textBoxDrugName_Leave(object sender, EventArgs e)
        {
            //compute availables here
            if (!string.IsNullOrEmpty(this.textBoxDrugName.Text))
            {
                labelQuantity.Text = availableDrug.GetDrugRemaining(textBoxDrugName.Text) + "";
            }
        }

        private void textBoxQuantity_TextChanged(object sender, EventArgs e)
        {

            int num;
            if (!string.IsNullOrEmpty(textBoxQuantity.Text))
            {
                if (int.TryParse(textBoxQuantity.Text, out num))
                {
                    if (availableDrug.GetDrugRemaining(textBoxDrugName.Text) > Convert.ToInt32(textBoxQuantity.Text))
                    {
                        labelQuantity.Text =
                            Convert.ToInt32(availableDrug.GetDrugRemaining(textBoxDrugName.Text)) -
                            Convert.ToInt32(textBoxQuantity.Text) + "";
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
                labelQuantity.Text = availableDrug.GetDrugRemaining("") + "";
            }
        }

        private void textBoxProvidedId_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxDrugName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
