using OutageManagementSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportTest.ReportTests
{
    public class Helper
    {
        private IReportGenerator _report;
        public Helper(IReportGenerator report)
        {
            _report = report;
        }
        public void GenerateReport(string filePath, List<FaultDescription> faults)
        {
            _report.GenerateReport(filePath, faults);
        }
    }
}
