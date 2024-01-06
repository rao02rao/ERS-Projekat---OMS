using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OutageManagementSystem
{
    public class CsvReportGenerator : IReportGenerator
    {
        private ElementManager elementManager;

        public CsvReportGenerator(ElementManager elementManager)
        {
            this.elementManager = elementManager;
        }

        public void GenerateReport(string filePath, List<FaultDescription> faults)
        {
            var csvLines = new List<string> { "ID Kvara,Naziv Elementa,Naponski Nivo,Izvršene Akcije" };

            foreach (var fault in faults)
            {
                var elementName = elementManager.GetElementNameById(fault.ElementId);
                var element = elementManager.GetElementById(fault.ElementId); // Dodajemo ovu liniju
                var actions = string.Join("; ", fault.Actions.Select(a => $"{a.TimeOfAction}: {a.Description}"));
                var line = $"{fault.FaultId},{elementName},{element?.VoltageLevel ?? "N/A"},{actions}";
                csvLines.Add(line);
            }

            File.WriteAllLines(filePath, csvLines);
        }
    }
}
