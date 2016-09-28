using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ClinicModel
{
    public class Patient
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProvidedId { get; set; }
        public string Designation { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Date { get; set; }

        public  IEnumerable<Consultation> Consultations { get; set; }
        public  IEnumerable<DispensedDrug> DispensedDrugs { get; set; }

        //public Patient()
        //{
        //    Consultations = PatientHistory(this);
        //    DispensedDrugs = PatientDrugHistory(this);
        //}
        //private IEnumerable<Consultation> PatientHistory(Patient patient)
        //{
        //    //using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
        //    //{
        //    //    if (connection.State == ConnectionState.Closed)
        //    //        connection.Open();
        //    //    return connection.Query<DispensedDrug>($"select * from Patient where id={patient}");

        //    //}
        //    return new List<DispensedDrug>();
        //}
        //private IEnumerable<DispensedDrug> PatientDrugHistory(Patient patient)
        //{
        //    //using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
        //    //{
        //    //    if (connection.State == ConnectionState.Closed)
        //    //        connection.Open();
        //    //    return connection.Query<Consultation>($"select * from Patient where id={patient}");

        //    //}
        //    return new List<Consultation>();
        //}
    }
}