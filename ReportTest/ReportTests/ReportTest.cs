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
            ElementManager el = new ElementManager();
            //List<FaultDescription> faults = new List<FaultDescription>();
            string filePath = "C:\\Users\\Djera\\Desktop\\V7 - Unit testing (2)\\MockTests\\izvestaj.pdf";
            Mock<IReportGenerator> reportMock = new Mock<IReportGenerator>();
            reportMock.Setup(_report => _report.GenerateReport(filePath, faults));
            PdfReportGenerator pdf= new PdfReportGenerator(el);
            pdf.GenerateReport(filePath, faults); 
            reportMock.Verify(_report => _report.GenerateReport(filePath, faults), Times.Once);
        }

    }
}