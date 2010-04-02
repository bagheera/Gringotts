using System;
using System.Collections.Generic;
using NHibernate;
using NUnit.Framework;
using Gringotts.Domain;

namespace Gringotts.Persistence
{
    [TestFixture]
    [Ignore]
    public class InvestmentRepositoryPersitenceTest : NHibernateInMemoryTestFixtureBase
    {
        private ISession session;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            InitalizeSessionFactory();
        }

        [SetUp]
        public void SetUp()
        {
            session = CreateSession();
        }

        [TearDown]
        public void TearDown()
        {
            session.Dispose();
        }

        [Test]
        public void Should_Persist()
        {
            Investor investor = new Investor(new Name("Investor 1"), new GringottsDate(DateTime.Today), new Amount(100));
            InvestorRepository investorRepository = new InvestorRepository();
            investorRepository.Session = session;
            investorRepository.Save(investor);

            Venture venture = new Venture(new Name("Ventura"), new Amount(100), new Amount(1));
            VentureRepository ventureRepository = new VentureRepository(session);
            ventureRepository.Save(venture);

            Investment investment = new Investment(investor, venture, new Amount(10));
            InvestmentRepository investmentRepository = new InvestmentRepository(session);
            investmentRepository.Save(investment);

            IList<Investment> investments = investmentRepository.FetchAll();
            Assert.Greater(investments.Count, 0);
        }

        [Test]
        public void Should_Be_Able_Save()
        {
            Investment investment =
                new Investment(new Investor(new Name("Investor"), new GringottsDate(DateTime.Now), new Amount(6000)),
                                new Venture(new Name("Venture"), new Amount(5000), new Amount(1250)), new Amount(400));
            Assert.AreEqual("expected", "actual");
        }
    }
}