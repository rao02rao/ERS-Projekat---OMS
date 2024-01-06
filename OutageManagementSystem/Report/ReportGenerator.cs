using System;
using System.Collections.Generic;
using System.IO;

namespace OutageManagementSystem
{
    public class ReportGenerator
    {
        private XmlFaultRepository faultRepository;
        private ElementManager elementManager;
        private Dictionary<string, IReportGenerator> reportGenerators;

        public ReportGenerator(XmlFaultRepository repository, ElementManager elementManager)
        {
            this.faultRepository = repository;
            this.elementManager = elementManager;
            this.reportGenerators = new Dictionary<string, IReportGenerator>
            {
                { "csv", new CsvReportGenerator(elementManager) },
                { "excel", new ExcelReportGenerator(elementManager) },
                { "pdf", new PdfReportGenerator(elementManager) }
            };
        }

        public void GenerateReport(string filePath, string format)
        {
            var allFaults = faultRepository.GetAllFaults();
            if (reportGenerators.TryGetValue(format.ToLower(), out var generator))
            {
                generator.GenerateReport(filePath, allFaults);
            }
            else
            {
                throw new InvalidOperationException("Nepodržan format izveštaja");
            }
        }

        public void ExecuteReportGeneration()
        {
            // Traženje korisnika da izabere format izveštaja
            Console.WriteLine("Izaberite format izveštaja (csv, excel, pdf):");
            string format = Console.ReadLine();

            // Generisanje putanje za izveštaj
            string reportPath = GenerateReportPath(format);

            // Provera da li je odabrani format podržan i generisanje izveštaja
            try
            {
                if (reportGenerators.ContainsKey(format.ToLower()))
                {
                    Console.WriteLine("Generišem izveštaj...");
                    GenerateReport(reportPath, format);
                    Console.WriteLine($"Izveštaj je generisan i sačuvan na lokaciji: {reportPath}");
                }
                else
                {
                    Console.WriteLine("Nepodržan format izveštaja.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Došlo je do greške prilikom generisanja izveštaja: {ex.Message}");
            }
        }

        private string GenerateReportPath(string format)
        {
            // Generisanje jedinstvenog timestamp-a za naziv fajla
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            // Proveravamo i prilagođavamo format za Excel izveštaje
            string fileExtension = format.ToLower() == "excel" ? "xlsx" : format;

            // Kreiranje putanje fajla u istom direktorijumu gde se nalazi aplikacija
            string directoryPath = AppDomain.CurrentDomain.BaseDirectory;
            string fileName = $"Report_{timestamp}.{fileExtension}";
            return Path.Combine(directoryPath, fileName);
        }
    }
}
