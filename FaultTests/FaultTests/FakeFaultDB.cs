using OutageManagementSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaultTests.FaultTests
{
    internal class FakeFaultDB : IFaultRepository
    {
        private List<IFaultDescription> _elements = new List<IFaultDescription>();

        public void AddFault(FaultDescription fault)
        {
            throw new NotImplementedException();
        }

        public void AddFaultTest(FaultDescription fault)
        {
            throw new NotImplementedException();
        }

        public List<FaultDescription> GetAllFaults()
        {
            throw new NotImplementedException();
        }

        public FaultDescription GetFault(string faultId)
        {
            throw new NotImplementedException();
        }

        public void RemoveFault(string faultId)
        {
            throw new NotImplementedException();
        }

        public void UpdateFault(FaultDescription updatedFault)
        {
            throw new NotImplementedException();
        }
    }
}
