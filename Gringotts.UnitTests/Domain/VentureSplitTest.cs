using System;
using Gringotts.Domain;
using NUnit.Framework;

namespace Gringotts.Domain
{
    [TestFixture]
    public class VentureSplitTest{
        [Test]
        public void ShouldCreateTwoVenturesOnSplit(){
            Venture venture = new Venture(new Name("venture-name"), new Amount(100), new Amount(10));
            TermsOfSplit terms = new TermsOfSplit(20, "new-venture-1", "new-venture-2");
            SplitVentures ventures = venture.Split();
        }
    }

    public class SplitVentures{
    }
}