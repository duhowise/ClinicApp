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
        public IEnumerable<string> DrugAutoComplete()
        {
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == Closed)
                    connection.Open();
                return connection.Query<string>("select brandname from Drugs");
            }

        }

        public IEnumerable<DrugDosageForm> GetDosageForms()
        {
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == Closed)
                    connection.Open();
                return connection.Query<DrugDosageForm>("select * from DrugDosageForms");
            }

        }
        public IEnumerable<DrugCategory> GetDrugCategories()
        {
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == Closed)
                    connection.Open();
                return connection.Query<DrugCategory>("SELECT * FROM dbo.DrugCategory");
            }

        }
        public IEnumerable<DrugForm> GetDrugForms()
        {
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == Closed)
                    connection.Open();
                return connection.Query<DrugForm>("SELECT * FROM dbo.drugForm");
            }

        }
        public IEnumerable<DrugDosageForm> GetDrugDosageForms()
        {
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == Closed)
                    connection.Open();
                return connection.Query<DrugDosageForm>("SELECT * FROM dbo.DrugDosageForms");
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
        public IEnumerable<DispensedDrug> GetDispensedDrugs()
        {
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == Closed)
                    connection.Open();
                return connection.Query<DispensedDrug>($"select * from Drugs");
            }
        }
        public int GetRemainingDrugs(Drug drug)
        {
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == Closed)
                    connection.Open();
                return connection.Query<int>($"select dbo.RemainingDrugs('{drug.brandName}')").First();

            }
        }
        //TODO ADD SAVEDRUG QUERY
        public void SaveDrug(Drug drugs)
        {
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == Closed)
                    connection.Open();
                connection.Execute("");

            }
        }
        //TODO ADD CATEGORY QUERY
        public void AddNewDrugCategory(DrugCategory drugCategory)
        {
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == Closed)
                    connection.Open();
                connection.Execute("");

            }
        }
       //TODO ADD CATEGORYTYPE QUERY
        public void AddNewDrugType(DrugForm drugForm)
        {
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == Closed)
                    connection.Open();
                connection.Execute("");

            }
        }

        public  IEnumerable<string> FetchByBrandName(string brandname)
        {
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == Closed)
                    connection.Open();
                return connection.Query<string>($"select * from Drugs  where  brandName={brandname}");
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