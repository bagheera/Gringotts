using System;
using NUnit.Framework;

namespace Gringotts.Domain
{
    [TestFixture]
    public class OfferTest
    {
        [Test]
        public void Should_Be_Able_To_Create_Investment_From_Offer()
        {
            Offer offer = new Offer(new Investor(new Name("Investor1"), new GringottsDate(DateTime.Now), new Amount(500)), new Amount(300), null);
            Investment investment = new Investment(new Investor(new Name("Investor1"), new GringottsDate(DateTime.Now), new Amount(500)), null, new Amount(300));
            Assert.AreEqual(investment, offer.ToInvestment());
        }
    }
}
