using NUnit.Framework;

namespace Gringotts.Domain{
    [TestFixture]
    public class SubscriptionTest{
        [Test]
        public void ShouldBeAbleToTellIfInvestorAlreadyInvested(){
            var investor = new Investor(new Name("investor"), new Amount(50000));
            var subscription = new Subscription();
            subscription.Add(new Offer(investor, new Amount(500), null));
            Assert.IsTrue(subscription.AlreadyInvested(investor));
        }
    }
}