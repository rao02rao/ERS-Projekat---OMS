using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;

namespace OutageManagementSystem
{

    public class FaultRepository
    {
        private List<FaultDescription> faults;
        private readonly string xmlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Faults.xml");


        public FaultRepository()
        {
            if (!File.Exists(xmlFilePath))
            {
                // Kreira prazan XML dokument sa korenom "Faults" ako fajl ne postoji
                new XDocument(new XElement("Faults")).Save(xmlFilePath);
            }
            LoadFaultsFromXml();
        }


        private void LoadFaultsFromXml()
        {
            if (!File.Exists(xmlFilePath))
            {
                faults = new List<FaultDescription>();
                return;
            }

            var xml = XDocument.Load(xmlFilePath);
            faults = xml.Root.Elements("FaultDescription")
                            .Select(x => FaultDescription.FromXml(x.ToString()))
                            .ToList();
        }

        public void SaveFaultsToXml()
        {
            var xml = new XElement("Faults",
                faults.Select(f => XElement.Parse(f.ToXml())));

            var doc = new XDocument(xml);
            doc.Save(xmlFilePath);
        }

        public void AddFault(FaultDescription fault)
        {
            faults.Add(fault);
            SaveFaultsToXml();
        }

        public void RemoveFault(string faultId)  // Promenjen tip parametra sa int na string
        {
            faults.RemoveAll(f => f.FaultId == faultId);
        }

        public FaultDescription GetFault(string faultId)
        {
            // Prolazi kroz listu 'faults' i traži kvar sa zadatim 'faultId'
            return faults.Find(f => f.FaultId == faultId);
        }


        public List<FaultDescription> GetAllFaults()
        {
            return faults;
        }

        public void UpdateFault(FaultDescription updatedFault)
        {
            for (int i = 0; i < faults.Count; i++)
            {
                if (faults[i].FaultId == updatedFault.FaultId)
                {
                    faults[i] = updatedFault;
                    SaveFaultsToXml();
                    break;
                }
            }
        }
    }


}
