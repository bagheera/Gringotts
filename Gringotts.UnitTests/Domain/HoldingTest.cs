using System;
using NUnit.Framework;

namespace Gringotts.Domain
{
    [TestFixture]
    public class HoldingTest
    {
        [Test]
        public void Should_Be_Able_To_Distribute_Dividends_Fairly()
        {
            Amount profit = new Amount(1000);
            Holding holding = new Holding();
            holding.Add(new Investment(new Investor(new Name("quarter"), new GringottsDate(DateTime.Now), new Amount(1500)), new Amount(250)));
            holding.Add(new Investment(new Investor(new Name("threeFourths"), new GringottsDate(DateTime.Now), new Amount(1000)), new Amount(750)));
            holding.DistributeDividends(profit);
        }

        [Test]
        public void Should_Be_Able_To_Calculate_Participation()
        {
            Holding holding = new Holding();
            holding.Add(new Investment(new Investor(new Name("quarter"), new GringottsDate(DateTime.Now), new Amount(1500)), new Amount(250)));
            holding.Add(new Investment(new Investor(new Name("threeFourths"), new GringottsDate(DateTime.Now), new Amount(1000)), new Amount(750)));
            holding.CalculateParticipation();
        }
    }
}
