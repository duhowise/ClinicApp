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
                SqlConnection _connection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["ClinicConnection"].ConnectionString))
            {
                try
                {
                    if (_connection.State == ConnectionState.Closed)
                    {
                        _connection.Open();
                        string query = "select ProvidedId from Patient";
                        var command = new SqlCommand(query, _connection);
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            //patientIds.Add(
                            //   new SinglePatientId()
                            //   {
                            //       ProvidedId = reader.GetString(0)
                            //   });

                        }
                        _connection.Close();
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
