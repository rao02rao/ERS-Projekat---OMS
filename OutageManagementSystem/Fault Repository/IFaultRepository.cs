using System.Collections.Generic;

namespace OutageManagementSystem
{
    // Interfejs za repozitorijum kvarova
    public interface IFaultRepository
    {
        void AddFault(FaultDescription fault); //izmenjen tip sa FaultDescription na IFaultDescription
        void RemoveFault(string faultId);
        FaultDescription GetFault(string faultId); //isto
        List<FaultDescription> GetAllFaults(); //isto 
        void UpdateFault(FaultDescription updatedFault); //isto
    }
}

