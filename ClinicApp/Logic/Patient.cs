using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ClinicApp.Logic
{
    public class OldPatient
    {
        CMB cmb =new CMB();
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
                        string query = "select * from PatientList";
                        var dataAdapter = new SqlDataAdapter(query, connection);
                        dataAdapter.Fill(data);
                        connection.Close();
                    }
                }
                catch (Exception exception)
                {
                    cmb.Message = "exception.Message";
                    cmb.Show();
                    //MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK,MessageBoxImage.Exclamation);
                }
                return data;
            }

        }

        public int TotalRegisteredPatient()
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
                        string query = "select count(Id) from OldPatient";
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
                    cmb.Message = "exception.Message";
                    cmb.Show();
                    //MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK,MessageBoxImage.Exclamation);
                }
                return total;
            }
        }

        public DataTable RetrieveHistory(String providedId)
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
                        string query = "select Complaint,Prescription,Date from Complaint where PatientId=(select id from patient where providedid='" + providedId + "' )";
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

        public static List<string> FetchPatientById(string providedId)
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
                        string query = "select * from OldPatient  where  ProvidedId='" + providedId + "'";
                        var command = new SqlCommand(query, connection);
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            detaiList.Add(reader.GetString(1));
                            detaiList.Add(reader.GetString(2));
                            detaiList.Add(reader.GetString(3));
                            detaiList.Add(reader.GetString(4));
                            detaiList.Add(reader.GetString(5));

                        }
                        connection.Close();
                    }
                }
                catch (Exception exception)
                {
                    CMB cmb = new CMB {Message = "exception.Message"};
                    cmb.Show();
                    //MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK,MessageBoxImage.Exclamation);
                }
                return detaiList;
            }
        }

        public void AddPatientComplaint(string patientId,string complaint, string presciption)
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
                        string query = "INSERT INTO dbo.Complaint(PatientId,Complaint,Prescription)VALUES((select id from patient where providedid='"+patientId+"' ),'" + complaint + "','" + presciption + "')";
                        var command = new SqlCommand(query, connection) { CommandType = CommandType.Text };
                        command.ExecuteNonQuery();
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
    }
}