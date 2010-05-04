using System.Collections.Generic;
using Gringotts.Domain;
using NHibernate;
using NUnit.Framework;

namespace Gringotts.Persistence{
    [TestFixture]
    public class OfferRepositoryPersistentTest : NHibernateInMemoryTestFixtureBase{
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
        public void ShouldPersist(){

            const int corpus = 4000;
            const string investorName = "Jagan";
            var investor = new Investor(new Name(investorName), new Amount(corpus));
            var investorRepository = new InvestorRepository(session);
            
            investorRepository.Save(investor);

            const int outlay = 2000;
            const int minInvestment = 400;
            const string ventureName = "Ram Capitalists";
            var venture = new Venture(new Name(ventureName), new Amount(outlay), new Amount(minInvestment));
            var ventureRepository = new VentureRepository(session);
            ventureRepository.Save(venture);

            const int denomination = 500;
            var offer = new Offer(investor, new Amount(denomination), venture);
            var offerRepository = new OfferRepository(session);
            offerRepository.Save(offer);

            session.Flush();

            session.Evict(offer);
            session.Evict(investor);
            session.Evict(venture);

            IList<Offer> offers = offerRepository.FetchAll();
            Assert.AreEqual(1, offers.Count);
            Offer savedOffer = offers[0];
            
            //offer props
            Assert.AreEqual(denomination, savedOffer.Value.Denomination);

            //investor props
            Assert.AreEqual(investorName, savedOffer.Investor.Name.GetValue());
            
            //venture props
            Assert.AreEqual(ventureName, savedOffer.Venture.Name.GetValue());
            Assert.AreEqual(new Amount(outlay), savedOffer.Venture.Outlay);
            Assert.AreEqual(new Amount(minInvestment), savedOffer.Venture.MinInvestment);
        }
    }
}