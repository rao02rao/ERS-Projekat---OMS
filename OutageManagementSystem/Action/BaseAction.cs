using System;

namespace OutageManagementSystem
{
    public abstract class BaseAction : IAction
    {
        public DateTime TimeOfAction { get; set; }
        public string Description { get; set; }

        protected BaseAction() { }

        protected BaseAction(DateTime timeOfAction, string description)
        {
            ValidateAction(timeOfAction, description);
            TimeOfAction = timeOfAction;
            Description = description;
        }

        private void ValidateAction(DateTime timeOfAction, string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Opis ne može biti prazan ili samo beli prostor.", nameof(description));

            if (timeOfAction > DateTime.Now)
                throw new ArgumentException("Vreme akcije ne može biti u budućnosti.", nameof(timeOfAction));
        }

        public abstract string GetActionDetails();
    }
}
