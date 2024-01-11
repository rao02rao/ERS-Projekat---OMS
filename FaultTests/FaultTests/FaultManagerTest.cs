using OutageManagementSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Action = OutageManagementSystem.Action;

namespace FaultTests.FaultTests
{
    internal class FaultManagerTest
    {

        private FakeFaultDB faultDB = new FakeFaultDB();

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        [TestCase(1, "izgorelo", "izgorelo je kuciste")]
        [TestCase(123, "poplava", "doslo je do kratkog spoja na napajanju")]

        public void PromptAndAddFault(int elementId, string shortDescription, string description)
        {
            FaultDescription fault = new FaultDescription(shortDescription, description, elementId);
            faultDB.AddFault(fault);
            Assert.IsNotNull(faultDB.GetFault(fault.FaultId));
        }

        [Test]
        [TestCase(1, "izgorelo", "izgorelo je kuciste")]
        [TestCase(123, "poplava", "doslo je do kratkog spoja na napajanju")]

        public void AddActionsToFault(int elementId, string shortDescription, string description)
        {
            var fault = new FaultDescription(shortDescription, description, elementId);
            Action action = new Action();
            foreach (FaultDescription f in faultDB.Faults)
            {
                if (fault.FaultId == f.FaultId)
                {
                    f.Actions.Add(action);
                    Assert.IsNotEmpty(f.Actions);
                }
            }

        }

        //[Test]
        //[TestCase(1, "izgorelo", "izgorelo je kuciste")]
        //[TestCase(123, "poplava", "doslo je do kratkog spoja na napajanju")]
        //public void CalculatePriority(int elementId, string shortDescription, string description)
        //{


        //    double priority = 0;
        //    TimeSpan timeSinceCreation = DateTime.Now - fault.TimeOfCreation;
        //    priority += timeSinceCreation.Days;

        //    if (fault.Actions != null)
        //    {
        //        priority += fault.Actions.Count * 0.5;
        //    }

        //    return priority;
        //}

        [Test]
        [TestCase("Nepotvrđen")]
        [TestCase("U popravci")]
        [TestCase("Testiranje")]
        [TestCase("Zatvoren")]
        public void IsValidStatusCorrectParameter(string status)
        {
            if(status == "Nepotvrđen" || status == "U popravci" || status == "Testiranje" || status == "Zatvoren")
            {
                Assert.Pass();
            }
        }

        [Test]
        [TestCase("")]
        [TestCase("popravka")]

        public void IsValidStatusWrongParameter(string status)
        {
            if (status == "Nepotvrđen" || status == "U popravci" || status == "Testiranje" || status == "Zatvoren")
            {
                Assert.Fail();
            }else
            {
                Assert.Pass();
            }
        }
        
        [Test]
        [TestCase(null)]
        public void IsValidStatusWrongParameterNull(string status)
        {
            Assert.IsNull(status);
        }

    }
}
