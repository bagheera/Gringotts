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
            Name nameOfVenture = new Name("Ventura");
            Amount outlay = new Amount(100);
            Amount minInvestment = new Amount(0);
            Venture venture = new Venture() { Name = nameOfVenture, Outlay = outlay, MinInvestment = minInvestment};

            Assert.IsNull(venture.Id);
            Assert.AreEqual(nameOfVenture, venture.Name);
            Assert.AreEqual(outlay, venture.Outlay);
            Assert.AreEqual(minInvestment, venture.MinInvestment);
        }
    }
}
