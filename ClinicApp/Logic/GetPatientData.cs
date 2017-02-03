using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace ClinicApp.Logic
{
    public static class GetPatientData
    {
        public static DataTable All()
        {
            var data = new DataTable();
            using (
                SqlConnection _connection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["ClinicConnection"].ConnectionString))
            {
                try
                {
                    if (_connection.State == ConnectionState.Closed)
                    {
                        _connection.Open();
                        string query = "select * from PatientList";
                        var dataAdapter = new SqlDataAdapter(query, _connection);
                        dataAdapter.Fill(data);
                        _connection.Close();
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
    }
}