using Irony.Parsing;
using OutageManagementSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaultTests.FaultTests
{
    internal class FakeFaultDB //: IFaultRepository
    {
        private List<FaultDescription> _faults = new List<FaultDescription>();

        public List<FaultDescription> Faults { get => _faults; set => _faults = value; }

        public void AddFault(FaultDescription fault)
        {
            Faults.Add(fault);
        }

        public List<FaultDescription> GetAllFaults()
        {
            List<FaultDescription> fakeFaults = new List<FaultDescription>();
            foreach (FaultDescription fault in Faults)
            {
                fakeFaults.Add(fault);
            }
            return fakeFaults;
        }

        public FaultDescription GetFault(string faultId)
        {
            foreach (FaultDescription fault in Faults)
            {
                if(fault.FaultId == faultId)
                {
                    return fault;
                }
            }
            return null;
        }

        public void RemoveFault(string faultId)
        {
            foreach(FaultDescription fault in Faults)
            {
                if(fault.FaultId == faultId)
                Faults.Remove(fault);
            }
        }

        public void UpdateFault(FaultDescription updatedFault)
        {
            foreach(FaultDescription f in Faults)
            {
                if(f.FaultId == updatedFault.FaultId)
                {
                    f.ShortDescription = updatedFault.ShortDescription;
                    f.Description = updatedFault.Description;
                    f.Status = updatedFault.Status;  
                }
            }
        }
    }
}
