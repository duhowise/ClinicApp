﻿using System.Collections.Generic;

namespace ClinicModel
{
    public class DrugForm
    {
        public int Id { get; set; }
        public string name { get; set; }
        public virtual ICollection<Drug> Drugs { get; set; }
    }
}