using NUnit.Framework;

namespace Gringotts.Domain{
    [TestFixture]
    public class OfferTest{
        [Test]
        public void ShouldBeAbleToCreateInvestmentFromOffer(){
            var offer = new Offer(
                new Investor(new Name("Investor1"), new Amount(500)), new Amount(300),
                null);
            var investment =
                new Investment(new Investor(new Name("Investor1"), new Amount(500)),
                               null, new Amount(300));
            Assert.AreEqual(investment, offer.ToInvestment());
        }
    }
}