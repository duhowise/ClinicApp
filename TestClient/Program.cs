using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicApp.Data;

namespace TestClient
{
    class Program
    {

        static void Main(string[] args)
        {
            var data=new PatientRepository().GetAllPatients();
            foreach (var sickperson in data)
            {
                Console.WriteLine(sickperson.ProvidedId);
                Console.WriteLine($"{sickperson.FirstName} {sickperson.LastName}");
                Console.WriteLine(sickperson.Designation);
                Console.WriteLine(sickperson.PhoneNumber);
                Console.WriteLine(sickperson.LastName);
                Console.WriteLine(@"**********************************************");
            }

            //Console.WriteLine(data.brandName);
        }
    }
}
