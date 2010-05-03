using System;
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
        public void ShouldPersistInvestment()
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
        }

        [Test]
        public void ShouldSaveAndLoadInvestment()
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
            Assert.AreEqual( new Amount(10),investments[0].Value);
            InvestorRepository repo = new InvestorRepository();
            repo.Session = session;
            Investor savedInvestor = repo.GetInvestorById(investor.Id);
            Assert.AreEqual(new Amount(10), savedInvestor.PortfolioValue);
        }
   }
}