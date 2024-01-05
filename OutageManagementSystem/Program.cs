using System;
using System.IO;

namespace OutageManagementSystem
{
    class Program
    {
        static XmlFaultRepository faultRepository = new XmlFaultRepository();
        static ElementManager elementManager = new ElementManager();
        static FaultManager faultManager = new FaultManager(faultRepository, elementManager);
        static ReportGenerator reportGenerator = new ReportGenerator(faultRepository, elementManager);


        static void Main(string[] args)
        {
            Console.WriteLine("Outage Management System (OMS)\n");

            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("\nGlavni meni:\n");
                Console.WriteLine("1. Unesite novi kvar");
                Console.WriteLine("2. Unesite novi električni element");
                Console.WriteLine("3. Prikazite kvarove");
                Console.WriteLine("4. Prikazite sve električne elemente");
                Console.WriteLine("5. Generišite izveštaj");
                Console.WriteLine("6. Izlaz");
                Console.Write("Izaberite opciju: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        faultManager.PromptAndAddFault();
                        break;
                    case "2":
                        elementManager.PromptAndAddElement();
                        break;
                    case "3":
                        faultManager.DisplayFaultsWithOptions();
                        break;
                    case "4":
                        elementManager.DisplayAllElements();
                        break;
                    case "5":
                        string format = UserInputHelper.PromptForReportFormat();
                        string reportPath = ElementManager.ElementsFilePath.Replace("ElectricalElements.csv", $"Report.{format}");
                        try
                        {
                            reportGenerator.GenerateReport(reportPath, format);

                            if (File.Exists(reportPath))
                            {
                                Console.WriteLine("Izveštaj je uspešno generisan.");
                            }
                            else
                            {
                                Console.WriteLine("Greška: Izveštaj nije generisan.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Došlo je do greške prilikom generisanja izveštaja: " + ex.Message);
                        }
                        break;

                    case "6":
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Nevažeća opcija. Pokušajte ponovo.\n");
                        break;
                }
            }
        }
    }
}
