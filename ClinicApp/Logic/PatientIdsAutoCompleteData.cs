using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace ClinicApp.Logic
{
    class PatientIdsAutoCompleteData
    {
        private ObservableCollection<SinglePatientId> patientIds = new ObservableCollection<SinglePatientId>();

        public void LoadPatientIdData()
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
                        string query = "select ProvidedId from Patient";
                        var command = new SqlCommand(query, connection);
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            //patientIds.Add(
                            //   new SinglePatientId()
                            //   {
                            //       ProvidedId = reader.GetString(0)
                            //   });

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
        public ObservableCollection<SinglePatientId> GetPatientIdAutoCompleteData()
        {
            patientIds.Clear();
           LoadPatientIdData();
            return patientIds;
        }
    }
}
