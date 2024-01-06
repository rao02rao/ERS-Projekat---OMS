using System;

namespace OutageManagementSystem
{
    // UserInterface upravlja korisničkim interfejsom aplikacije
    class UserInterface
    {
        // Definisanje potrebnih menadžera za rad sa podacima i izveštajima
        private FaultManager faultManager;
        private ElementManager elementManager;
        private ReportGenerator reportGenerator;

        // Konstruktor inicijalizuje sve potrebne komponente
        public UserInterface(FaultManager faultManager, ElementManager elementManager, ReportGenerator reportGenerator)
        {
            this.faultManager = faultManager;
            this.elementManager = elementManager;
            this.reportGenerator = reportGenerator;
        }

        // Prikaz glavnog menija i obrada korisničkih izbora
        public bool DisplayMainMenu()
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
            return ProcessUserChoice(choice);
        }

        // Privatna metoda za obradu izbora korisnika
        private bool ProcessUserChoice(string choice)
        {
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
                    reportGenerator.ExecuteReportGeneration();
                    break;
                case "6":
                    return false;
                default:
                    Console.WriteLine("Nevažeća opcija. Pokušajte ponovo.\n");
                    break;
            }
            return true;
        }
    }
}
