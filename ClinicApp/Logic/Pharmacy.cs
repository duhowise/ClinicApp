using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace ClinicApp.Logic
{
    public class Pharmacy
    {
        private static int oldBoxValue;
        private static int oldNumberInBoxValue;
        private static int oldTotalQunatityValue;
        public string UserCode = new MainWindow().userName.Text;

        public static int OldBoxValue { get; set; }
        public static int OldNumberInBoxValue { get; set; }
        public static int OldTotalQunatityValue { get; set; }

        CMB cmb =new CMB();
        public int DispensedDrugs()
        {
            int count = 0;
            using (
                SqlConnection connection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["ClinicConnection"].ConnectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                        string query = "";
                        var command = new SqlCommand(query, connection) {CommandType = CommandType.Text};
                        command.Parameters.AddWithValue("", "");
                        command.Parameters.AddWithValue("", "");
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            count = reader.GetInt32(0);
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
                return count;
            }
        }

        public int RemainingDrugs()
        {
            int count = 0;
            using (
                SqlConnection connection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["ClinicConnection"].ConnectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                        string query = "";
                        var command = new SqlCommand(query, connection) {CommandType = CommandType.Text};
                        command.Parameters.AddWithValue("", "");
                        command.Parameters.AddWithValue("", "");
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            count = reader.GetInt32(0);
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
                return count;
            }
        }

        public int TotalDrugs()
        {
            int count = 0;
            using (
                SqlConnection connection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["ClinicConnection"].ConnectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                        string query = "";
                        var command = new SqlCommand(query, connection) {CommandType = CommandType.Text};
                        command.Parameters.AddWithValue("", "");
                        command.Parameters.AddWithValue("", "");
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            count = reader.GetInt32(0);
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
                return count;
            }
        }

        public AutoCompleteStringCollection DrugNames()
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
                        var command = new SqlCommand(query, connection) {CommandType = CommandType.Text};
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
                    MessageBox.Show(exception.Message.ToString(), "Error", MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);
                }

            }
            return nameSource;
        }
    
        public DataTable DrugStats()
        {
            DataTable countRecords = new DataTable();
            using (
                SqlConnection connection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["ClinicConnection"].ConnectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                        string query = "select RoleId from Users where username='' and password=''";
                        var command = new SqlCommand(query, connection) {CommandType = CommandType.Text};
                        // command.Parameters.AddWithValue(@username, username);
                        // command.Parameters.AddWithValue(@password, password);
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            countRecords = reader.GetSchemaTable();
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
                return countRecords;
            }
        }

        //adding new drug to the drug view table
        public static bool AddNewDrug(string drugname, int boxquantity, int numberinbox, int totalquantity, string supplier, DateTime expirydate)
        {
            bool result = false;
            try
            {
                using (
                    SqlConnection connection =
                        new SqlConnection(ConfigurationManager.ConnectionStrings["ClinicConnection"].ConnectionString))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                        string query ="INSERT INTO dbo.DrugsOld(Name,Box,NumberInBox,Quantity,ExpiryDate,SupplierId)VALUES('"+drugname+"','"+boxquantity+"','"+numberinbox+"','"+totalquantity+"','"+expirydate+"',(select id from supplier where name='"+supplier+"'))";
                        var command = new SqlCommand(query, connection) { CommandType = CommandType.Text };
                        command.ExecuteNonQuery();
                        result = true;
                        connection.Close();
                    }
                }
            }
            catch (Exception exception)
            {
                result = false;
                MessageBox.Show(exception.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

            return result;
        }


        //update drug --adding new stock to old stock function

        public static void UpdateDrugNewStock(string drugName,int boxquantity, int numberinbox, int totalquantity)
        {
            try
            {
                using (
                    SqlConnection connection =
                        new SqlConnection(ConfigurationManager.ConnectionStrings["ClinicConnection"].ConnectionString))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                        string query = "update dbo.DrugsOld set Box='" + (boxquantity + OldBoxValue)+ "'," +
                                       " NumberInBox='" + (numberinbox + OldNumberInBoxValue) + "'," +
                                       " Quantity='" + ((OldBoxValue+boxquantity) * (OldNumberInBoxValue+numberinbox)) + "' where " +
                                       "Name='"+drugName+"'";
                        var command = new SqlCommand(query, connection) { CommandType = CommandType.Text };
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }


        }
        public static void UpdatePatient(string firstname, string lastname, string providedid, string designation, string phonenumber)
        {
            try
            {
                using (
                    SqlConnection connection =
                        new SqlConnection(ConfigurationManager.ConnectionStrings["ClinicConnection"].ConnectionString))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                        string query = "UPDATE dbo.OldPatient SET  FirstName ='"+ firstname + "' , LastName ='"+ lastname + "'," +
                                       " Designation ='"+ designation + "', PhoneNumber ='"+ phonenumber + "'"   + " WHERE  ProvidedId ='"+providedid+"' ";
                        var command = new SqlCommand(query, connection) { CommandType = CommandType.Text };
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }


        }


        public bool AddSupplier(string suppliername,string address,string phone)
        {
            bool result = false;
            using (
               SqlConnection connection =
                   new SqlConnection(ConfigurationManager.ConnectionStrings["ClinicConnection"].ConnectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                        string query = @"INSERT INTO dbo.Supplier( Name,Address,Phone)VALUES('"+suppliername.ToUpper()+"','"+address.ToUpper()+"','"+phone+"')";
                        var command = new SqlCommand(query, connection);
                        command.ExecuteReader();
                        result = true;
                    }

                }
                catch (Exception exception)
                {
                    result=false;
                    MessageBox.Show(exception.Message.ToString(), "Error", MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);
                }
                
            }
            return result;
        }

        public static void AddNewPatient(string firstname, string lastname, string providedid, string designation, string phonenumber)
        {
            try
            {
                using (
                    SqlConnection connection =
                        new SqlConnection(ConfigurationManager.ConnectionStrings["ClinicConnection"].ConnectionString))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                        string query = "INSERT INTO dbo.OldPatient(FirstName,LastName,ProvidedId,Designation,PhoneNumber)VALUES('"+firstname+"','"+lastname+"','"+providedid+"','"+designation+"','"+phonenumber+"')";
                        var command = new SqlCommand(query, connection) { CommandType = CommandType.Text };
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }


        }

        public bool IsDrugExist(string drug)
        {
            bool isExist = false;

            using (
              SqlConnection connection =
                  new SqlConnection(ConfigurationManager.ConnectionStrings["ClinicConnection"].ConnectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                        string query = "select Name from DrugsOld where Name='"+drug+"'";
                        var command = new SqlCommand(query, connection);
                        var reader = command.ExecuteReader();
                        if(reader.Read())
                        {
                            isExist = true;
                        }
                        else
                        {
                            isExist = false;
                        }
                        connection.Close();
                    }
                }
                catch (Exception exception)
                {
                    isExist = false;
                   
                }

            }
            return isExist;
        }

        public void DispenseDrug(string patientId, string drugname, string quantity,int userId)
        {
            if (IsDrugExist(drugname))
            {
                try
                {
                    using (
                        SqlConnection connection =
                            new SqlConnection(
                                ConfigurationManager.ConnectionStrings["ClinicConnection"].ConnectionString))
                    {
                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                            string query = $"INSERT INTO dbo.PrescribedDrugs(PatientId,DrugId,Quantity,UserId)VALUES((select id from patient where providedid='{patientId}' ),(select top 1 id from drugs where Name='{drugname}'),'{quantity}','{userId}')";
                            var command = new SqlCommand(query, connection) {CommandType = CommandType.Text};
                            if (command.ExecuteNonQuery() > 0)
                            {
                                cmb.Message = "DrugsOld Dispensed Successfully";
                                cmb.Show();
                            }
                            connection.Close();
                        }
                    }
                }
                catch (Exception exception)
                {
                    cmb.Message = exception.Message;
                    cmb.Show();
                    //MessageBox.Show(exception.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else
            {
                cmb.Message = "OOPS!!! DrugsOld not found in stock";
                cmb.Show();
            }
        }
    }
}

