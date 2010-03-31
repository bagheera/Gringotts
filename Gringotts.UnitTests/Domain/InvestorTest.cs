﻿using System;
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
        public void Should_Be_Able_To_Pay()
        {
            Investor investor = new Investor(new Name("Investor1"), new GringottsDate(DateTime.Now), new Amount(500));
            investor.Pay(new Amount(400));
            Assert.AreEqual(new Amount(100), investor.Corpus);
        }


        [Test]
        public void Should_Be_Able_To_Invest_In_A_Venture()
        {
            //todo: move the setters to constructors.
            Investor investor = new Investor(new Name("Inverstor1"), new GringottsDate(DateTime.Now), new Amount(1000));
            Venture venture = new Venture(new Name("venture1"), new Amount(5000), new Amount(1000));
            Investment investment = venture.OfferToInvest(investor, new Amount(1000));
            Assert.NotNull(investment);
            Assert.AreEqual(new Amount(0), investor.Corpus);
        }
    }
}

