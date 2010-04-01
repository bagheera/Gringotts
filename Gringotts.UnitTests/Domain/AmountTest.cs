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
            Assert.AreEqual(new Amount(100), amount);
        }

        [Test]
        public void Should_Be_Able_To_Add()
        {
            Amount amount = new Amount(500);
            Amount another = new Amount(400);
            amount += another;
            Assert.AreEqual(new Amount(900), amount);
        }

        [Test]
        public void Should_Be_Able_To_Divide()
        {
            Amount amount = new Amount(500);
            Amount another = new Amount(400);
            amount = amount / another;
            Assert.AreEqual(new Amount((decimal) 500/400), amount);
        }

        [Test]
        public void Should_Be_Able_To_Multiply()
        {
            Amount amount = new Amount(500);
            Amount another = new Amount(400);
            amount = amount * another;
            Assert.AreEqual(new Amount((decimal)500 * 400), amount);
        }
    }
}