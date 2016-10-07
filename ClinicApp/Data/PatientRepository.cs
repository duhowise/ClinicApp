using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
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
                return connection.Query<Patient>($"select * from Patient where Provided={id}").SingleOrDefault();

            }
        }
        public Patient GetPatient(string firstname,string lastName)
        {
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                return connection.Query<Patient>($"select * from Patient where FirstName={firstname} and LastName={lastName}" ).SingleOrDefault();

            }
        }
        public void AddNewPatient(Patient patient)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    var query = @"INSERT INTO dbo.Patient(FirstName,LastName,ProvidedId,Designation,PhoneNumber)Values(@fname,@lname,@id,@des,@phone)";
                    var command = new SqlCommand(query, connection);
                    command.Parameters.Add("fname", SqlDbType.NVarChar).Value =patient.FirstName;
                    command.Parameters.Add("lname", SqlDbType.NVarChar).Value =patient.LastName;
                    command.Parameters.Add("id", SqlDbType.NVarChar).Value =patient.ProvidedId;
                    command.Parameters.Add("des", SqlDbType.NVarChar).Value =patient.Designation;
                    command.Parameters.Add("phone", SqlDbType.NVarChar).Value =patient.PhoneNumber;
                    command.ExecuteNonQuery();

                }
            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message);
            }
        }

        public void AddNewConsultation(Consultation consultation)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    var query = @"INSERT INTO dbo.Consultation(PatientId,Temperature,BloodPressure,Complaint,Symptoms,Diagnosis,Prescription)values
                    (@Id,@temp,@blood,@comp,@symp,@diag,@pres)";
                    var command = new SqlCommand(query, connection);
                    command.Parameters.Add("Id", SqlDbType.Int).Value = consultation.PatientId;
                    command.Parameters.Add("temp", SqlDbType.NVarChar).Value = consultation.Temperature;
                    command.Parameters.Add("blood", SqlDbType.NVarChar).Value = consultation.BloodPressure;
                    command.Parameters.Add("comp", SqlDbType.NVarChar).Value = consultation.Symptoms;
                    command.Parameters.Add("symp", SqlDbType.NVarChar).Value = consultation.Signs;
                    command.Parameters.Add("diag", SqlDbType.NVarChar).Value = consultation.Diagnosis;
                    command.Parameters.Add("pres", SqlDbType.NVarChar).Value = consultation.Prescription;
                    command.ExecuteNonQuery();

                }
            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message);
            }
        }

        public Consultation PatientHistory(Patient patient)
        {
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                return connection.Query<Consultation>($"select * from Consultation where patientid={patient.Id}").SingleOrDefault();

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