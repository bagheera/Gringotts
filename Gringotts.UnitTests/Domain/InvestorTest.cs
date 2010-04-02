using System;
using NUnit.Framework;

namespace Gringotts.Domain
{
    [TestFixture]
    public class InvestorTest
    {
        [Test]
        public void Can_Create_Investor()
        {
            Amount amount = new Amount(10);
            Investor investor = new Investor(new Name("Investor 1"), new GringottsDate(DateTime.Now), amount);
            Assert.AreEqual(amount, investor.Corpus);
        }

        [Test]
        public void Corpus_Decreases_To_The_Extent_Of_The_Offer()
        {
            Investor investor = new Investor(new Name("Inverstor1"), new GringottsDate(DateTime.Now), new Amount(1000));
            Venture venture = new Venture(new Name("venture1"), new Amount(1000), new Amount(500));
            Offer offer = venture.AddOffer(investor, new Amount(600));
            Assert.NotNull(offer);
            Assert.AreEqual(new Amount(400), investor.Corpus);
        }

        [Test]
        public void Should_Be_Able_To_Accept_Investment()
        {
            Investor investor = new Investor(new Name("Inverstor1"), new GringottsDate(DateTime.Now), new Amount(1000));
            investor.AddInvestmentToPortfolio(new Investment(investor, null, new Amount(600)));
            Assert.AreEqual(new Amount(600), investor.PortfolioValue);
        }

        [Test]
        public void Should_Be_Able_ToAccept_Offer()
        {
            Investor investor = new Investor(new Name("Inverstor1"), new GringottsDate(DateTime.Now), new Amount(1000));
            investor.AcceptOffer(new Offer(investor, new Amount(600), null));
        }

        [Test]
        public void Portfolio_Should_Increase_To_The_Extent_Of_The_Offer()
        {
            Investor investor = new Investor(new Name("Inverstor1"), new GringottsDate(DateTime.Now), new Amount(1000));
            Venture venture = new Venture(new Name("venture1"), new Amount(1000), new Amount(500));
            venture.AddOffer(investor, new Amount(600));
            Assert.AreEqual(new Amount(600), investor.OfferValue);
        }

        [Test]
        public void Should_Be_Able_To_Accept_Credit_Surplus()
        {
            Investor investor = new Investor(new Name("Inverstor1"), new GringottsDate(DateTime.Now), new Amount(1000));
            investor.AcceptSurplus(new Amount(50));
            Assert.AreEqual(new Amount(1050), investor.Corpus);
        }
    }
}