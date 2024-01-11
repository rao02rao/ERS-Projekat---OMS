using DocumentFormat.OpenXml.Math;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Moq;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using OutageManagementSystem;
using System.Runtime.CompilerServices;

namespace ReportTest.ReportTests
{
    [TestFixture]
    public class ReportTest
    {
        public List<FaultDescription> faults = new List<FaultDescription>();
       
        [SetUp]
        public void Setup()
        {
            faults.Add(new FaultDescription("sd","d",2,"Nepotvrđen"));
            faults.Add(new FaultDescription("sds", "dd", 32, "Nepotvrđen"));
            faults.Add(new FaultDescription("sdss", "dddd", 42, "Nepotvrđen"));
        }

        [Test]
        public void GenerateReportTest()
        {
            // Generisanje jedinstvenog timestamp-a za naziv fajla
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            // Proveravamo i prilagođavamo format za Excel izveštaje
            string fileExtension = "pdf";

            // Kreiranje putanje fajla u istom direktorijumu gde se nalazi aplikacija
            string directoryPath = AppDomain.CurrentDomain.BaseDirectory;
            string fileName = $"Report_{timestamp}.{fileExtension}";
            string reportPath = Path.Combine(directoryPath, fileName);

            ElementManager el = new ElementManager();
            Mock<IReportGenerator> reportMock = new Mock<IReportGenerator>();
            reportMock.Setup(_report => _report.GenerateReport(reportPath, faults));
            Helper helper = new Helper(reportMock.Object);
            helper.GenerateReport(reportPath, faults);
            reportMock.Verify(_report => _report.GenerateReport(reportPath, faults), Times.Once);
        }

    }
}