using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml.Serialization;
using System.IO;

namespace OutageManagementSystem
{
    public class FaultDescription
    {
        public string FaultId { get; set; }
        public DateTime TimeOfCreation { get; set; }
        public string Status { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public int ElementId { get; set; }
        public List<Action> Actions { get; set; }

        private static int dailyFaultCount = 0;
        private static DateTime lastFaultDate = DateTime.MinValue;

        public FaultDescription()
        {
            // Parameterless konstruktor potreban za XML serijalizaciju
        }

        public FaultDescription(string shortDescription, string description, int elementId, string status = "Nepotvrđen")
        {
            TimeOfCreation = DateTime.Now;

            if (lastFaultDate.Date != TimeOfCreation.Date)
            {
                dailyFaultCount = 0;
                lastFaultDate = TimeOfCreation;
            }

            dailyFaultCount++;
            FaultId = $"{TimeOfCreation:yyyyMMddHHmmss}_{dailyFaultCount}";
            ShortDescription = shortDescription;
            Description = description;
            ElementId = elementId;
            Status = status;
            Actions = new List<Action>();
        }

        public string ToXml()
        {
            var serializer = new XmlSerializer(typeof(FaultDescription));
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, this);
                return writer.ToString();
            }
        }

        public static FaultDescription FromXml(string xml)
        {
            var serializer = new XmlSerializer(typeof(FaultDescription));
            using (var reader = new StringReader(xml))
            {
                return (FaultDescription)serializer.Deserialize(reader);
            }
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Fault ID: {FaultId}");
            stringBuilder.AppendLine($"Element ID: {ElementId}");
            stringBuilder.AppendLine($"Created On: {TimeOfCreation}");
            stringBuilder.AppendLine($"Status: {Status}");
            stringBuilder.AppendLine($"Short Description: {ShortDescription}");
            stringBuilder.AppendLine($"Description: {Description}");

            if (Actions.Count > 0)
            {
                stringBuilder.AppendLine("Actions:");
                foreach (var action in Actions.OrderByDescending(a => a.TimeOfAction)) // Uverite se da je ovaj red ispravan
                {
                    stringBuilder.AppendLine($"- Time: {action.TimeOfAction}, Description: {action.Description}");
                }
            }
            else
            {
                stringBuilder.AppendLine("Nema akcija");
            }

            return stringBuilder.ToString();
        }
    }
}
