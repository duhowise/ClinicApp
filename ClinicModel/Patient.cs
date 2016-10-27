using System;
using System.Collections.Generic;

namespace ClinicModel
{
    public class Patient
    {

        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProvidedId { get; set; }
        public string Designation { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Date { get; set; }
        public string Gender { get; set; }
        public string FulName()
        {
            return FirstName + " " + LastName;
        }public string FulNameR()
        {
            return LastName + " " + FirstName;
        }
    }
}