using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using ClinicModel;
using Dapper;

namespace ClinicApp.Data
{
    public class PatientRepository
    {
        public IEnumerable<Patient> GetAllPatients()
        {
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                        connection.Open();
                 return connection.Query<Patient>("select * from Patient");
              
            }

        }
        public Patient GetPatient(int id)
        {
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                return connection.Query<Patient>($"select * from Patient where id={id}").SingleOrDefault();

            }
        }
        public Patient GetPatient(string name)
        {
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                return connection.Query<Patient>($"select * from Patient where name={name}").SingleOrDefault();

            }
        }
        public void AddNewPatient(Patient patient)
        {
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                connection.Execute("");

            }
        }

        public IEnumerable<Consultation> PatientHistory(Patient patient)
        {
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                return connection.Query<Consultation>($"select * from Patient where id={patient}");

            }
        }
        public IEnumerable<DispensedDrug> PatientDrugHistory(Patient patient)
        {
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                return connection.Query<DispensedDrug>($"select * from Patient where id={patient.Id}");

            }

        }
    }
}