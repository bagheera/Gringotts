using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Gringotts.Domain
{
    [TestFixture]
    public class VentureSplitTest{
        [Test]
        public void ShouldCreateTwoVenturesOnSplit(){
            var venture = new Venture(new Name("venture-name"), new Amount(100), new Amount(10));
            var firstVentureName = new Name("new-venture-1");
            var secondVentureName =new Name("new-venture-2");
            var terms = new TermsOfSplit(new Percentage(0.8f), firstVentureName, secondVentureName);
            var ventures = venture.Split(terms);
            Assert.AreEqual(2, ventures.Count());
        }

        [Test]
        public void ShouldCreateVenturesWithPassedNames()
        {
            var venture = new Venture(new Name("venture-name"), new Amount(100), new Amount(10));
            var firstVentureName = new Name("new-venture-1");
            var secondVentureName = new Name("new-venture-2");
            var terms = new TermsOfSplit(new Percentage(0.8f), firstVentureName, secondVentureName);
            var ventures = venture.Split(terms);
            Assert.AreEqual(firstVentureName, ventures.First().Name);
            Assert.AreEqual(secondVentureName, ventures.Last().Name);
        }

        [Test]
        public void ShouldSplitOutlayMoneyAccordingToRatio()
        {
            var venture = new Venture(new Name("venture-name"), new Amount(100), new Amount(10));
            var firstVentureName = new Name("new-venture-1");
            var secondVentureName = new Name("new-venture-2");
            var percentage = new Percentage(0.2f);
            var terms = new TermsOfSplit(percentage, firstVentureName, secondVentureName);
            var ventures = venture.Split(terms);
            Assert.AreEqual(percentage.Apply(venture.Outlay), ventures.First().Outlay);
            Assert.AreEqual(percentage.ApplyRemaining(venture.Outlay), ventures.Last().Outlay);
        }
    }
}