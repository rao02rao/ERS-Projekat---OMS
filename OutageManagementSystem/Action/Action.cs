using System;

namespace OutageManagementSystem
{
    public class Action : BaseAction
    {
        // Parameterless konstruktor potreban za serijalizaciju
        public Action() { }

        public Action(DateTime timeOfAction, string description) : base(timeOfAction, description) { }

        public override string GetActionDetails()
        {
            return $"Akcija: Vreme - {TimeOfAction}, Opis - {Description}";
        }

        public override string ToString()
        {
            return GetActionDetails();
        }
    }
}
