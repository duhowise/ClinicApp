using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace ClinicApp.Logic
{
    public static class Clinic
    {
        public static int VerifyUser(string username, string password)
        { 
            CurrentUserLoggedInData userData=new CurrentUserLoggedInData();
            int id = 0;
            try
            {
                using (
                    SqlConnection connection =
                        new SqlConnection(ConfigurationManager.ConnectionStrings["ClinicConnection"].ConnectionString))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                        string query = "select * from Users where username='" + @username + "' and password='" +
                                       @password + "'";
                        var command = new SqlCommand(query, connection) {CommandType = CommandType.Text};
                        command.Parameters.AddWithValue(@username, username);
                        command.Parameters.AddWithValue(@password, password);
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            id++;
                            CurrentUserLoggedInData.Id = reader.GetInt32(0);
                            CurrentUserLoggedInData.FirstName = reader.GetString(1);
                            CurrentUserLoggedInData.LastName = reader.GetString(2);
                            CurrentUserLoggedInData.UserName = reader.GetString(3);
                            CurrentUserLoggedInData.Role = reader.GetInt32(5);
                        }
                        reader.Close();
                        connection.Close();
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            return id;
        }

        public static int USerType(string username, string password)
        {
            int usertype = 0;
            using (
                SqlConnection connection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["ClinicConnection"].ConnectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                        string query = "select RoleId from Users where username='" + @username + "' and password='" +
                                       @password + "'";
                        var command = new SqlCommand(query, connection) {CommandType = CommandType.Text};
                        command.Parameters.AddWithValue(@username, username);
                        command.Parameters.AddWithValue(@password, password);
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            usertype = reader.GetInt32(0);
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

            return usertype;
        }

        public static void AddPatient(string firstname, string lastName, string providedId, string designation, string phoneNumber)
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
                        string query =
                            "INSERT INTO dbo.Patient(FirstName , LastName , ProvidedId, Designation, PhoneNumber)values('" +
                            @firstname + "','" + @lastName + "','" + @providedId + "','" + @designation + "','" +
                            @phoneNumber + "')";
                        var command = new SqlCommand(query, connection) {CommandType = CommandType.Text};
                        command.Parameters.AddWithValue(@firstname, firstname);
                        command.Parameters.AddWithValue(@lastName, lastName);
                        command.Parameters.AddWithValue(@providedId, providedId);
                        command.Parameters.AddWithValue(@designation, designation);
                        command.Parameters.AddWithValue(@phoneNumber, phoneNumber);
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

        public static DataTable PatientDetails()
        {
            DataTable details = null;
            return details;
        }

        public static string Username(string username, string password)
        {
            string name = null;

            using (
                SqlConnection connection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["ClinicConnection"].ConnectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                        string query = "select firstname,lastname from Users where username='" + @username +
                                       "' and password='" + @password + "'";
                        var command = new SqlCommand(query, connection) {CommandType = CommandType.Text};
                        command.Parameters.AddWithValue(@username, username);
                        command.Parameters.AddWithValue(@password, password);
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            name = reader.GetString(0) + " " + reader.GetString(1);


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
                return name;
            }

        }
    }
}