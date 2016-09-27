using System.Collections.Generic;

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
        public System.DateTime Date { get; set; }

        public virtual ICollection<Consultation> Consultations { get; set; }
        public virtual ICollection<DispensedDrug> DispensedDrugs { get; set; }
    }
}