using Gringotts.Domain;
using NUnit.Framework;

[TestFixture]
public class SubscriptionTest
{
    [Test]
    public void Should_Be_Able_To_Add_Investment_ToSubscription()
    {
        Subscription subscription = new Subscription();
        subscription.Add(new Investment());
        Assert.AreEqual(1, subscription.Count);
    }
}