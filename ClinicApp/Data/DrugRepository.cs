using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
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

        public IEnumerable<Packaging> GetAllDrugPackaging()
        {
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == Closed)
                    connection.Open();
                return connection.Query<Packaging>("select * from packaging");
            }

        }
        public IEnumerable<string> DrugAutoComplete()
        {
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == Closed)
                    connection.Open();
                return connection.Query<string>("select brandName from Drugs");
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
                return connection.Query<Drug>($"select * from Drugs where brandName='{name}'").SingleOrDefault();

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
                return connection.Query<int>($"select dbo.RemainingDrugs('{drug.BrandName}')").First();

            }
        }
        public void SaveDrug(Drug drugs)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
                {
                    if (connection.State == Closed)
                        connection.Open();
                    connection.Query(@"INSERT INTO dbo.Drugs(GenericName,brandName,Box,NumberPackInBox,Quantity,ExpiryDate,NumberinPack,
                    DosageFormId,DrugFormId,CategoryId,PackagingId,SupplierId)Values(@GenericName,@brandName,@Box,@NumberPackInBox,@Quantity,
                    @ExpiryDate,@NumberinPack,@DosageFormId,@DrugFormId,@CategoryId,@PackagingId,@SupplierId)", drugs);
                }
                
            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message);
            }
        }
         public void AddNewDrugCategory(DrugCategory drugCategory)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
                {
                    if (connection.State == Closed)
                        connection.Open();
                    var query = @"INSERT INTO dbo.DrugCategory(name)VALUES(@name)";
                    var command = new SqlCommand(query, connection);
                    command.Parameters.Add("name", SqlDbType.NVarChar).Value = drugCategory.name;
                    command.ExecuteNonQuery();

                }
            }
            catch (Exception exception)
            {
               MessageBox.Show(exception.Message);
            }
        }
        public void AddNewDrugType(DrugForm drugForm)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
                {
                    if (connection.State == Closed)
                        connection.Open();
                    var query = @"INSERT INTO dbo.drugForm(name)VALUES(@name)";
                    var command = new SqlCommand(query, connection);
                    command.Parameters.Add("name", SqlDbType.NVarChar).Value = drugForm.name;
                    command.ExecuteNonQuery();

                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
        public void AddNewDrugDosage(DrugDosageForm drugForm)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
                {
                    if (connection.State == Closed)
                        connection.Open();
                    var query = @"INSERT INTO dbo.DrugDosageForms(name)VALUES(@name)";
                    var command = new SqlCommand(query, connection);
                    command.Parameters.Add("name", SqlDbType.NVarChar).Value = drugForm.name;
                    command.ExecuteNonQuery();

                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
        public  IEnumerable<string> FetchByBrandName(string brandname)
        {
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == Closed)
                    connection.Open();
                return connection.Query<string>($"select * from Drugs  where  brandName=@brandName",brandname);
            }
        }

        public void DispenseDrug(DispensedDrug dispensedDrug)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
                {
                    if (connection.State == Closed)
                        connection.Open();
                    connection.Query(@"INSERT INTO dbo.DispensedDrugs(PatientId,DrugId,Quantity,UserId)Values(@PatientId,@DrugId,@Quantity,@UserId)",dispensedDrug);
                }

            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message);
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
        public void UpdateDrug(Drug drug)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
                {
                    if (connection.State == Closed)
                        connection.Open();
                    connection.Query(@"UPDATE dbo.Drugs SET  GenericName =@GenericName,brandName = @brandName,Box = @Box,NumberPackInBox = @NumberPackInBox
                    ,Quantity = @Quantity,ExpiryDate = @ExpiryDate,NumberinPack = @NumberinPack,DosageFormId = @DosageFormId,DrugFormId = @DrugFormId,
                        CategoryId = @CategoryId,PackagingId = @PackagingId,SupplierId = @SupplierId WHERE Id = @Id",drug);

                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }

   
}