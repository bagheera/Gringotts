using System;
using System.Collections.Generic;
using Gringotts.Domain;
using NHibernate;
using NUnit.Framework;

namespace Gringotts.Persistence{
    [TestFixture]
    [Ignore]
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
        public void ShouldBeAbleSave(){
            var investment =
                new Investment(new Investor(new Name("Investor"), new Amount(6000)),
                               new Venture(new Name("Venture"), new Amount(5000), new Amount(1250)), new Amount(400));
            Assert.AreEqual("expected", "actual");
        }

        [Test]
        public void ShouldPersist(){
            var investor = new Investor(new Name("Investor 1"), new Amount(100));
            var investorRepository = new InvestorRepository();
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
    }
}