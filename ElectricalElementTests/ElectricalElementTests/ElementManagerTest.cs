using iTextSharp.text;
using Moq;
using OutageManagementSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ElectricalElementTests.ElectricalElementTests
{
    internal class ElementManagerTest
    {
        private FakeElectricalElementDB _elementsDB = new FakeElectricalElementDB(); //DB iz koje se kao ucitava

        private FakeElectricalElementDB _elements = new FakeElectricalElementDB(); //DB u koju se upisuje

        private IElectricalElement _element;


        public ElectricalElement el1 = new ElectricalElement();
        public ElectricalElement el2 = new ElectricalElement();
        public ElectricalElement el3 = new ElectricalElement();


        [SetUp]
        public void Setup()
        {
            // Mock<IElectricalElement> elementDouble = new Mock<IElectricalElement>();
            // _element= elementDouble.Object;
            _elementsDB.ElementsDB.Add(el1);
            _elementsDB.ElementsDB.Add(el2);
            _elementsDB.ElementsDB.Add(el3);
        }

        [Test]
        public void LoadElementTrueTest()
        {
            if (_elementsDB.ElementsDB.Count != 0)
                foreach (ElectricalElement el in _elementsDB.ElementsDB)
                {
                    _elements.ElementsDB.Add(el);
                }
            Assert.IsNotEmpty(_elements.ElementsDB);
        }

        [Test]
        [TestCase(12)]
        [TestCase(1422)]
        [TestCase(122)]
        public void GetElementNameByIdTrueTest(int elementId)
        {
            ElectricalElement element = new ElectricalElement(elementId, "sa", "sa", 200.0, 1000.0, "srednji nivo");
            _elements.ElementsDB.Add(element);
            foreach (ElectricalElement el in _elements.ElementsDB)
            {
                if (el.ElementId == elementId)
                {
                    string name = el.Name;
                    Assert.IsNotNull(name);
                }
            }
        }

        [Test]
        public void SaveElementTest()
        {
            ElectricalElement element = new ElectricalElement();
            _elements.ElementsDB.Add(element);
            foreach (ElectricalElement el in _elements.ElementsDB)
            {
                if (el.ElementId == element.ElementId)
                {
                    Assert.IsNotNull(el);
                }
            }
        }
    

        [Test]

        [TestCase(1)]
        [TestCase(421)]
        public void GetElementByIdTrueTest(int elementId)
        {
            foreach (ElectricalElement el in _elements.ElementsDB)
            {
                if (el.ElementId == elementId)
                {
                    Assert.IsNotNull(el);
                }
            }
        }

        [Test]

        [TestCase(1, "kompas", "zvucni", 200, 400, "srednji nivo")]
        [TestCase(122, "zica", "zicana", 2030, 4300, "srednji nivo")]
        public void AddElementTrueTest(int elementId, string name, string type, double latitude, double longitude, string voltageLevel)
        {
            ElectricalElement element = new ElectricalElement(elementId, name, type, latitude, longitude, voltageLevel);
            _elements.ElementsDB.Add(element);
            Assert.IsNotEmpty(_elements.ElementsDB);
        }

        [Test]

        [TestCase(1)]
        [TestCase(122)]
        public void CheckIfElementExistByIdTrueTest(int elementId)
        {

            foreach (ElectricalElement el in _elements.ElementsDB)
            {
                if (el.ElementId == elementId)
                {
                    Assert.Pass();
                }
            }
        }

    }
}
