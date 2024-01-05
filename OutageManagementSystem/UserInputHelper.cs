using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutageManagementSystem
{
    public static class UserInputHelper
    {
        public static string PromptForReportFormat()
        {
            while (true)
            {
                Console.WriteLine("Izaberite format izveštaja (excel, pdf, csv):");
                string format = Console.ReadLine().ToLower();

                if (format == "excel" || format == "pdf" || format == "csv")
                {
                    return format;
                }
                else
                {
                    Console.WriteLine("Nevažeći format. Molimo unesite 'excel', 'pdf' ili 'csv'.");
                }
            }
        }

        // Ovde možete dodati i druge metode za traženje unosa od korisnika
    }

}
