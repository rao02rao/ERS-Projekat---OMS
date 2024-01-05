using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutageManagementSystem
{
    class UtilityClass
    {

        public static double PromptForDouble(string message)
        {
            double value;
            while (true)
            {
                Console.Write(message);
                if (double.TryParse(Console.ReadLine(), out value))
                {
                    return value;
                }
                Console.WriteLine("Unos mora biti broj. Molimo pokušajte ponovo.");
            }
        }
    }
}
