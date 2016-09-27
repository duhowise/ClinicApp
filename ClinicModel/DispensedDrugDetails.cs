using System.Collections.Generic;

namespace ClinicModel
{
    public class DispensedDrugDetails : DispensedDrug
    {
        public virtual ICollection<Drug> Drugs { get; set; }
    }
}