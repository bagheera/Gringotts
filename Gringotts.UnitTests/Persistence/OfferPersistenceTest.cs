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
            var investor = new Investor(new Name("Jagan"), new Amount(2000));
            var investorRepository = new InvestorRepository();
            investorRepository.Session = session;
            investorRepository.Save(investor);

            var venture = new Venture(new Name("Ram Capitalists"), new Amount(2000), new Amount(400));
            var ventureRepository = new VentureRepository(session);
            ventureRepository.Save(venture);

            var offer = new Offer(investor, new Amount(500), venture);
            var offerRepository = new OfferRepository(session);
            offerRepository.Save(offer);

            IList<Offer> offers = offerRepository.FetchAll();
            Assert.AreEqual(1, offers.Count);
            Offer savedOffer = offers[0];
            Assert.AreEqual(500, savedOffer.Value.Denomination);
            Assert.AreEqual("Jagan", savedOffer.Investor.Name.GetValue());
            Assert.AreEqual("Ram Capitalists", savedOffer.Venture.Name.GetValue());
        }
    }
}