using System;

namespace ClinicModel
{
    public class Consultation
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string Temperature { get; set; }
        public string Pulse { get; set; }
        public string Weight { get; set; }
        public string Respiration { get; set; }
        ///public int  ConditionCatId { get; set; }
        public string BloodPressure { get; set; }
        public string Symptoms { get; set; }
        public string Signs { get; set; }
        public string Diagnosis { get; set; }
        public short IsSensitive { get; set; }
        public string Prescription { get; set; }
        public string Investigation { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }


    }

}