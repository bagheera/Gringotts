using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Gringotts.Domain{
    [TestFixture]
    public class VentureTest{
        public void ShouldBeAbleToHandOutDividends(){
            var venture = new Venture(new Name("Venture"), new Amount(1000), new Amount(1));
            var quarterInvestor = new Investor(new Name("investor"), new Amount(1000));
            var threeFourthsInvestor = new Investor(new Name("investor"), new Amount(1000));
            var dividend = new Amount(1000);

            venture.AddOffer(quarterInvestor, new Amount(250));
            venture.AddOffer(threeFourthsInvestor, new Amount(750));

            venture.Start();

            venture.HandOutDividends(dividend);
        }

        public void ShouldNotBeAbleToDivideDividendsUnlessInAStartedState(){
            var dividend = new Amount(1000);
            var venture = new Venture(new Name("Ventura"), new Amount(100), new Amount(1));
            Assert.Throws<Exception>(delegate { venture.HandOutDividends(dividend); });
            venture.ChangeStateToCancelled();
            Assert.Throws<Exception>(delegate { venture.HandOutDividends(dividend); });
            venture.ChangeStateToStarted();
            Assert.DoesNotThrow(delegate { venture.HandOutDividends(dividend); });
        }

        [Test]
        public void HoldingShouldBeCreatedWhenVentureStarts(){
            var venture = new Venture(new Name("Ventura"), new Amount(100), new Amount(1));
            var investor0 = new Investor(new Name("Investor0"),  new Amount(100));
            var investor1 = new Investor(new Name("Investor1"),  new Amount(100));
            venture.AddOffer(investor0, new Amount(50));
            venture.AddOffer(investor1, new Amount(50));
            venture.Start();
            Assert.Greater(venture.Holding.Investments.Count, 0);
        }

        [Test]
        public void ShouldAcceptInvestment(){
            var venture = new Venture(new Name("Venture"), new Amount(1000000), new Amount(23538));
            var investor = new Investor(new Name("investor"),  new Amount(50000));
            Assert.DoesNotThrow(() => venture.AddOffer(investor, new Amount(30000)));
        }

        [Test]
        public void ShouldAcceptInvestmentOnlyIfGreaterThanMinimumAmount(){
            var venture = new Venture(new Name("Venture"), new Amount(1000000), new Amount(23538));
            var investor = new Investor(new Name("investor"),  new Amount(50000));
            Assert.Throws<InvalidOfferException>(() => venture.AddOffer(investor, new Amount(3000)));
        }

        [Test]
        public void ShouldAllowInvestorToInvestOnlyOnce(){
            var venture = new Venture(new Name("Ventura"), new Amount(100), new Amount(1));
            var investor = new Investor(new Name("investor"),  new Amount(50000));
            var duplicateInvestor = new Investor(new Name("investor"),  new Amount(500));
            venture.AddOffer(investor, new Amount(2));
            Assert.Throws<InvalidOfferException>(() => venture.AddOffer(duplicateInvestor, new Amount(2)));
        }

        [Test]
        public void ShouldBeAbleToChangeStateOfANewVentureToCancelled(){
            var venture = new Venture(new Name("Ventura"), new Amount(100), new Amount(1));
            venture.ChangeStateToCancelled();
            Assert.AreEqual(Venture.CANCELLED_STATE, venture.State);
        }

        [Test]
        public void ShouldBeAbleToChangeStateOfANewVentureToStarted(){
            var venture = new Venture(new Name("Ventura"), new Amount(100), new Amount(1));
            venture.ChangeStateToStarted();
            Assert.AreEqual(Venture.STARTED_STATE, venture.State);
        }

        [Test]
        public void ShouldBeAbleToConfirmSubscription(){
            var subscription = new Subscription();
            var investor0 = new Investor(new Name("Investor0"),  new Amount(100));
            var investor1 = new Investor(new Name("Investor1"),  new Amount(100));
            var investor2 = new Investor(new Name("Investor2"),  new Amount(100));
            var investor3 = new Investor(new Name("Investor3"),  new Amount(100));
            subscription.Add(new Offer(investor0, new Amount(100), null));
            subscription.Add(new Offer(investor1, new Amount(200), null));
            subscription.Add(new Offer(investor2, new Amount(300), null));
            var excess = new Offer(investor3, new Amount(400), null);
            subscription.Add(excess);
            var outlay = new Amount(600);
            List<Investment> confirmations = subscription.Confirm(outlay);
            Assert.IsFalse(confirmations.Contains(excess.ToInvestment()));
            Assert.AreEqual(outlay, confirmations.Aggregate(new Amount(0), (sum, inv) => sum + inv.Value));
        }

        [Test]
        public void ShouldBeAbleToCreateAVenture(){
            var nameOfVenture = new Name("Ventura");
            var outlay = new Amount(100);
            var minInvestment = new Amount(1);
            var venture = new Venture(nameOfVenture, outlay, minInvestment);
            Assert.AreEqual(nameOfVenture, venture.Name);
            Assert.AreEqual(outlay, venture.Outlay);
            Assert.AreEqual(minInvestment, venture.MinInvestment);
            Assert.True(venture.IsProposed());
        }

        [Test]
        public void ShouldBeAbleToCreateAndAddInvestmentsToHoldings(){
            var holding = new Holding();
            var investor = new Investor(new Name("investor"),  new Amount(50000));
            holding.Add(new Investment(investor, null, new Amount(100)));
        }

        [Test]
        public void ShouldBeAbleToGetVenturesHoldings(){
            var venture = new Venture(new Name("Ventura"), new Amount(100), new Amount(1));
            Assert.NotNull(venture.Holding);
        }

        [Test]
        public void ShouldBeAbleToStartAVenture(){
            var outlay = new Amount(40);
            var venture = new Venture(new Name("Ventura"), outlay, new Amount(1));
            var investor0 = new Investor(new Name("Investor0"),  new Amount(100));
            venture.AddOffer(investor0, new Amount(50));
            venture.Start();
            Assert.True(venture.IsStarted());
        }

        [Test]
        public void ShouldIncreaseSubscription(){
            var venture = new Venture(new Name("Venture1"), new Amount(50099), new Amount(1345));
            var investor = new Investor(new Name("Investor1"), new Amount(5000));
            venture.AddOffer(investor, new Amount(2000));
            Assert.AreEqual(new Amount(2000), venture.SubscribedAmount());
        }

        [Test]
        public void ShouldNotBeAbleToCreateAVentureForNegativeMinInvestment(){
            Assert.Throws<Exception>(() => new Venture(new Name("Ventura"), new Amount(100), new Amount(-1)));
        }

        [Test]
        public void ShouldNotBeAbleToCreateAVentureForOutlayLesserThanMinInvestment(){
            Assert.Throws<Exception>(() => new Venture(new Name("Ventura"), new Amount(0), new Amount(1)));
        }

        [Test]
        public void ShouldNotBeAbleToStartAVentureIfStatusIsNotProposed(){
            var venture = new Venture(new Name("Ventura"), new Amount(100), new Amount(1));
            venture.ChangeStateToCancelled();
            Assert.Throws<Exception>(venture.Start);
        }

        [Test]
        public void ShouldNotBeAbleToStartAVentureIfSubscriptionIsLessThanOverlay(){
            var venture = new Venture(new Name("Ventura"), new Amount(100), new Amount(1));
            var investor = new Investor(new Name("Investor"),  new Amount(50));
            venture.AddOffer(investor, new Amount(40));
            Assert.Throws<Exception>(venture.Start);
        }

        [Test]
        public void ShouldNotTakeMoreInvestmentThanOutlay(){
            var outlay = new Amount(40);
            var venture = new Venture(new Name("Ventura"), outlay, new Amount(1));
            var investor0 = new Investor(new Name("Investor0"),  new Amount(100));
            venture.AddOffer(investor0, new Amount(50));
            venture.Start();
            // TODO: make aggregation in venture
            Assert.AreEqual(outlay, venture.Holding.Investments.Aggregate(new Amount(0), (sum, inv) => sum + inv.Value));
        }

        [Test]
        public void ShouldReturnOverInvestmentToInvestorWhenStart(){
            var outlay = new Amount(40);
            var venture = new Venture(new Name("Ventura"), outlay, new Amount(1));
            var initialCorpus = new Amount(100);
            var investor = new Investor(new Name("Investor0"),  initialCorpus);
            venture.AddOffer(investor, new Amount(50));
            venture.Start();
            Assert.AreEqual(initialCorpus - outlay, investor.Balance);
        }

        [Test]
        public void StartedVentureMayGoBankrupt(){
            var outlay = new Amount(40);
            var venture = new Venture(new Name("Ventura"), outlay, new Amount(1));
            var initialCorpus = new Amount(100);
            var investor = new Investor(new Name("Investor0"), initialCorpus);
            venture.AddOffer(investor, new Amount(50));
            venture.Start();

            venture.GoBankrupt();
            Assert.AreEqual(Venture.BANKRUPT_STATE, venture.State);
        }

        [Test]
        public void VentureNotInStartedStateWillNotGoBankrupt()
        {
            var outlay = new Amount(40);
            var venture = new Venture(new Name("Ventura"), outlay, new Amount(1));
            var initialCorpus = new Amount(100);
            var investor = new Investor(new Name("Investor0"), initialCorpus);
            venture.AddOffer(investor, new Amount(50));
            
            Assert.Throws<InvalidOperationException>(venture.GoBankrupt);
        }

        [Test]
        public void ShouldNotGiveDividendsIfItIsBankrupt(){
            var outlay = new Amount(40);
            var venture = new Venture(new Name("Ventura"), outlay, new Amount(1));
            var initialCorpus = new Amount(100);
            var investor = new Investor(new Name("Investor0"), initialCorpus);
            venture.AddOffer(investor, new Amount(50));

            venture.Start();

            venture.GoBankrupt();

            Assert.Throws<InvalidOperationException>(()=> venture.HandOutDividends(new Amount(100)));
        }

    }
}