using System;
using NUnit.Framework;

namespace Gringotts.Domain{
    [TestFixture]
    public class InvestorTest{
        [Test]
        public void CanCreateInvestor(){
            var amount = new Amount(10);
            var investor = new Investor(new Name("Investor 1"), amount);
            Assert.AreEqual(amount, investor.Balance);
        }

        [Test]
        public void CorpusDecreasesToTheExtentOfTheOffer(){
            var investor = new Investor(new Name("Inverstor1"), new Amount(1000));
            var venture = new Venture(new Name("venture1"), new Amount(1000), new Amount(500));
            Offer offer = venture.AddOffer(investor, new Amount(600));
            Assert.NotNull(offer);
            Assert.AreEqual(new Amount(400), investor.Balance);
        }

        [Test]
        public void PortfolioShouldIncreaseToTheExtentOfTheOffer(){
            var investor = new Investor(new Name("Inverstor1"), new Amount(1000));
            var venture = new Venture(new Name("venture1"), new Amount(1000), new Amount(500));
            venture.AddOffer(investor, new Amount(600));
            Assert.AreEqual(new Amount(600), investor.OfferValue);
        }

        [Test]
        public void ShouldBeAbleToAcceptCreditSurplus(){
            var investor = new Investor(new Name("Inverstor1"), new Amount(1000));
            investor.AcceptSurplus(new Amount(50));
            Assert.AreEqual(new Amount(1050), investor.Balance);
        }

        [Test]
        public void ShouldBeAbleToAcceptInvestment(){
            var investor = new Investor(new Name("Inverstor1"), new Amount(1000));
            investor.AddInvestmentToPortfolio(new Investment(investor, null, new Amount(600)));
            Assert.AreEqual(new Amount(600), investor.PortfolioValue);
        }

        [Test]
        public void ShouldBeAbleToAcceptOffer(){
            var investor = new Investor(new Name("Inverstor1"), new Amount(1000));
            var venture = new Venture(new Name("venture1"), new Amount(1000), new Amount(500));
            investor.AcceptOffer(new Offer(investor, new Amount(600), venture));
        }

        [Test]
        public void ShouldCreateABalanceEventWhenInvestorIsCreated(){
            Amount amount = new Amount(100);
            var investor = new Investor(new Name("Investor"), amount);
            BalanceHistory history = investor.GetBalanceHistory();
            BalanceEvent balanceEvent = new BalanceEvent(BalanceEvent.INVESTOR_CREATED, amount);
            Assert.Contains(balanceEvent, history.GetEvents());
        }

        [Test]
        public void ShouldCreateABalanceEventWhenInvestorMakesAnOffer()
        {
            var investor = new Investor(new Name("Inverstor1"), new Amount(1000));
            var venture = new Venture(new Name("ventura!"), new Amount(1000), new Amount(500));
            Offer offer = venture.AddOffer(investor, new Amount(600));
            Assert.NotNull(offer);
            Assert.AreEqual(new Amount(400), investor.Balance);
            BalanceHistory history = investor.GetBalanceHistory();
            string offerEvent = string.Format(BalanceEvent.OFFER_ACCEPTED, offer.VentureName);
            BalanceEvent balanceEvent = new BalanceEvent(offerEvent, new Amount(400));
            Assert.Contains(balanceEvent, history.GetEvents());
        }

        [Test]
        public void ShouldCreateAOfferRejectedEventWhenVentureRejectsAnOffer(){
            var initialBalance = new Amount(1000);
            var investor1 = new Investor(new Name("Inverstor 1"), initialBalance);
            var investor2 = new Investor(new Name("Inverstor 2"), initialBalance);
            
            var outlay = new Amount(500);
            var venture = new Venture(new Name("ventura Inc."), outlay, new Amount(1));
            
            venture.AddOffer(investor1, outlay);
            Assert.AreEqual(initialBalance - outlay, investor1.Balance);
            var offerAmount2 = new Amount(600);
            venture.AddOffer(investor2, offerAmount2);
            Assert.AreEqual(initialBalance - offerAmount2, investor2.Balance);

            venture.Start();

            BalanceHistory history = investor2.GetBalanceHistory();
            string offerEvent = string.Format(BalanceEvent.OFFER_REJECTED, venture.Name.GetValue());
            BalanceEvent balanceEvent = new BalanceEvent(offerEvent, initialBalance);
            Assert.Contains(balanceEvent, history.GetEvents());
        }
    }
}