using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace ClinicApp
{
    public class Drug
    {
        public string Name { get; set; }
        private CMB cmb = new CMB();

        public DataTable RetrieveAll()
        {
            var data = new DataTable();
            using (
                SqlConnection connection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["ClinicConnection"].ConnectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                        string query = "select * from DrugList";
                        var dataAdapter = new SqlDataAdapter(query, connection);
                        dataAdapter.Fill(data);
                        connection.Close();
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message.ToString(), "Error", MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);
                }
                return data;
            }

        }


        public int TotalDrugsInStock(String tableName)
        {
            int total = 0;

            using (
                SqlConnection connection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["ClinicConnection"].ConnectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                        string query = "SELECT SUM(Quantity) AS TotalDrugsQuantity FROM " + tableName;
                        var command = new SqlCommand(query, connection) {CommandType = CommandType.Text};
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            if (reader.GetInt32(0) > 0)
                            {
                                total += reader.GetInt32(0);
                            }
                            else
                            {
                                total = 0;
                            }
                        }
                        connection.Close();
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message.ToString(), "Error", MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);
                }

                return total;
            }
        }

        public DataTable RetrieveDrugHistory()
        {
            var data = new DataTable();
            using (
                SqlConnection connection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["ClinicConnection"].ConnectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                        //string query = "select DrugId,Quantity,Date from PrescribedDrugs";
                        string query =
                            "SELECT Drugs.Name, PrescribedDrugs.Quantity, PrescribedDrugs.Date FROM Drugs INNER JOIN PrescribedDrugs ON Drugs.Id = PrescribedDrugs.DrugId";
                        var dataAdapter = new SqlDataAdapter(query, connection);
                        dataAdapter.Fill(data);
                        connection.Close();
                    }
                }
                catch (Exception exception)
                {
                    cmb.Message = exception.Message;
                    cmb.Show();
                    //MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK,MessageBoxImage.Exclamation);
                }
                return data;
            }
        }

      
        public static DataTable LoadFrequentDrugs()
        {
            var data = new DataTable();
            using (
                SqlConnection connection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["ClinicConnection"].ConnectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                        //string query = "select DrugId,Quantity,Date from PrescribedDrugs";
                        string query =
                            "SELECT TOP 5 [Drug Name], [Number Of Drugs Dispensed] FROM FrequentDrugs f ORDER BY f.[Number of times Dispensed] DESC";
                        var dataAdapter = new SqlDataAdapter(query, connection);
                        dataAdapter.Fill(data);
                        connection.Close();
                    }
                }
                catch (Exception exception)
                {
                    //cmb.Message = exception.Message;
                    //cmb.Show();
                    MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK,MessageBoxImage.Exclamation);
                }
                return data;
            }
        }

        public static List<string> FetchByName(string name)
        {
            List<string> detaiList = new List<string>();
            using (
                SqlConnection connection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["ClinicConnection"].ConnectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                        string query = "select * from Drugs  where  Name='" + name + "'";
                        var command = new SqlCommand(query, connection);
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            detaiList.Add(reader.GetString(1));

                            detaiList.Add(reader.GetInt32(2).ToString());
                            detaiList.Add(reader.GetInt32(3).ToString());
                            detaiList.Add(reader.GetInt32(4).ToString());
                        }
                        connection.Close();
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message.ToString(), "Error", MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);
                }
                return detaiList;
            }
        }

        public int GetDrugRemaining(string drugName)
        {
            int remaining = 0;
            //fetch drugId by passing drugName as a parameter to the query
            if (drugName!="")
            {
                using (
               SqlConnection connection =
                   new SqlConnection(ConfigurationManager.ConnectionStrings["ClinicConnection"].ConnectionString))
                {
                    try
                    {
                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                            string query = $"SELECT dbo.RemainingDrugs('{drugName}')";
                            var command = new SqlCommand(query, connection);
                            var reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                remaining = reader.GetInt32(0);
                            }
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
            return remaining;
        }
    }
}