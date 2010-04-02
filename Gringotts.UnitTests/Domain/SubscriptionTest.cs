using System;
using NUnit.Framework;

namespace Gringotts.Domain
{
    [TestFixture]
    public class SubscriptionTest
    {
        [Test]
        public void Should_Be_Able_To_Tell_If_Investor_Already_Invested()
        {
            Investor investor = new Investor(new Name("investor"), new GringottsDate(DateTime.Now), new Amount(50000));
            Venture venture = new Venture(new Name("Ventura"), new Amount(100), new Amount(1));
            Subscription subscription = new Subscription();
            subscription.Add(new Offer(investor, new Amount(500), null));
            Assert.IsTrue(subscription.AlreadyInvested(investor));
        }
    }
}