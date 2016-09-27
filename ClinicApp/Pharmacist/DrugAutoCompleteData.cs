using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using ClinicApp.Logic;

namespace ClinicApp.Pharmacist
{
    public class DrugAutoCompleteData
    {
         private ObservableCollection<SingleDrugData> drugs = new ObservableCollection<SingleDrugData>();

        public void LoadDrugsData()
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
                        string query = "select Name from Drugs";
                        var command = new SqlCommand(query, connection);
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            drugs.Add(
                                new SingleDrugData()
                                {
                                    DrugName = reader.GetString(0)
                                });

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

        public ObservableCollection<SingleDrugData> GetDrugAutoCompleteData()
        {
            drugs.Clear();
            LoadDrugsData();
            return drugs;
        } 

    }
}