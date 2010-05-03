using NUnit.Framework;

namespace Gringotts.Domain
{
    [TestFixture]
    public class HoldingTest
    {
        [Test]
        public void ShouldBeAbleToDistributeDividendsFairly()
        {
            Amount profit = new Amount(1000);
            Holding holding = new Holding();
            holding.Add(new Investment(new Investor(new Name("quarter"), new Amount(1500)), null, new Amount(250)));
            holding.Add(new Investment(new Investor(new Name("threeFourths"), new Amount(1000)), null, new Amount(750)));
            holding.DistributeDividends(profit);
        }

        [Test]
        public void ShouldBeAbleToCalculateParticipation()
        {
            Holding holding = new Holding();
            holding.Add(new Investment(new Investor(new Name("quarter"), new Amount(1500)), null, new Amount(250)));
            holding.Add(new Investment(new Investor(new Name("threeFourths"), new Amount(1000)), null, new Amount(750)));
            holding.DistributeDividends(new Amount(1000));
        }
    }
}