using NUnit.Framework;

namespace Gringotts.Domain{
    [TestFixture]
    public class InvestmentTest{
        [Test]
        public void ShouldBeAbleToGiveReturns(){
            var corpus = new Amount(1000);
            var offer = new Amount(250);
            var dividend = new Amount(50);
            var investor = new Investor(new Name("Dummy"), corpus);

            var investment = new Investment(investor, null, offer);
            investment.GiveReturn(dividend);
            Assert.AreEqual(corpus + dividend, investor.Balance);
        }
    }
}