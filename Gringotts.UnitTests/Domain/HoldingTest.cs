﻿using System.Linq;
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
            Venture dummyVenture = new Venture(new Name("ventura"), new Amount(1000), new Amount(1));
            holding.Add(new Investment(new Investor(new Name("quarter"), new Amount(1500)), dummyVenture, new Amount(250)));
            holding.Add(new Investment(new Investor(new Name("threeFourths"), new Amount(1000)), dummyVenture, new Amount(750)));
            holding.DistributeDividends(profit);
        }

        [Test]
        public void ShouldBeAbleToCalculateParticipation()
        {
            Holding holding = new Holding();
            Venture dummyVenture = new Venture(new Name("ventura"), new Amount(1000), new Amount(1));

            holding.Add(new Investment(new Investor(new Name("quarter"), new Amount(1500)), dummyVenture, new Amount(250)));
            holding.Add(new Investment(new Investor(new Name("threeFourths"), new Amount(1000)), dummyVenture, new Amount(750)));
            holding.DistributeDividends(new Amount(1000));
        }

        [Test]
        public void ShouldBeAbleToSplitInvestments()
        {
            var holding = new Holding();
            Venture dummyVenture = new Venture(new Name("ventura"), new Amount(1000), new Amount(1));
            holding.Add(new Investment(new Investor(new Name("quarter"), new Amount(1500)), dummyVenture, new Amount(250)));
            holding.Add(new Investment(new Investor(new Name("threeFourths"), new Amount(1000)), dummyVenture, new Amount(750)));
            var aPercentageOfSplit = new Percentage(0.4f);
            Assert.AreEqual(2,holding.Split(aPercentageOfSplit).Count());
        }
    }
}