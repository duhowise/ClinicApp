using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace ClinicApp
{
    public class DrugsOld
    {
        public string Name { get; set; }
        private CMB cmb = new CMB();

        


        
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
                            "SELECT DrugsOld.Name, PrescribedDrugs.Quantity, PrescribedDrugs.Date FROM DrugsOld INNER JOIN PrescribedDrugs ON DrugsOld.Id = PrescribedDrugs.DrugId";
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
                            "SELECT TOP 5 [DrugsOld Name], [Number Of DrugsOld Dispensed] FROM FrequentDrugs f ORDER BY f.[Number of times Dispensed] DESC";
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
                        string query = "select * from DrugsOld  where  Name='" + name + "'";
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