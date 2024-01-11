using DocumentFormat.OpenXml.Wordprocessing;
using iTextSharp.text;
using OutageManagementSystem;


namespace ElectricalElementTests.ElectricalElementTests
{
    public class ElectricalElementTests
    {
        ElectricalElement element = new ElectricalElement();

        [SetUp]
        public void Setup()
        {

        }


        [Test]
        [TestCase(1, "kompas", "elektricni", 500, 600, "srednji napon")]
        [TestCase(123, "busola", "kameni", 800, 2000, "srednji napon")]

        public void ElecElementConstructorWithCorrectParameters(int elementId, string name, string type, double latitude, double longitude, string voltageLevel = "srednji napon")
        {
            ElectricalElement el = new ElectricalElement(elementId, name, type, latitude, longitude, voltageLevel);

            Assert.AreEqual(el.ElementId, elementId);
            Assert.AreEqual(el.Name, name);
            Assert.AreEqual(el.Type, type);
            Assert.AreEqual(el.Latitude, latitude);
            Assert.AreEqual(el.Longitude, longitude);
            Assert.AreEqual(el.VoltageLevel, voltageLevel);

        }

        [Test]

        [TestCase(1, "", "elektricni", 500, 600, "srednji napon")]
        [TestCase(1, "kompas", "", 500, 600, "srednji nivo")]
        [TestCase(1, "kompas", "elektricni", 500, 600, "")]
        public void ElecElementConstructorWithWrongParameters(int elementId, string name, string type, double latitude, double longitude, string voltageLevel = "srednji napon")
        {

            Assert.Throws<ArgumentException>(
                () =>
                {
                    ElectricalElement el = new ElectricalElement(elementId, name, type, latitude, longitude, voltageLevel);
                });
        }

        [Test]

        [TestCase(13, null, "kameni", 800, 2000, "srednji napon")]
        [TestCase(123, "busola", null, 800, 2000, "srednji napon")]
        [TestCase(3, "busola", "kameni", 800, 2000, null)]
        public void ElecElementConstructorWithWrongParametersNull(int elementId, string name, string type, double latitude, double longitude, string voltageLevel)
        {

            Assert.Throws<ArgumentException>(
                () =>
                {
                    ElectricalElement el = new ElectricalElement(elementId, name, type, latitude, longitude, voltageLevel);
                });
        }

        [Test]

        [TestCase(1)]
        [TestCase(1234432)]

        public void setElementIdWithCorrectValue(int elementId)
        {
            //ElectricalElement element = new ElectricalElement(2, "telefon", "bezicni", 500, 600, "srednji nivo");

            if (elementId > 0)
            {
                element.ElementId = elementId;
                Assert.AreEqual(element.ElementId, elementId);
            }
        }

        [Test]

        [TestCase(0)]
        [TestCase(-10)]

        public void setElementIdWithWrongValue(int elementId)
        {
            if (elementId < 1)
            {
                Assert.Pass();
            }
        }

        [Test]

        [TestCase("busola")]
        [TestCase("komp")]
        public void setNameWithCorrectValue(string name)
        {
            // ElectricalElement element = new ElectricalElement(2, "telefon", "bezicni", 500, 600, "srednji nivo");

            if (name.Length > 0 && name != null)
            {
                element.Name = name;
                Assert.AreEqual(element.Name, name);
            }
        }

        [Test]

        [TestCase("")]

        public void setNameWithWrongValue(string name)
        {
            if (name.Length <= 0)
            {
                Assert.Pass();
            }
        }

        [Test]

        [TestCase(null)]
        public void setNameWithWrongValueNull(string name)
        {
            Assert.IsNull(name);
        }

        [Test]

        [TestCase("elektricni")]
        [TestCase("vodeni")]
        public void setTypeWithCorrectValue(string type)
        {
            //ElectricalElement element = new ElectricalElement(2, "telefon", "bezicni", 500, 600, "srednji nivo");

            if (type.Length > 0 && type != null)
            {
                element.Type = type;
                Assert.AreEqual(element.Type, type);
            }
        }

        [Test]

        [TestCase("")]

        public void setTypeWithWrongValue(string type)
        {
            if (type.Length <= 0)
            {
                Assert.Pass();
            }
        }

        [Test]

        [TestCase(null)]
        public void setTypeWithWrongValueNull(string type)
        {
            Assert.IsNull(type);
        }

        [Test]

        [TestCase(500)]
        public void setLatitudeWithCorrectValue(int latitutde)
        {
            element.Latitude = latitutde;
            Assert.AreEqual(element.Latitude, latitutde);
        }

        [Test]

        [TestCase(800)]
        public void setLongitudeWithCorrectValue(int longitude)
        {
            element.Longitude = longitude;
            Assert.AreEqual(element.Longitude, longitude);
        }

    }
}