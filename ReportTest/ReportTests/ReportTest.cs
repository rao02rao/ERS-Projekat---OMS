using DocumentFormat.OpenXml.Math;
using Moq;
using NUnit.Framework.Internal;
using OutageManagementSystem;

namespace ReportTest.ReportTests
{
    [TestFixture]
    public class ReportTest
    {

        [Test]
        [TestCase("poruka")]
        public void GenerateReportTest(string poruka)
        {
            List<FaultDescription> faults = new List<FaultDescription>();
            string filePath = "faults";
            Mock<IReportGenerator> reportMock = new Mock<IReportGenerator>();
            reportMock.Setup(_report => _report.GenerateReport(filePath, faults));
            //Helper helper = new Helper(loggerMock.Object);
            //helper.WriteToFile(message);
            //loggerMock.Verify(_logger => _logger.Info(message), Times.Once);
        }

    }
}