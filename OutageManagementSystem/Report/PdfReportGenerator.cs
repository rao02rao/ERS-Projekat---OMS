using System.Collections.Generic;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Linq;

namespace OutageManagementSystem
{
    public class PdfReportGenerator : IReportGenerator
    {
        private ElementManager elementManager;

        public PdfReportGenerator(ElementManager elementManager)
        {
            this.elementManager = elementManager;
        }

        public void GenerateReport(string filePath, List<FaultDescription> faults)
        {
            using (var document = new Document())
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
    }
}
