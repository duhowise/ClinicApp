﻿using System;

namespace ClinicModel
{
    public class Consultation
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string Complaint { get; set; }
        public string Symptoms { get; set; }
        public string Diagnosis { get; set; }
        public string Prescription { get; set; }
        public DateTime Date { get; set; }
    }
}