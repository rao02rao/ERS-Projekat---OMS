using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace OutageManagementSystem
{
    public class ReportGenerator
    {
        private FaultRepository faultRepository;
        private ElementManager elementManager;  // Dodato za pristup podacima o elementima

        public ReportGenerator(FaultRepository repository, ElementManager elementManager)
        {
            faultRepository = repository;
            this.elementManager = elementManager;
        }

        public void GenerateReport(string filePath, string format)
        {
            var allFaults = faultRepository.GetAllFaults();
            switch (format.ToLower())
            {
                case "excel":
                    GenerateExcelReport(filePath, allFaults);
                    break;
                case "pdf":
                    GeneratePdfReport(filePath, allFaults);
                    break;
                default:
                    GenerateCsvReport(filePath, allFaults);
                    break;
            }
        }

        private void GenerateCsvReport(string filePath, List<FaultDescription> faults)
        {
            var csvLines = new List<string>();
            csvLines.Add("ID Kvara,Naziv Elementa,Naponski Nivo,Izvršene Akcije");

            foreach (var fault in faults)
            {
                var elementName = elementManager.GetElementNameById(fault.ElementId);
                var element = elementManager.GetElementById(fault.ElementId);
                var actions = string.Join("; ", fault.Actions.Select(a => $"{a.TimeOfAction}: {a.Description}"));
                var line = $"{fault.FaultId},{elementName},{element?.VoltageLevel ?? "N/A"},\"{actions}\"";
                csvLines.Add(line);
            }

            File.WriteAllLines(filePath, csvLines);
        }

        private void GenerateExcelReport(string filePath, List<FaultDescription> faults)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Izveštaj");
                worksheet.Cell(1, 1).Value = "ID Kvara";
                worksheet.Cell(1, 2).Value = "Naziv Elementa";
                worksheet.Cell(1, 3).Value = "Naponski Nivo";
                worksheet.Cell(1, 4).Value = "Izvršene Akcije";

                int row = 2;
                foreach (var fault in faults)
                {
                    var elementName = elementManager.GetElementNameById(fault.ElementId);
                    var element = elementManager.GetElementById(fault.ElementId); // Pretpostavljamo da postoji ova metoda
                    var actions = string.Join(", ", fault.Actions.Select(a => $"{a.TimeOfAction}: {a.Description}"));
                    worksheet.Cell(row, 1).Value = fault.FaultId;
                    worksheet.Cell(row, 2).Value = elementName;
                    worksheet.Cell(row, 3).Value = element?.VoltageLevel ?? "N/A";
                    worksheet.Cell(row, 4).Value = actions;
                    row++;
                }

                workbook.SaveAs(filePath);
            }
        }

        private void GeneratePdfReport(string filePath, List<FaultDescription> faults)
        {
            using (var document = new iTextSharp.text.Document())
            {
                PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                document.Open();

                var table = new PdfPTable(new float[] { 25f, 25f, 25f, 25f }) { WidthPercentage = 100 };
                table.AddCell("ID Kvara");
                table.AddCell("Naziv Elementa");
                table.AddCell("Naponski Nivo");
                table.AddCell("Izvršene Akcije");

                foreach (var fault in faults)
                {
                    var elementName = elementManager.GetElementNameById(fault.ElementId);
                    var element = elementManager.GetElementById(fault.ElementId);
                    var actions = string.Join("; ", fault.Actions.Select(a => $"{a.TimeOfAction}: {a.Description}"));
                    table.AddCell(fault.FaultId);
                    table.AddCell(elementName);
                    table.AddCell(element?.VoltageLevel ?? "N/A");
                    table.AddCell(actions);
                }

                document.Add(table);
                document.Close();
            }
        }

        // Dodatne metode za različite formate...
    }
}
