using OutageManagementSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalElementTests.ElectricalElementTests
{
    internal class FakeElectricalElementDB
    {
        private List<IElectricalElement> elementsDB = new List<IElectricalElement>();

        public List<IElectricalElement> ElementsDB { get => elementsDB; set => elementsDB = value; }
    }
}
