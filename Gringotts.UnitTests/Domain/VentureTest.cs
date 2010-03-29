using System;
using Gringotts.Domain;
using NUnit.Framework;

namespace Gringotts.UnitTests.Domain
{
    [TestFixture]
    public class VentureTest
    {
        [Test]
        public void Should_Be_Able_To_Create_A_Venture()
        {
            String nameOfVenture = "Venture-1";
            int outlay = 100;
            Venture venture = new Venture() { Name = nameOfVenture, Outlay = outlay};
            Assert.IsNull(venture.Id);
            Assert.AreEqual(nameOfVenture, venture.Name);
            Assert.AreEqual(outlay, venture.Outlay);
        }
    }
}
