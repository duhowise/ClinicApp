using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using ClinicModel;
using Dapper;
using static System.Data.ConnectionState;

namespace ClinicApp.Data
{
    public class DrugRepository
    {
        public IEnumerable<Drug> GetAllDrugs()
        {
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == Closed)
                    connection.Open();
                return connection.Query<Drug>("select * from Drugs");
            }

        }
        public Drug GetDrugById(int id)
        {
            using (var connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == Closed)
                    connection.Open();
                return connection.Query<Drug>($"select * from Drugs where id={id}").SingleOrDefault();
            }
        }
        public Drug GetDrugByName(string name)
        {

            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == Closed)
                    connection.Open();
                return connection.Query<Drug>($"select * from Drugs where brandName={name}").SingleOrDefault();

            }
        }
        public Drug GetDispensedDrugs(string name)
        {
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == Closed)
                    connection.Open();
                return connection.Query<Drug>($"select * from Drugs where brandName={name}").SingleOrDefault();
            }
        }
        public int GetRemainingDrugs(Drug drugses)
        {
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == Closed)
                    connection.Open();
                return connection.Query<int>($"select dbo.RemainingDrugs('{drugses.brandName}')").First();

            }
        }
        public void SaveDrug(Drug drugs)
        {
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == Closed)
                    connection.Open();
                connection.Execute("");

            }
        }

        public static IEnumerable FetchByName(string name)
        {
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == Closed)
                    connection.Open();
                return connection.Query<string>($"select * from Drugs  where  brandName={name}");
            }
        }
        public int TotalDrugsQuantity(string tableName)
        {
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == Closed)
                    connection.Open();
                return connection.Query<int>($"SELECT SUM(Quantity) AS TotalDrugsQuantity FROM {tableName}").First();

            }
        }
    }
}