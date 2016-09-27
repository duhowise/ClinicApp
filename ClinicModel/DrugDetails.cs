using System.Collections.Generic;

namespace ClinicModel
{
    public class DrugDetails : Drug
    {
        public virtual ICollection<DispensedDrug> DispensedDrugs { get; set; }
        public virtual DrugCategory DrugCategory { get; set; }
        public virtual DrugDosageForm DrugDosageForm { get; set; }
        public virtual DrugForm drugForm { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}