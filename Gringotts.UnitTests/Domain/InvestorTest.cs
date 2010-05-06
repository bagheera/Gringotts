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
            investor.PartialRefundOnOffer(new Amount(50));
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
        public void ShouldCreateAInvestorCreatedBalanceEventWhenInvestorIsCreated(){
            Amount amount = new Amount(100);
            var investor = new Investor(new Name("Investor"), amount);
            BalanceHistory history = investor.GetBalanceHistory();
            BalanceEvent balanceEvent = new BalanceEvent(BalanceEvent.INVESTOR_CREATED, amount);
            Assert.Contains(balanceEvent, history.GetEvents());
        }

        [Test]
        public void ShouldCreateAOfferAcceptedBalanceEventWhenInvestorMakesAnOffer()
        {
            var investor = new Investor(new Name("Inverstor1"), new Amount(1000));
            var venture = new Venture(new Name("ventura!"), new Amount(1000), new Amount(500));
            Offer offer = venture.AddOffer(investor, new Amount(600));
            Assert.AreEqual(new Amount(400), investor.Balance);
            BalanceHistory history = investor.GetBalanceHistory();
            string offerEvent = string.Format(BalanceEvent.OFFER_ACCEPTED, offer.VentureName);
            BalanceEvent balanceEvent = new BalanceEvent(offerEvent, new Amount(400));
            Assert.Contains(balanceEvent, history.GetEvents());
        }

        [Test]
        public void ShouldCreateABalanceEventWhenVentureBankruptcyIsNotified()
        {
            var investor = new Investor(new Name("Inverstor1"), new Amount(1100));
            var venture = new Venture(new Name("Hacker's Venture"), new Amount(500), new Amount(500));
            Offer offer = venture.AddOffer(investor, new Amount(500));
            Investment investment = offer.ToInvestment();
            
            venture.Start();

            investor.NotifyVentureBankruptcy(investment);
            
            BalanceHistory history = investor.GetBalanceHistory();
            String offerEvent = String.Format(BalanceEvent.VENTURE_BANKRUPT, "Hacker's Venture");

            BalanceEvent expectedBalanceEvent = new BalanceEvent(offerEvent, new Amount(600));

            Assert.Contains(expectedBalanceEvent, history.GetEvents());
        }

        [Test]
        public void ShouldCreateAOfferRejectedEventWhenVentureRejectsAnOffer(){
            var initialBalance = new Amount(1000);
            var investor1 = new Investor(new Name("Inverstor 1"), initialBalance);
            var investor2 = new Investor(new Name("Inverstor 2"), initialBalance);
            
            var outlay = new Amount(500);
            var venture = new Venture(new Name("Ventura Inc."), outlay, new Amount(1));
            
            venture.AddOffer(investor1, outlay);
            var offerAmount2 = new Amount(600);
            venture.AddOffer(investor2, offerAmount2);

            venture.Start();

            BalanceHistory history = investor2.GetBalanceHistory();
            string offerEvent = string.Format(BalanceEvent.OFFER_REJECTED, venture.Name);
            BalanceEvent balanceEvent = new BalanceEvent(offerEvent, initialBalance);
            Assert.Contains(balanceEvent, history.GetEvents());
        }
    
        [Test]
        public void ShouldCreateAOfferPartiallyAcceptedEventWhenOffersAreConfirmed()
        {
            var initialBalance = new Amount(1000);
            var investor1 = new Investor(new Name("Inverstor 1"), initialBalance);

            var outlay = new Amount(400);
            var venture = new Venture(new Name("Ventura Inc."), outlay, new Amount(1));

            var excess = new Amount(100);
            venture.AddOffer(investor1, outlay + excess);
            Assert.AreEqual(initialBalance - (outlay + excess), investor1.Balance);

            venture.Start();

            BalanceHistory history = investor1.GetBalanceHistory();
            string offerEvent = string.Format(BalanceEvent.OFFER_PARTIALLY_ACCEPTED, venture.Name);
            BalanceEvent balanceEvent = new BalanceEvent(offerEvent, initialBalance - outlay);
            Assert.Contains(balanceEvent, history.GetEvents());
        }

        [Test]
        public void ShouldCreateDividendReceivedEventWhenDividendIsDeclared(){
            var initialBalance = new Amount(1000);
            var investor1 = new Investor(new Name("Inverstor 1"), initialBalance);

            var outlay = new Amount(400);
            var venture = new Venture(new Name("Ventura Inc."), outlay, new Amount(1));

            venture.AddOffer(investor1, outlay);

            venture.Start();
            var dividend = new Amount(1000);
            venture.HandOutDividends(dividend);

            BalanceHistory history = investor1.GetBalanceHistory();
            string dividendEvent = string.Format(BalanceEvent.DIVIDEND_RECEIVED, venture.Name);
            BalanceEvent balanceEvent = new BalanceEvent(dividendEvent, initialBalance - outlay + dividend);
            Assert.Contains(balanceEvent, history.GetEvents());
        }
    }
}