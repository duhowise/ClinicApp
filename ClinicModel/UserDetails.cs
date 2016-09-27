using System.Collections.Generic;

namespace ClinicModel
{
    public class UserDetails : User
    {
        public virtual Role Role { get; set; }
        public virtual ICollection<DispensedDrug> DispensedDrugs { get; set; }

    }
}