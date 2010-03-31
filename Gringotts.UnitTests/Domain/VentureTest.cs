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
            Amount minInvestment = new Amount(1);
            Venture venture = new Venture(nameOfVenture, outlay, minInvestment);
            Assert.AreEqual(nameOfVenture, venture.Name);
            Assert.AreEqual(outlay, venture.Outlay);
            Assert.AreEqual(minInvestment, venture.MinInvestment);
        }

        [Test]
        public void Should_not_be_able_to_create_a_venture_for_negative_min_investment()
        {
            Assert.Throws<Exception>(delegate { new Venture(new Name("Ventura"), new Amount(100), new Amount(-1)); });            
        }

        [Test]
        public void Should_not_be_able_to_create_a_venture_for_outlay_lesser_than_min_investment()
        {
            Assert.Throws<Exception>(delegate { new Venture(new Name("Ventura"), new Amount(0), new Amount(1)); });            
        }
    }
}
