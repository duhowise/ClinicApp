using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ClinicModel;
using Dapper;

namespace ClinicApp.Data
{
    public class PatientRepository
    {
        public IEnumerable<ClinicModel.Patient> GetAllPatients()
        {
            IEnumerable<ClinicModel.Patient> patients = new List<ClinicModel.Patient>();
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                    patients = connection.Query<ClinicModel.Patient>("select * from Patient");
                    connection.Close();
                }
            }

            return (List<ClinicModel.Patient>)patients;
        }
        public Patient GetPatient(int id)
        {
            return GetPatient(id);
        }
        public Patient GetPatient(string name)
        {
            return GetPatient(name);
        }


        public void AddNewPatient(Patient patient)
        {

        }
    }
}