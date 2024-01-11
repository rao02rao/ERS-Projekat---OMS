using System;
using System.Collections.Generic;

namespace OutageManagementSystem
{
    // Interfejs koji definiše osnovne karakteristike opisa kvara.
    public interface IFaultDescription
    {
        string FaultId { get; }
        DateTime TimeOfCreation { get; }
        string Status { get; set; }
        string ShortDescription { get; set;  }
        string Description { get; set; }
        int ElementId { get;  }
        List<Action> Actions { get; set; }
        string ToXml();
    }
}
