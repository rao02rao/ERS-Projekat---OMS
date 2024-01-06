using System;

namespace OutageManagementSystem
{
    // Glavna ulazna tačka aplikacije
    class Program
    {
        static void Main(string[] args)
        {
            // Inicijalizacija ključnih komponenti aplikacije
            XmlFaultRepository faultRepository = new XmlFaultRepository();
            ElementManager elementManager = new ElementManager();
            FaultManager faultManager = new FaultManager(faultRepository, elementManager);
            ReportGenerator reportGenerator = new ReportGenerator(faultRepository, elementManager);
            UserInterface ui = new UserInterface(faultManager, elementManager, reportGenerator);

            // Pokretanje glavnog menija aplikacije
            bool isRunning = true;
            while (isRunning)
            {
                isRunning = ui.DisplayMainMenu();
            }
        }
    }
}
