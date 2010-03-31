using System;
using Gringotts.Domain;
using NUnit.Framework;

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
            Assert.AreEqual(Venture.PROPOSED_STATE, venture.State);
        }

        [Test]
        public void Should_not_be_able_to_create_a_venture_for_negative_min_investment()
        {
            Assert.Throws<Exception>(delegate { new Venture(new Name("Ventura"), new Amount(100), new Amount(-1)); });            
        }

        [Test]
        public void Should_not_be_able_to_create_a_venture_for_outlay_lesser_than_min_investment()
        {
            Assert.Throws<Exception>(delegate { new Venture(new Name("Ventura"), new Amount(0), new Amount(1)); });            
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

        //[Test]
        //public void Should_Be_Able_To_Start_A_Venture()
        //{
        //    Venture venture = new Venture(new Name("Ventura"), new Amount(100), new Amount(1));
        //    Investor investor0 = new Investor(new Name("Investor 0"), new GringottsDate(DateTime.Now), new Amount(100));
        //    Investor investor1 = new Investor(new Name("Investor 1"), new GringottsDate(DateTime.Now), new Amount(300));
        //    Investor investor2 = new Investor(new Name("Investor 2"), new GringottsDate(DateTime.Now), new Amount(250));
        //    Investor investor3 = new Investor(new Name("Investor 3"), new GringottsDate(DateTime.Now), new Amount(300));
        //    Investor investor4 = new Investor(new Name("Investor 4"), new GringottsDate(DateTime.Now), new Amount(150));
        //    Investor investor5 = new Investor(new Name("Investor 5"), new GringottsDate(DateTime.Now), new Amount(400));

        //    Investment investment0 = venture.AddOffer(investor0, new Amount(10));
        //    Investment investment1 = venture.AddOffer(investor1, new Amount(30));
        //    Investment investment2 = venture.AddOffer(investor2, new Amount(25));
        //    Investment investment3 = venture.AddOffer(investor3, new Amount(20));
        //    Investment investment4 = venture.AddOffer(investor4, new Amount(15));
        //    Investment investment5 = venture.AddOffer(investor5, new Amount(40));


        //}
    }
}
