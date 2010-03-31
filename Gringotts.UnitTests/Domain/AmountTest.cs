using Gringotts.Domain;
using NUnit.Framework;

namespace Gringotts.Domain
{
    [TestFixture]
    public class AmountTest
    {
        [Test]
        public void Should_Be_Able_To_Subtract()
        {
            Amount amount = new Amount(500);
            Amount another = new Amount(400);
            amount -= another;
            Assert.AreEqual(100, amount.Value);
        }

        [Test]
        public void Should_Be_Able_To_Add()
        {
            Amount amount = new Amount(500);
            Amount another = new Amount(400);
            amount += another;
            Assert.AreEqual(900, amount.Value);
        }
    }
}