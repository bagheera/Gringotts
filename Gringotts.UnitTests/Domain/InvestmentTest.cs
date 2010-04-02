using System;
using NUnit.Framework;

namespace Gringotts.Domain
{
    [TestFixture]
    public class InvestmentTest
    {
        [Test]
        public void Should_Be_Able_To_Give_Returns()
        {
            Amount corpus = new Amount(1000);
            Amount offer = new Amount(250);
            Amount dividend = new Amount(50);
            Investor investor = new Investor(new Name("Dummy"), new GringottsDate(DateTime.Now), corpus);

            Investment investment = new Investment(investor, null, offer);
            investment.GiveReturn(dividend);
            Assert.AreEqual(corpus + dividend, investor.Corpus);
        }
    }
}
