using System.Collections.Generic;

namespace ClinicModel
{
    public class DrugDosageForm
    {
        public int id { get; set; }
        public string name { get; set; }
       
    }

    public class DrugDosageFormDetails : DrugDosageForm
    {
        public virtual ICollection<Drug> Drugs { get; set; }
    }
}