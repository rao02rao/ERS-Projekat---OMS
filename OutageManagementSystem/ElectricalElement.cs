using System;
using System.IO;
using System.Xml.Serialization;

namespace OutageManagementSystem
{
    public class ElectricalElement : IElectricalElement
    {
        // Javna svojstva električnog elementa
        public int ElementId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string VoltageLevel { get; set; }

        // Parameterless konstruktor
        public ElectricalElement()
        {
            VoltageLevel = "srednji napon";
        }

        // Konstruktor sa parametrima
        public ElectricalElement(int elementId, string name, string type, double latitude, double longitude, string voltageLevel = "srednji napon")
        {
            SetElementId(elementId);
            SetName(name);
            SetType(type);
            SetLatitude(latitude);
            SetLongitude(longitude);
            SetVoltageLevel(voltageLevel);
        }

        private void SetElementId(int elementId)
        {
            if (elementId <= 0)
                throw new ArgumentException("ID mora biti veći od 0.", nameof(elementId));
            ElementId = elementId;
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Ime ne može biti prazno.", nameof(name));
            Name = name;
        }

        private void SetType(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("Tip ne može biti prazan.", nameof(type));
            Type = type;
        }

        private void SetLatitude(double latitude)
        {
            Latitude = latitude;
        }

        private void SetLongitude(double longitude)
        {
            Longitude = longitude;
        }

        private void SetVoltageLevel(string voltageLevel)
        {
            if (string.IsNullOrWhiteSpace(voltageLevel))
                throw new ArgumentException("Naponski nivo ne može biti prazan.", nameof(voltageLevel));
            VoltageLevel = voltageLevel;
        }

        // Metoda za serijalizaciju u XML
        public string ToXml()
        {
            var serializer = new XmlSerializer(typeof(ElectricalElement));
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, this);
                return writer.ToString();
            }
        }

        // Metoda za deserijalizaciju iz XML
        public static ElectricalElement FromXml(string xml)
        {
            var serializer = new XmlSerializer(typeof(ElectricalElement));
            using (var reader = new StringReader(xml))
            {
                return (ElectricalElement)serializer.Deserialize(reader);
            }
        }

        // ToString metoda za prikaz informacija o elementu
        public override string ToString()
        {
            return $"Element ID: {ElementId}\nName: {Name}\nType: {Type}\nLatitude: {Latitude}\nLongitude: {Longitude}\nNaponski nivo: {VoltageLevel}";
        }
    }
}
