using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;

namespace Gringotts.Domain
{
    [TestFixture]
    public class VentureTest
    {
        [Test]
        public void Should_Be_Able_To_Create_A_Venture()
        {
            Name nameOfVenture = new Name("Ventura");
            Amount outlay = new Amount(100);
            Amount minInvestment = new Amount(1);
            Venture venture = new Venture(nameOfVenture, outlay, minInvestment);
            Assert.AreEqual(nameOfVenture, venture.Name);
            Assert.AreEqual(outlay, venture.Outlay);
            Assert.AreEqual(minInvestment, venture.MinInvestment);
            Assert.True(venture.IsProposed());
        }

        [Test]
        public void Should_not_be_able_to_create_a_venture_for_negative_min_investment()
        {
            Assert.Throws<Exception>(() => new Venture(new Name("Ventura"), new Amount(100), new Amount(-1)));
        }

        [Test]
        public void Should_not_be_able_to_create_a_venture_for_outlay_lesser_than_min_investment()
        {
            Assert.Throws<Exception>(() => new Venture(new Name("Ventura"), new Amount(0), new Amount(1)));
        }

        [Test]
        public void Should_Increase_Subscription()
        {
            Venture venture = new Venture(new Name("Venture1"), new Amount(50099), new Amount(1345));
            Investor investor = new Investor(new Name("Investor1"), new GringottsDate(DateTime.Now), new Amount(5000));
            venture.AddOffer(investor, new Amount(2000));
            Assert.AreEqual(new Amount(2000), venture.SubscribedAmount());
        }

        [Test]
        public void Should_Be_Able_To_Change_State_Of_A_New_Venture_To_Cancelled()
        {
            Venture venture = new Venture(new Name("Ventura"), new Amount(100), new Amount(1));
            venture.ChangeStateToCancelled();
            Assert.AreEqual(Venture.CANCELLED_STATE, venture.State);
        }

        [Test]
        public void Should_Be_Able_To_Change_State_Of_A_New_Venture_To_Started()
        {
            Venture venture = new Venture(new Name("Ventura"), new Amount(100), new Amount(1));
            venture.ChangeStateToStarted();
            Assert.AreEqual(Venture.STARTED_STATE, venture.State);
        }

        [Test]
        public void Should_Accept_Investment_Only_If_Greater_Than_MinimumAmount()
        {
            Venture venture = new Venture(new Name("Venture"), new Amount(1000000), new Amount(23538));
            Investor investor = new Investor(new Name("investor"), new GringottsDate(DateTime.Now), new Amount(50000));
            Assert.Throws<InvalidOfferException>(() => venture.AddOffer(investor, new Amount(3000)));
        }
        [Test]
        public void Should_Accept_Investment()
        {
            Venture venture = new Venture(new Name("Venture"), new Amount(1000000), new Amount(23538));
            Investor investor = new Investor(new Name("investor"), new GringottsDate(DateTime.Now), new Amount(50000));
            Assert.DoesNotThrow(() => venture.AddOffer(investor, new Amount(30000)));
        }

        public void Should_Be_Able_To_Hand_Out_Dividends()
        {
            Venture venture = new Venture(new Name("Venture"), new Amount(1000), new Amount(1));
            Investor quarterInvestor = new Investor(new Name("investor"), new GringottsDate(DateTime.Now), new Amount(1000));
            Investor threeFourthsInvestor = new Investor(new Name("investor"), new GringottsDate(DateTime.Now), new Amount(1000));
            Amount dividend = new Amount(1000);

            venture.AddOffer(quarterInvestor, new Amount(250));
            venture.AddOffer(threeFourthsInvestor, new Amount(750));

            venture.Start();

            venture.HandOutDividends(dividend);
        }

        [Test]
        public void Should_Be_Able_To_Create_And_Add_Investments_To_Holdings()
        {
            Holding holding = new Holding();
            Investor investor = new Investor(new Name("investor"), new GringottsDate(DateTime.Now), new Amount(50000));

            holding.Add(new Investment(investor, new Amount(100)));
        }

        [Test]
        public void Should_Be_Able_To_Get_Ventures_Holdings()
        {
            Venture venture = new Venture(new Name("Ventura"), new Amount(100), new Amount(1));
            Assert.NotNull(venture.Holding);
        }

        [Test]
        public void Should_Not_Be_Able_To_Start_A_Venture_If_Status_Is_Not_Proposed()
        {
            Venture venture = new Venture(new Name("Ventura"), new Amount(100), new Amount(1));
            venture.ChangeStateToCancelled();
            Assert.Throws<Exception>(venture.Start);
        }

        [Test]
        public void Should_Not_Be_Able_To_Start_A_Venture_If_Subscription_Is_Less_Than_Overlay()
        {
            Venture venture = new Venture(new Name("Ventura"), new Amount(100), new Amount(1));
            Investor investor = new Investor(new Name("Investor"), new GringottsDate(DateTime.Now), new Amount(50));
            venture.AddOffer(investor, new Amount(40));
            Assert.Throws<Exception>(venture.Start);
        }

        [Test]
        public void Holding_Should_Be_Created_When_Venture_Starts()
        {
            Venture venture = new Venture(new Name("Ventura"), new Amount(100), new Amount(1));
            Investor investor0 = new Investor(new Name("Investor0"), new GringottsDate(DateTime.Now), new Amount(100));
            Investor investor1 = new Investor(new Name("Investor1"), new GringottsDate(DateTime.Now), new Amount(100));
            venture.AddOffer(investor0, new Amount(50));
            venture.AddOffer(investor1, new Amount(50));
            venture.Start();
            Assert.Greater(venture.Holding.Investments.Count, 0);
        }

        [Test]
        public void Should_Be_Able_To_Confirm_Subscription()
        {
            Subscription subscription = new Subscription();
            Investor investor0 = new Investor(new Name("Investor0"), new GringottsDate(DateTime.Now), new Amount(100));
            Investor investor1 = new Investor(new Name("Investor1"), new GringottsDate(DateTime.Now), new Amount(100));
            Investor investor2 = new Investor(new Name("Investor2"), new GringottsDate(DateTime.Now), new Amount(100));
            Investor investor3 = new Investor(new Name("Investor3"), new GringottsDate(DateTime.Now), new Amount(100));
            subscription.Add(new Investment(investor0, new Amount(100)));
            subscription.Add(new Investment(investor1, new Amount(200)));
            subscription.Add(new Investment(investor2, new Amount(300)));
            Investment excess = new Investment(investor3, new Amount(400));
            subscription.Add(excess);
            Amount outlay = new Amount(600);
            List<Investment> confirmations = subscription.Confirm(outlay);
            Assert.IsFalse(confirmations.Contains(excess));
            Assert.AreEqual(outlay, confirmations.Aggregate(new Amount(0), (sum, inv) => sum + inv.Value));
        }

        [Test]
        public void Should_Not_Take_More_Investment_Than_Outlay()
        {
            Amount outlay = new Amount(40);
            Venture venture = new Venture(new Name("Ventura"), outlay, new Amount(1));
            Investor investor0 = new Investor(new Name("Investor0"), new GringottsDate(DateTime.Now), new Amount(100));
            venture.AddOffer(investor0, new Amount(50));
            venture.Start();
            Assert.AreEqual(outlay, venture.Holding.Investments.Aggregate(new Amount(0), (sum, inv) => sum + inv.Value));
        }

        [Test]
        public void Should_Return_Over_Investment_To_Investor_When_Start()
        {
            Amount outlay = new Amount(40);
            Venture venture = new Venture(new Name("Ventura"), outlay, new Amount(1));
            Investor investor0 = new Investor(new Name("Investor0"), new GringottsDate(DateTime.Now), new Amount(100));
            venture.AddOffer(investor0, new Amount(50));
            venture.Start();
            Assert.AreEqual(new Amount(60), investor0.Corpus);
        }

        [Test]
        public void Should_Be_Able_To_Start_A_Venture()
        {
            Amount outlay = new Amount(40);
            Venture venture = new Venture(new Name("Ventura"), outlay, new Amount(1));
            Investor investor0 = new Investor(new Name("Investor0"), new GringottsDate(DateTime.Now), new Amount(100));
            venture.AddOffer(investor0, new Amount(50));
            venture.Start();
            Assert.True(venture.IsStarted());
        }

        public void Should_Not_Be_Able_To_Divide_Dividends_Unless_In_A_Started_State()
        {
            Amount dividend = new Amount(1000);
            Venture venture = new Venture(new Name("Ventura"), new Amount(100), new Amount(1));
            Assert.Throws<Exception>(delegate{venture.HandOutDividends(dividend);});
            venture.ChangeStateToCancelled();
            Assert.Throws<Exception>(delegate { venture.HandOutDividends(dividend); });
            venture.ChangeStateToStarted();
            Assert.DoesNotThrow(delegate { venture.HandOutDividends(dividend); });
        }

        [Test]
        public void Should_Allow_Investor_To_Invest_Only_Once()
        {
            Venture venture = new Venture(new Name("Ventura"), new Amount(100), new Amount(1));
            Investor investor = new Investor(new Name("investor"), new GringottsDate(DateTime.Now), new Amount(50000));
            Investor duplicateInvestor = new Investor(new Name("investor"), new GringottsDate(DateTime.Now), new Amount(500));
            venture.AddOffer(investor, new Amount(2));
            Assert.Throws<InvalidOfferException>(() => venture.AddOffer(duplicateInvestor, new Amount(2)));
        }
    }
}