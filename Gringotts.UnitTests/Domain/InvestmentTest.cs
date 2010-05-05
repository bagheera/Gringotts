using NUnit.Framework;

namespace Gringotts.Domain{
    [TestFixture]
    public class InvestmentTest{
        [Test]
        public void ShouldBeAbleToGiveReturns(){
            var balance = new Amount(1000);
            var offer = new Amount(250);
            var dividend = new Amount(50);
            var investor = new Investor(new Name("Dummy"), balance);

            var investment = new Investment(investor, null, offer);
            Venture dummyVenture = new Venture(new Name("ventura"), new Amount(1000), new Amount(1));
            investment.GiveReturn(dummyVenture, dividend);
            Assert.AreEqual(balance + dividend, investor.Balance);
        }
    }
}