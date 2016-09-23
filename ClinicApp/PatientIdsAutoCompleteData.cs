using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClinicApp
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
           // LoadPatientIdData();
            patientIds.Add(
                              new SinglePatientId()
                              {
                                  ProvidedId = "8888"
                              });
            patientIds.Add(
                             new SinglePatientId()
                             {
                                 ProvidedId = "7788"
                             });
            patientIds.Add(
                             new SinglePatientId()
                             {
                                 ProvidedId = "9988"
                             });
            return patientIds;
        }
    }
}
