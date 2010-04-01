using System;
using NHibernate;
using NUnit.Framework;
using Gringotts.Domain;

namespace Gringotts.Persistence
{
    [TestFixture]
    public class InvestmentPersistenceTest : NHibernateInMemoryTestFixtureBase
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
        }
    }
}
