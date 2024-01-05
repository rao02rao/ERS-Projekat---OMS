using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;


namespace OutageManagementSystem
{
    using System;
    public class ElectricalElement
    {
        public int ElementId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string VoltageLevel { get; set; } = "srednji napon";

        public ElectricalElement()
        {
            // Parameterless konstruktor potreban za XML serijalizaciju
        }

        public ElectricalElement(int elementId, string name, string type, double latitude, double longitude, string voltageLevel = "srednji napon")
        {
            ElementId = elementId;
            Name = name;
            Type = type;
            Latitude = latitude;
            Longitude = longitude;
            VoltageLevel = voltageLevel;
        }

        public string ToXml()
        {
            var serializer = new XmlSerializer(typeof(ElectricalElement));
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, this);
                return writer.ToString();
            }
        }

        public static ElectricalElement FromXml(string xml)
        {
            var serializer = new XmlSerializer(typeof(ElectricalElement));
            using (var reader = new StringReader(xml))
            {
                return (ElectricalElement)serializer.Deserialize(reader);
            }
        }

        public override string ToString()
        {
            return $"Element ID: {ElementId}\nName: {Name}\nType: {Type}\nLatitude: {Latitude}\nLongitude: {Longitude}\nNaponski nivo: {VoltageLevel}";
        }
    }

}
