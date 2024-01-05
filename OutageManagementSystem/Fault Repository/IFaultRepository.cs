using System.Collections.Generic;

namespace OutageManagementSystem
{
    // Interfejs za repozitorijum kvarova
    public interface IFaultRepository
    {
        void AddFault(FaultDescription fault);
        void RemoveFault(string faultId);
        FaultDescription GetFault(string faultId);
        List<FaultDescription> GetAllFaults();
        void UpdateFault(FaultDescription updatedFault);
    }
}

