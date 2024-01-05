using System;
using System.IO;
using System.Collections.Generic; // Dodato za upotrebu List<T>
using System.Linq;
using System.Xml.Linq;



namespace OutageManagementSystem
{
    public class ElementManager
    {
        private static readonly string elementsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "ElectricalElements.csv");
        private List<ElectricalElement> elements = new List<ElectricalElement>(); // Lista za skladištenje elemenata
        public static readonly string ElementsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "ElectricalElements.csv");
        private readonly string xmlFilePath = "ElectricalElements.xml";




        public ElementManager()
        {
            LoadElements();
        }

        private void LoadElements()
        {
            if (!File.Exists(xmlFilePath))
            {
                elements = new List<ElectricalElement>();
                return;
            }

            var xml = XDocument.Load(xmlFilePath);
            elements = xml.Root.Elements("ElectricalElement")
                              .Select(x => ElectricalElement.FromXml(x.ToString()))
                              .ToList();
        }

        public void DisplayAllElements()
        {
            if (elements.Count == 0)
            {
                Console.WriteLine("Nema sačuvanih električnih elemenata.");
                return;
            }

            Console.WriteLine($"{"ID elementa",-15} {"Ime elementa",-25} {"Tip elementa",-15} {"Geo. širina",-15} {"Geo. dužina",-15} {"Naponski nivo",-15}");
            Console.WriteLine(new String('-', 100));

            foreach (var element in elements)
            {
                Console.WriteLine($"{element.ElementId,-15} {element.Name,-25} {element.Type,-15} {element.Latitude,-15} {element.Longitude,-15} {element.VoltageLevel,-15}");
            }
        }

        public void PromptAndAddElement()
        {
            int elementId = PromptForElementId();
            string name = PromptForInput("Unesite ime elementa: ");
            string type = PromptForInput("Unesite tip elementa: ");
            double latitude = UtilityClass.PromptForDouble("Unesite geografsku širinu (samo brojevi): ");
            double longitude = UtilityClass.PromptForDouble("Unesite geografsku dužinu (samo brojevi): ");
            string voltageLevel = PromptForVoltageLevel();

            AddElement(elementId, name, type, latitude, longitude, voltageLevel);
        }

        private int PromptForElementId()
        {
            while (true)
            {
                Console.Write("Unesite ID elementa (samo brojevi): ");
                if (int.TryParse(Console.ReadLine(), out int elementId) && !CheckIfElementExists(elementId))
                {
                    return elementId;
                }
                Console.WriteLine("Nevažeći ID ili ID već postoji. Molimo pokušajte ponovo.");
            }
        }

        private string PromptForInput(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        private string PromptForVoltageLevel()
        {
            Console.Write("Unesite naponski nivo (visoki napon/srednji napon/nizak napon): ");
            string voltageLevel = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(voltageLevel) ||
                !(voltageLevel.Equals("visoki napon", StringComparison.OrdinalIgnoreCase) ||
                  voltageLevel.Equals("srednji napon", StringComparison.OrdinalIgnoreCase) ||
                  voltageLevel.Equals("nizak napon", StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Nije uneta dobra vrednost za naponski nivo, postavlja se na 'srednji napon'.");
                voltageLevel = "srednji napon";
            }
            return voltageLevel;
        }

        public string GetElementNameById(int elementId)
        {
            var element = elements.FirstOrDefault(e => e.ElementId == elementId);
            return element != null ? element.Name : "Nepoznat element";
        }


        private void AddElement(int elementId, string name, string type, double latitude, double longitude, string voltageLevel)
        {
            var element = new ElectricalElement(elementId, name, type, latitude, longitude, voltageLevel);
            elements.Add(element);
            SaveElement(element);
            Console.WriteLine("Električni element uspešno unet.");
        }

        private void SaveElement(ElectricalElement element)
        {
            var xml = new XElement("ElectricalElements",
                elements.Select(e => XElement.Parse(e.ToXml())));

            var doc = new XDocument(xml);
            doc.Save(xmlFilePath);
        }

        public bool CheckIfElementExists(int elementId)
        {
            return elements.Any(e => e.ElementId == elementId);
        }

        public ElectricalElement GetElementById(int elementId)
        {
            return elements.FirstOrDefault(e => e.ElementId == elementId);
        }
    }
}
