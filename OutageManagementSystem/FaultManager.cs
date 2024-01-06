using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace OutageManagementSystem
{
    public class FaultManager
    {
        private XmlFaultRepository faultRepository;
        private ElementManager elementManager;

        public FaultManager(XmlFaultRepository repository, ElementManager elementManager)
        {
            faultRepository = repository;
            this.elementManager = elementManager;
        }

        public void PromptAndAddFault()
        {
            string shortDescription = PromptForString("Unesite kratki opis kvara: ");
            string description = PromptForString("Unesite opis kvara: ");
            int elementId = PromptForElementId();

            var fault = new FaultDescription(shortDescription, description, elementId);
            AddActionsToFault(fault);

            faultRepository.AddFault(fault);
            Console.WriteLine("Kvar uspešno unet.");
        }

        private int PromptForElementId()
        {
            int elementId;
            do
            {
                Console.Write("Unesite ID električnog elementa na kojem se kvar desio: ");
                if (!int.TryParse(Console.ReadLine(), out elementId) || !elementManager.CheckIfElementExists(elementId))
                {
                    Console.WriteLine("Nevažeći ID. Molimo unesite ID postojećeg električnog elementa.");
                }
            } while (!elementManager.CheckIfElementExists(elementId));
            return elementId;
        }

        private void AddActionsToFault(FaultDescription fault)
        {
            while (PromptForConfirmation("Da li želite da dodate akciju za ovaj kvar? (da/ne): "))
            {
                var action = PromptForAction();
                fault.Actions.Add(action);
            }
        }

        private Action PromptForAction()
        {
            var timeOfAction = PromptForDateTime("Unesite vreme akcije (format: yyyy-MM-dd HH:mm): ");
            var actionDescription = PromptForString("Unesite opis akcije: ");
            return new Action(timeOfAction, actionDescription);
        }

        private DateTime PromptForDateTime(string message)
        {
            DateTime dateTime;
            Console.Write(message);
            while (!DateTime.TryParse(Console.ReadLine(), out dateTime))
            {
                Console.WriteLine("Nevažeći format datuma. Pokušajte ponovo.");
                Console.Write(message);
            }
            return dateTime;
        }

        private string PromptForString(string message)
        {
            string input;
            do
            {
                Console.Write(message);
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Polje ne može biti prazno. Molimo unesite vrednost.");
                }
            } while (string.IsNullOrWhiteSpace(input));
            return input;
        }

        private bool PromptForConfirmation(string message)
        {
            Console.Write(message);
            return Console.ReadLine().Trim().ToLower() == "da";
        }

        public void DisplayFaultsWithOptions()
        {
            if (PromptForConfirmation("Da li želite da vidite sve kvarove? (da/ne): "))
            {
                DisplayFaults();
            }
            else
            {
                DisplayFaultsInDateRange();
            }
            AskForFaultDetails();
        }

        private void DisplayFaultsInDateRange()
        {
            DateTime startDate = PromptForDateTime("Unesite početni datum (format: yyyy-MM-dd): ");
            DateTime endDate;
            do
            {
                endDate = PromptForDateTime("Unesite krajnji datum (format: yyyy-MM-dd): ");
                if (endDate < startDate)
                {
                    Console.WriteLine("Krajnji datum ne može biti pre početnog datuma.");
                }
            } while (endDate < startDate);
            DisplayFaults(startDate, endDate);
        }

        public void DisplayFaults(DateTime? startDate = null, DateTime? endDate = null)
        {
            var allFaults = faultRepository.GetAllFaults();
            var filteredFaults = allFaults
                .Where(f => (startDate == null || f.TimeOfCreation >= startDate) && (endDate == null || f.TimeOfCreation <= endDate))
                .ToList();
            if (filteredFaults.Count == 0)
            {
                Console.WriteLine("Nema kvarova u zadatom vremenskom periodu.");
                return;
            }
            PrintFaults(filteredFaults);
        }

        private void PrintFaults(List<FaultDescription> faults)
        {
            Console.WriteLine($"{"Redni broj",-12} {"Vreme kreiranja",-25} {"Status",-15} {"Kratki opis",-30} {"Element ID",-12}");
            Console.WriteLine(new String('-', 95));
            for (int i = 0; i < faults.Count; i++)
            {
                var fault = faults[i];
                Console.WriteLine($"{i + 1,-12} {fault.TimeOfCreation,-25} {fault.Status,-15} {fault.ShortDescription,-30} {fault.ElementId,-12}");
            }
        }

        private void AskForFaultDetails()
        {
            Console.WriteLine("Unesite redni broj kvara za detalje, ili '0' za povratak: ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0)
            {
                DisplayFaultDetailsByIndex(choice - 1); // Oduzimamo 1 jer lista počinje od 0
            }
        }

        private void DisplayFaultDetailsByIndex(int index)
        {
            var allFaults = faultRepository.GetAllFaults();
            if (index >= 0 && index < allFaults.Count)
            {
                var fault = allFaults[index];
                Console.WriteLine("Detalji kvara:");
                Console.WriteLine(fault);

                // Izračunavanje i prikaz prioriteta ako je status "U popravci"
                if (fault.Status == "U popravci")
                {
                    double priority = CalculatePriority(fault);
                    Console.WriteLine($"Prioritet kvara: {priority}");
                }

                // Provjera da li je moguće modifikovati kvar
                if (fault.Status != "Zatvoreno")
                {
                    Console.WriteLine("Da li želite da modifikujete ovaj kvar? (da/ne)");
                    if (Console.ReadLine().ToLower() == "da")
                    {
                        ModifyFault(fault);
                        faultRepository.UpdateFault(fault);
                    }
                }
                else
                {
                    Console.WriteLine("Ovaj kvar je 'Zatvoren' i ne može se modifikovati.");
                }
            }
            else
            {
                Console.WriteLine("Nevažeći redni broj kvara.");
            }
        }


        private double CalculatePriority(FaultDescription fault)
        {
            double priority = 0;
            TimeSpan timeSinceCreation = DateTime.Now - fault.TimeOfCreation;
            priority += timeSinceCreation.Days;

            if (fault.Actions != null)
            {
                priority += fault.Actions.Count * 0.5;
            }

            return priority;
        }

        private void ModifyFaultIfNotClosed(FaultDescription fault)
        {
            if (fault.Status != "Zatvoreno" && PromptForConfirmation("Da li želite da modifikujete ovaj kvar? (da/ne): "))
            {
                ModifyFault(fault);
                faultRepository.UpdateFault(fault);
            }
        }

        private void ModifyFault(FaultDescription fault)
        {
            Console.WriteLine("Izmena kvara. Ostavite polje prazno ako ne želite da ga menjate.");

            // Modifikacija statusa
            string newStatus;
            do
            {
                Console.Write("Unesite novi status kvara (Nepotvrđen, U popravci, Testiranje, Zatvoreno): ");
                newStatus = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(newStatus))
                    break;
            } while (!IsValidStatus(newStatus));

            if (!string.IsNullOrWhiteSpace(newStatus))
                fault.Status = newStatus;

            // Modifikacija kratkog opisa
            Console.Write("Unesite novi kratki opis kvara: ");
            var newShortDescription = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newShortDescription))
                fault.ShortDescription = newShortDescription;

            // Modifikacija detaljnog opisa
            Console.Write("Unesite novi detaljni opis kvara: ");
            var newDescription = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newDescription))
                fault.Description = newDescription;

            // Dodavanje nove akcije
            Console.Write("Da li želite da dodate novu akciju za ovaj kvar? (da/ne): ");
            if (Console.ReadLine().ToLower() == "da")
            {
                var action = PromptForAction();
                fault.Actions.Add(action);
            }
        }

        private bool IsValidStatus(string status)
        {
            var validStatuses = new List<string> { "Nepotvrđen", "U popravci", "Testiranje", "Zatvoreno" };
            return validStatuses.Contains(status);
        }
    }
}
