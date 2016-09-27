using System.Collections.Generic;

namespace ClinicModel
{
    public class DrugForm
    {
        public int id { get; set; }
        public string name { get; set; }
        public virtual ICollection<Drug> Drugs { get; set; }
    }
}