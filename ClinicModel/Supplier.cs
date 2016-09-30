using System.Collections.Generic;

namespace ClinicModel
{
    public class Supplier
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string CompanyName { get; set; }
        public virtual ICollection<Drug> Drugs { get; set; }
    }
}