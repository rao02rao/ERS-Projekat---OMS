using System;
using System.Collections.Generic;

namespace OutageManagementSystem
{
    // Interfejs koji definiše osnovne karakteristike opisa kvara.
    public interface IFaultDescription
    {
        string FaultId { get; }
        DateTime TimeOfCreation { get; }
        string Status { get; }
        string ShortDescription { get; }
        string Description { get; }
        int ElementId { get; }
        List<Action> Actions { get; }
        string ToXml();
    }
}
