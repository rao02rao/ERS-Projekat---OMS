using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutageManagementSystem
{
    public class Action
    {
        public DateTime TimeOfAction { get; set; }
        public string Description { get; set; }

        public Action()
        {
            // Parameterless konstruktor potreban za XML serijalizaciju
        }


        public Action(DateTime timeOfAction, string description)
        {
            TimeOfAction = timeOfAction;
            Description = description;
        }

        public override string ToString()
        {
            return $"Vreme: {TimeOfAction}, Opis: {Description}";
        }
    }
}
