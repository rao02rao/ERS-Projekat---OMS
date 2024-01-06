using System.Collections.Generic;

namespace OutageManagementSystem
{
    public interface IReportGenerator
    {
        void GenerateReport(string filePath, List<FaultDescription> faults);
    }
}
