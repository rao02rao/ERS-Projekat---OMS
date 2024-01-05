using System;

namespace OutageManagementSystem
{
    public interface IAction
    {
        DateTime TimeOfAction { get; }
        string Description { get; }
        string GetActionDetails();
    }
}
