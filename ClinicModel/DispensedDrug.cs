using System;

namespace ClinicModel
{
    //[ImplementPropertyChanged]
    public class DispensedDrug
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DrugId { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public int ConsultationId { get; set; }
    }
}