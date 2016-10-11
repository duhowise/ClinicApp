using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Forms;
using ClinicApp.Data;
using ClinicApp.Logic;
using ClinicModel;
using MessageBox = System.Windows.MessageBox;

namespace ClinicApp.Pharmacist
{
    public partial class PharDispenseDrugWin : Form
    {
        CMB cmb = new CMB();
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
                        string query = "select Name from DrugsOld";
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

                new Pharmacy().DispenseDrug(textBoxProvidedId.Text, textBoxDrugName.Text, textBoxQuantity.Text,MainWindow.ID);

                //MessageBox.Show("DrugsOld saved Saved Successfully", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);
                this.textBoxDrugName.Text = "";
                this.textBoxQuantity.Text = "";


            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
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

        private void textBoxDrugName_Leave(object sender, EventArgs e)
        {
            //compute availables here
            if (!string.IsNullOrEmpty(this.textBoxDrugName.Text))
            {
                labelQuantity.Text = new DrugRepository().GetRemainingDrugs(new Drug { BrandName = textBoxDrugName.Text }) + "";
            }
        }

        private void textBoxQuantity_TextChanged(object sender, EventArgs e)
        {

            int num;
            if (!string.IsNullOrEmpty(textBoxQuantity.Text))
            {
                if (int.TryParse(textBoxQuantity.Text, out num))
                {
                    if (new DrugRepository().GetRemainingDrugs(new Drug {BrandName = textBoxDrugName.Text}) > Convert.ToInt32(textBoxQuantity.Text))
                    {
                        labelQuantity.Text =
                            Convert.ToInt32(new DrugRepository().GetRemainingDrugs(new Drug { BrandName = textBoxDrugName.Text })) -
                            Convert.ToInt32(textBoxQuantity.Text) + "";
                    }
                    else
                    {
                        cmb.Message = "DrugsOld available is less than \nthe quantity specified";
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
                labelQuantity.Text = new DrugRepository().GetRemainingDrugs(new Drug {BrandName = ""}) + "";
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
