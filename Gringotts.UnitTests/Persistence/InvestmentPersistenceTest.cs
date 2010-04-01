using System;
using Gringotts.Domain;
using NUnit.Framework;

namespace Gringotts.Persistence
{
    [TestFixture]
    [Ignore]
    public class InvestmentRepositoryPersitenceTest
    {
        [Test]
        public void Should_Be_Able_Save()
        {
            Investment investment =
                new Investment(new Investor(new Name("Investor"), new GringottsDate(DateTime.Now), new Amount(6000)),
                               new Amount(400), new Venture(new Name("Venture"), new Amount(5000), new Amount(1250)));
            Assert.AreEqual("expected", "actual");
        }
    }
}
