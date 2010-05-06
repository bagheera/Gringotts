﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Gringotts.Domain
{
    [TestFixture]
    public class VentureSplitTest
    {
        [Test]
        public void ShouldCreateTwoVenturesOnSplit()
        {
            var venture = new Venture(new Name("venture-name"), new Amount(100), new Amount(10));
            var firstVentureName = new Name("new-venture-1");
            var secondVentureName = new Name("new-venture-2");
            var terms = new TermsOfSplit(new Percentage(0.8f), firstVentureName, secondVentureName);
            venture.AddOffer(new Investor(new Name("testName"), new Amount(1000)), new Amount(100));
            venture.Start();
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
            venture.AddOffer(new Investor(new Name("testName"), new Amount(1000)), new Amount(100));
            venture.Start();
            var ventures = venture.Split(terms);
            Assert.AreEqual(firstVentureName.GetValue(), ventures.First().Name);
            Assert.AreEqual(secondVentureName.GetValue(), ventures.Last().Name);
        }

        [Test]
        public void ShouldSplitOutlayMoneyAccordingToRatio()
        {
            var venture = new Venture(new Name("venture-name"), new Amount(100), new Amount(10));
            var firstVentureName = new Name("new-venture-1");
            var secondVentureName = new Name("new-venture-2");
            var percentage = new Percentage(0.2f);
            var terms = new TermsOfSplit(percentage, firstVentureName, secondVentureName);
            venture.AddOffer(new Investor(new Name("testName"), new Amount(1000)), new Amount(100));
            venture.Start();
            var ventures = venture.Split(terms);
            Assert.AreEqual(percentage.Apply(venture.Outlay), ventures.First().Outlay);
            Assert.AreEqual(percentage.ApplyRemaining(venture.Outlay), ventures.Last().Outlay);
        }

        [Test]
        public void ShouldSplitHoldingInvestmentsAccordingToRatio()
        {
            var outlay = new Amount(40);
            var venture = new Venture(new Name("Ventura"), outlay, new Amount(1));
            var investor0 = new Investor(new Name("Investor0"), new Amount(100));
            venture.AddOffer(investor0, new Amount(50));
            venture.Start();
            var firstVentureName = new Name("new-venture-1");
            var secondVentureName = new Name("new-venture-2");
            var percentage = new Percentage(0.2f);

            var terms = new TermsOfSplit(percentage, firstVentureName, secondVentureName);
            var ventures = venture.Split(terms);
            Assert.AreEqual(venture.HoldingValue.Denomination, ventures.Sum(n => n.HoldingValue.Denomination));
        }

        [Test]
        public void ShouldCloseTheVentureWhenAVentureSplits(){
            var outlay = new Amount(40);
            var venture = new Venture(new Name("Ventura"), outlay, new Amount(1));
            var investor0 = new Investor(new Name("Investor0"), new Amount(100));
            venture.AddOffer(investor0, new Amount(50));
            venture.Start();
            var firstVentureName = new Name("new-venture-1");
            var secondVentureName = new Name("new-venture-2");
            var percentage = new Percentage(0.2f);

            var terms = new TermsOfSplit(percentage, firstVentureName, secondVentureName);
            venture.Split(terms);

            Assert.IsTrue(venture.IsClosed());
        }

        [Test]
        public void NewVenturesShouldStartWhenAVentureSplits()
        {
            var outlay = new Amount(40);
            var venture = new Venture(new Name("Ventura"), outlay, new Amount(1));
            var investor0 = new Investor(new Name("Investor0"), new Amount(100));
            venture.AddOffer(investor0, new Amount(50));
            venture.Start();
            var firstVentureName = new Name("new-venture-1");
            var secondVentureName = new Name("new-venture-2");
            var percentage = new Percentage(0.2f);

            var terms = new TermsOfSplit(percentage, firstVentureName, secondVentureName);
            var newVentures = venture.Split(terms);

            Assert.IsTrue(newVentures.First().IsStarted());
            Assert.IsTrue(newVentures.Last().IsStarted());
        }

        [Test]
        public void ShouldNotBeAbleToSplitANonStartedVenture(){
            var outlay = new Amount(40);
            var venture = new Venture(new Name("Ventura"), outlay, new Amount(1));
            var investor0 = new Investor(new Name("Investor0"), new Amount(100));
            venture.AddOffer(investor0, new Amount(50));

            var firstVentureName = new Name("new-venture-1");
            var secondVentureName = new Name("new-venture-2");
            var percentage = new Percentage(0.2f);
            var terms = new TermsOfSplit(percentage, firstVentureName, secondVentureName);

            Assert.Throws<Exception>(()=>venture.Split(terms));
        }

        [Test]
        public void ShouldUpdateThePortfolioOfTheInvestorWhenVentureCloses(){
            var outlay = new Amount(50);
            var venture = new Venture(new Name("Ventura"), outlay, new Amount(1));
            var investor0 = new Investor(new Name("Investor0"), new Amount(100));
            venture.AddOffer(investor0, new Amount(50));
            venture.Start();
            var firstVentureName = new Name("new-venture-1");
            var secondVentureName = new Name("new-venture-2");
            var percentage = new Percentage(0.2f);

            var terms = new TermsOfSplit(percentage, firstVentureName, secondVentureName);
            var ventures = venture.Split(terms);

           Assert.IsFalse(investor0.HasInvestmentIn(venture));
           Assert.IsTrue(investor0.HasInvestmentIn(ventures.First()));
           Assert.IsTrue(investor0.HasInvestmentIn(ventures.Last()));
        }
    }
}
