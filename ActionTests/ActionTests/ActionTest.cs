using System;

namespace ActionTests.ActionTests
{
    public class ActionTests
    {
        public DateTime dt = new DateTime(2021, 1, 1);
        [SetUp]
        public void Setup()
        {
            DateTime dateTime = DateTime.Now;
        }

        [Test]

        [TestCase("01/01/2012","opis")]
        [TestCase("12/01/2015","sadsadas sad opis")]
        [TestCase("11/30/2015","opis malko duzi")]
        public void ValidateActionWithCorrectParameters(DateTime timeOfAction, string description)
        {
            if ((!string.IsNullOrWhiteSpace(description) && (timeOfAction <= DateTime.Now)))
                Assert.Pass();
        }


        [Test]

        [TestCase("01/01/2021", "")]
        [TestCase("12/05/2025", "opis")]
        [TestCase("01/10/2024", null)]
        public void ValidateActionWithWrongParameters(DateTime timeOfAction, string description)
        {
            if (string.IsNullOrWhiteSpace(description) || (timeOfAction > DateTime.Now))
                Assert.Pass();
        }

    }
}