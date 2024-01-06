using System.Collections.Generic;
using ClosedXML.Excel;
using System.Linq;

namespace OutageManagementSystem
{
    public class ExcelReportGenerator : IReportGenerator
    {
        private ElementManager elementManager;

        public ExcelReportGenerator(ElementManager elementManager)
        {
            this.elementManager = elementManager;
        }

        public void GenerateReport(string filePath, List<FaultDescription> faults)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Izveštaj");
                worksheet.Cell("A1").Value = "ID Kvara";
                worksheet.Cell("B1").Value = "Naziv Elementa";
                worksheet.Cell("C1").Value = "Naponski Nivo";
                worksheet.Cell("D1").Value = "Izvršene Akcije";

                int row = 2;
                foreach (var fault in faults)
                {
                    var elementName = elementManager.GetElementNameById(fault.ElementId);
                    var element = elementManager.GetElementById(fault.ElementId); 
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
    }
}
