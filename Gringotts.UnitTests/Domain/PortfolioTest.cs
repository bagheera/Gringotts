using NUnit.Framework;

namespace Gringotts.Domain{
    [TestFixture]
    public class PortfolioTest{
        [Test]
        public void ShouldBeAbleToCreateAndAddInvestmentToPortfolio(){
            var investor = new Investor(new Name("Inverstor1"), new Amount(1000));
            var portfolio = new Portfolio();
            portfolio.AddInvestment(new Investment(investor, null, new Amount(500)));
            Assert.AreEqual(new Amount(500), portfolio.Value);
        }
    }
}