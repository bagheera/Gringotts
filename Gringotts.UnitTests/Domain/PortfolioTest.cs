using System;
using NUnit.Framework;

namespace Gringotts.Domain
{
    [TestFixture]
    public class PortfolioTest
    {
        [Test]
        public void Should_Be_Able_To_Create_And_Add_Investment_To_Portfolio()
        {
            Investor investor = new Investor(new Name("Inverstor1"), new GringottsDate(DateTime.Now), new Amount(1000));
            Portfolio portfolio = new Portfolio();
            portfolio.AddInvestment(new Investment(investor, new Amount(500)));
            Assert.AreEqual(new Amount(500), portfolio.Value);
        }
    }
}