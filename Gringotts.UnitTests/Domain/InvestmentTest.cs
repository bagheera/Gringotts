using NUnit.Framework;
using System;

namespace Gringotts.Domain
{
    [TestFixture]
    public class InvestmentTest
    {
        [Test]
        public void Should_Be_Able_To_Return_Investment_To_Invester()
        {
            Investor investor = new Investor(new Name("Investor"), new GringottsDate(DateTime.Now), new Amount(100));
            Investment investment = new Investment(investor, new Amount(50));
            investment.CreditSurplus(new Amount(40));
            //Assert.AreEqual(new Amount());
        }
    }
}
