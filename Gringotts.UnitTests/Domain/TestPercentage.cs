

using System;
using NUnit.Framework;

namespace Gringotts.Domain
{
    [TestFixture]
    public class TestPercentage
    {
        [Test]
        public void ShouldThrowExceptionWhenPercentageRatioIsInvalid()
        {
            Assert.Throws<ArgumentException>(() => new Percentage(12.0f));
            Assert.Throws<ArgumentException>(() => new Percentage(-12.0f));
        }

        [Test]
        public void ShouldBeAbleToApplyPercentage()
        {
            var percentage = new Percentage(0.4f);
            Assert.AreEqual(new Amount(40.0f), percentage.Apply(new Amount(100)));
        }

        [Test]
        public void ShouldBeAbleToApplyRemainingPercentage()
        {
            var percentage = new Percentage(0.4f);
            Assert.AreEqual(new Amount(60.0f).Denomination, percentage.ApplyRemaining(new Amount(100)).Denomination,0.1f);
        }
    }
}
