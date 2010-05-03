﻿using System;
using System.Collections.Generic;
using Gringotts.Domain;
using NHibernate;
using NUnit.Framework;

namespace Gringotts.Persistence{
    [TestFixture]
    public class InvestmentRepositoryPersitenceTest : NHibernateInMemoryTestFixtureBase{
        #region Setup/Teardown

        [SetUp]
        public void SetUp(){
            session = CreateSession();
        }

        [TearDown]
        public void TearDown(){
            session.Dispose();
        }

        #endregion

        private ISession session;

        [TestFixtureSetUp]
        public void TestFixtureSetUp(){
            InitalizeSessionFactory();
        }

        [Test]
        public void ShouldSaveAndLoadInvestment()
        {
            Investor investor = new Investor(new Name("Investor 1"), new Amount(100));
            InvestorRepository investorRepository = new InvestorRepository();
            investorRepository.Session = session;
            investorRepository.Save(investor);

            var venture = new Venture(new Name("Ventura"), new Amount(100), new Amount(1));
            var ventureRepository = new VentureRepository(session);
            ventureRepository.Save(venture);

            var investment = new Investment(investor, venture, new Amount(10));
            var investmentRepository = new InvestmentRepository(session);
            investmentRepository.Save(investment);

            IList<Investment> investments = investmentRepository.FetchAll();
            Assert.Greater(investments.Count, 0);
            Assert.AreEqual(new Amount(10), investments[0].Value);
        }

        [Test]
        public void ShouldSaveAndLoadInvestorPortfolios()
        {
            Investor investor = new Investor(new Name("Investor 1"), new Amount(100));
            InvestorRepository investorRepository = new InvestorRepository();
            investorRepository.Session = session;
            investorRepository.Save(investor);
            session.Flush();
            session.Evict(investor);


            Venture venture = new Venture(new Name("Ventura"), new Amount(100), new Amount(1));
            VentureRepository ventureRepository = new VentureRepository(session);
            ventureRepository.Save(venture);
            session.Flush();
            session.Evict(venture);

            Investment investment = new Investment(investor, venture, new Amount(10));
            InvestmentRepository investmentRepository = new InvestmentRepository(session);
            investmentRepository.Save(investment);
            session.Flush();
            session.Evict(investment);

            IList<Investment> investments = investmentRepository.FetchAll();
            Assert.Greater(investments.Count, 0);
            InvestorRepository repo = new InvestorRepository();
            repo.Session = session;
            Investor savedInvestor = repo.GetInvestorById(investor.Id);
            Assert.AreEqual(new Amount(10), savedInvestor.PortfolioValue);
        }

        [Test]
        public void ShouldSaveAndLoadVentureHoldings(){
            Investor investor = new Investor(new Name("Investor 1"), new Amount(100));
            InvestorRepository investorRepository = new InvestorRepository();
            investorRepository.Session = session;
            investorRepository.Save(investor);
            session.Flush();
            session.Evict(investor);


            Venture venture = new Venture(new Name("Ventura"), new Amount(100), new Amount(1));
            VentureRepository ventureRepository = new VentureRepository(session);
            ventureRepository.Save(venture);
            session.Flush();
            session.Evict(venture);

            Investment investment = new Investment(investor, venture, new Amount(20));
            InvestmentRepository investmentRepository = new InvestmentRepository(session);
            investmentRepository.Save(investment);
            session.Flush();
            session.Evict(investment);

            IList<Investment> investments = investmentRepository.FetchAll();
            Assert.Greater(investments.Count, 0);
            VentureRepository repo = new VentureRepository(session);
            IList<Venture> savedVentures = repo.FetchAll();
            Assert.AreEqual(1, savedVentures.Count);
            Assert.AreEqual(new Amount(20), savedVentures[0].Holding.Value);
        }

        [Test]
        public void ShouldSaveAndLoadMultipleInvestments()
        {
            Investor investor1 = new Investor(new Name("Investor 1"), new Amount(100));
            InvestorRepository investorRepository = new InvestorRepository();
            investorRepository.Session = session;
            investorRepository.Save(investor1);
            session.Flush();
            session.Evict(investor1);

            Investor investor2 = new Investor(new Name("Investor 2"), new Amount(200));
            investorRepository.Save(investor2);
            session.Flush();
            session.Evict(investor2);

            Venture venture1 = new Venture(new Name("Ventura"), new Amount(100), new Amount(1));
            VentureRepository ventureRepository = new VentureRepository(session);
            ventureRepository.Save(venture1);
            session.Flush();
            session.Evict(venture1);
            
            Venture venture2 = new Venture(new Name("Ventura1"), new Amount(150), new Amount(1));
            ventureRepository.Save(venture2);
            session.Flush();
            session.Evict(venture2);

            Investment investment1 = new Investment(investor1, venture1, new Amount(20));
            InvestmentRepository investmentRepository = new InvestmentRepository(session);
            investmentRepository.Save(investment1);
            session.Flush();
            session.Evict(investment1);            
            
            Investment investment2 = new Investment(investor1, venture2, new Amount(30));
            investmentRepository.Save(investment2);
            session.Flush();
            session.Evict(investment2);            
            
            Investment investment3 = new Investment(investor2, venture1, new Amount(40));
            investmentRepository.Save(investment3);
            session.Flush();
            session.Evict(investment3);            
            
            Investment investment4 = new Investment(investor2, venture2, new Amount(50));
            investmentRepository.Save(investment4);
            session.Flush();
            session.Evict(investment4);

            IList<Investment> investments = investmentRepository.FetchAll();
            Assert.AreEqual(4, investments.Count);
            InvestorRepository repo = new InvestorRepository();
            repo.Session = session;
            Investor savedInvestor = repo.GetInvestorById(investor1.Id);
            Assert.AreEqual(new Amount(50), savedInvestor.PortfolioValue);
            savedInvestor = repo.GetInvestorById(investor2.Id);
            Assert.AreEqual(new Amount(90), savedInvestor.PortfolioValue);
        }
   }
}