using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApp
{
    class SinglePatientId
    {
        private string providedId;

        public string ProvidedId
        {
            get
            {
                return providedId;
            }

            set
            {
                if (providedId != value)
                {
                    providedId = value;
                }
            }
        }
    }
}
