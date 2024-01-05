using System;
using System.IO;

namespace OutageManagementSystem
{
    public class FileManager
    {
        public void GenerateReport(string filePath, FaultRepository faultRepository)
        {
            var allFaults = faultRepository.GetAllFaults();
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.WriteLine("Fault ID,Time of Creation,Status,Description");
                foreach (var fault in allFaults)
                {
                    sw.WriteLine($"{fault.FaultId},{fault.TimeOfCreation},{fault.Status},{fault.Description}");
                }
            }
            Console.WriteLine("Izveštaj generisan na: " + filePath);
        }
    }
}

