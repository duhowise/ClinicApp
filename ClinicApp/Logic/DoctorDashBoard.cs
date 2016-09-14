using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace ClinicApp.Logic
{
    public class DoctorDashBoard
    {
        public int RegisteredThisMonth()
        {
            int registered = 0;
            using (
                SqlConnection connection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["ClinicConnection"].ConnectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                        string query = "select RoleId from Users where username='" + "" + "' and password='" +
                                       "" + "'";
                        var command = new SqlCommand(query, connection) {CommandType = CommandType.Text};
                        command.Parameters.AddWithValue("", "");
                        command.Parameters.AddWithValue("", "");
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            registered = reader.GetInt32(0);
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
                return registered;
            }
        }

        public int ResolvedThisMonth()
        {
            int resolved = 0;
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
                            resolved = reader.GetInt32(0);
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
                return resolved;
            }
        }

        public int PendingThisMonth()
        {
            int pending = 0;
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
                            pending = reader.GetInt32(0);
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
                return pending;
            }

        }
    }
}