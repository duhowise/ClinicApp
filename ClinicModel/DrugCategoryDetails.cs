using System.Collections.Generic;

namespace ClinicModel
{
    public class DrugCategoryDetails : DrugCategory
    {
        public virtual ICollection<Drug> Drugs { get; set; }
    }
}