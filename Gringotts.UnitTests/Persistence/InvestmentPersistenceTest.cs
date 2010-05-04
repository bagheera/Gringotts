using System.Collections.Generic;
using Gringotts.Domain;
using NHibernate;
using NUnit.Framework;

namespace Gringotts.Persistence{
    [TestFixture]
    public class InvestmentRepositoryPersitenceTest : NHibernateInMemoryTestFixtureBase{
        

        [TestFixtureSetUp]
        public void TestFixtureSetUp(){
            InitalizeSessionFactory();
        }

        [Test]
        public void ShouldSaveAndLoadInvestment()
        {
            Investor investor = new Investor(new Name("Investor 1"), new Amount(100));
            InvestorRepository investorRepository = new InvestorRepository(session);
            investorRepository.Save(investor);
            session.Flush();
            session.Evict(investor);

            var venture = new Venture(new Name("Ventura"), new Amount(100), new Amount(1));
            var ventureRepository = new VentureRepository(session);
            ventureRepository.Save(venture);
            session.Flush();
            session.Evict(venture);

            var investment = new Investment(investor, venture, new Amount(10));
            var investmentRepository = new InvestmentRepository(session);
            investmentRepository.Save(investment);
            session.Flush();
            session.Evict(investment);

            IList<Investment> investments = investmentRepository.FetchAll();
            Assert.Greater(investments.Count, 0);
            Assert.AreEqual(new Amount(10), investments[0].Value);
        }

        [Test]
        public void ShouldSaveAndLoadInvestorPortfolios()
        {
            Investor investor = new Investor(new Name("Investor 1"), new Amount(100));
            InvestorRepository investorRepository = new InvestorRepository(session);
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
            InvestorRepository repo = new InvestorRepository(session);
            Investor savedInvestor = repo.GetInvestorById(investor.Id);
            Assert.AreEqual(new Amount(10), savedInvestor.PortfolioValue);
        }

        [Test]
        public void ShouldSaveAndLoadVentureHoldings(){
            Investor investor = new Investor(new Name("Investor 1"), new Amount(100));
            InvestorRepository investorRepository = new InvestorRepository(session);
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
            Assert.AreEqual(new Amount(20), savedVentures[0].HoldingValue);
        }

        [Test]
        public void ShouldSaveAndLoadMultipleInvestments()
        {
            Investor investor1 = new Investor(new Name("Investor 1"), new Amount(100));
            InvestorRepository investorRepository = new InvestorRepository(session);
            investorRepository.Save(investor1);
            session.Flush();
            session.Evict(investor1);

            Investor investor2 = new Investor(new Name("Investor 2"), new Amount(200));
            investorRepository.Save(investor2);
            session.Flush();
            session.Evict(investor2);

            Venture venture1 = new Venture(new Name("Ventura 1"), new Amount(100), new Amount(1));
            VentureRepository ventureRepository = new VentureRepository(session);
            ventureRepository.Save(venture1);
            session.Flush();
            session.Evict(venture1);
            
            Venture venture2 = new Venture(new Name("Ventura 2"), new Amount(150), new Amount(1));
            ventureRepository.Save(venture2);
            session.Flush();
            session.Evict(venture2);

            Amount investmentAmount1 = new Amount(20);
            Investment investment1 = new Investment(investor1, venture1, investmentAmount1);
            InvestmentRepository investmentRepository = new InvestmentRepository(session);
            investmentRepository.Save(investment1);
            session.Flush();
            session.Evict(investment1);

            Amount investmentAmount2 = new Amount(30);
            Investment investment2 = new Investment(investor1, venture2, new Amount(30));
            investmentRepository.Save(investment2);
            session.Flush();
            session.Evict(investment2);

            Amount investmentAmount3 = new Amount(40);
            Investment investment3 = new Investment(investor2, venture1, new Amount(40));
            investmentRepository.Save(investment3);
            session.Flush();
            session.Evict(investment3);

            Amount investmentAmount4 = new Amount(50);
            Investment investment4 = new Investment(investor2, venture2, new Amount(50));
            investmentRepository.Save(investment4);
            session.Flush();
            session.Evict(investment4);

            IList<Investment> investments = investmentRepository.FetchAll();
            Assert.AreEqual(4, investments.Count);

            Investor savedInvestor = investorRepository.GetInvestorById(investor1.Id);
            Assert.AreEqual(investmentAmount1 + investmentAmount2, savedInvestor.PortfolioValue);
            savedInvestor = investorRepository.GetInvestorById(investor2.Id);
            Assert.AreEqual(investmentAmount3 + investmentAmount4, savedInvestor.PortfolioValue);

            Venture savedVenture = ventureRepository.GetVentureById(venture1.Id);
            Assert.AreEqual(investmentAmount1 + investmentAmount3, savedVenture.HoldingValue);
            savedVenture = ventureRepository.GetVentureById(venture2.Id);
            Assert.AreEqual(investmentAmount2+ investmentAmount4, savedVenture.HoldingValue);

        }
   }
}