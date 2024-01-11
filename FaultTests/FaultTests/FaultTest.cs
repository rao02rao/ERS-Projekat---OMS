using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using iTextSharp.text;
using OutageManagementSystem;

namespace FaultTests.FaultTests
{
    public class FaultTests
    {
        List<FaultDescription> faultDBin = new List<FaultDescription>();
        List<FaultDescription> faultDBout = new List<FaultDescription>();

        [SetUp]
        public void Setup()
        {
            FaultDescription f1 = new FaultDescription();
            FaultDescription f2 = new FaultDescription();
            FaultDescription f3 = new FaultDescription();
            faultDBin.Add(f1);
            faultDBin.Add(f2);
            faultDBin.Add(f3);
        }

        [Test]
        [TestCase("kvar","pozar",21,"Nepotvrđen")]
        [TestCase("poplava","poplavaljeno napajanje",321,"Nepotvrđen")]
        [TestCase("pozar","kuciste izgorelo",121,"Nepotvrđen")]
       
        public void FaultConstructorWithCorrectParameters(string shortDescription, string description, int elementId, string status = "Nepotvrđen")
        {
            FaultDescription ft = new FaultDescription(shortDescription, description, elementId, status);

            Assert.AreEqual(ft.ElementId, elementId);
            Assert.AreEqual(ft.ShortDescription, shortDescription);
            Assert.AreEqual(ft.Description, description);
        }    
        
        [Test]

        public void LoadFaultsfromDB() { 
            FaultDescription fttest = new FaultDescription();
            if (faultDBin.Count != 0)
            {
                foreach (FaultDescription f in faultDBin)
                {
                    faultDBout.Add(f);
                }
                Assert.IsNotEmpty(faultDBout);
            }
        }


        public void SaveFaultsinDB()
        {
            FaultDescription ft1 = new FaultDescription();
            faultDBout.Add(ft1);
            Assert.IsNotEmpty(faultDBout);
        }
    }
}