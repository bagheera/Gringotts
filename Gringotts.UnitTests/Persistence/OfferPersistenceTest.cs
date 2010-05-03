using System;
using System.Collections.Generic;
using Gringotts.Domain;
using NHibernate;
using NUnit.Framework;

namespace Gringotts.Persistence{

    [TestFixture]
    public class OfferRepositoryPersistentTest : NHibernateInMemoryTestFixtureBase{

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
        public void ShouldPersist(){
            Investor investor = new Investor(new Name("Jagan"), new GringottsDate(DateTime.Today), new Amount(2000M));
            InvestorRepository investorRepository = new InvestorRepository();
            investorRepository.Session = session;
            investorRepository.Save(investor);

            Venture venture = new Venture(new Name("Ram Capitalists"), new Amount(2000), new Amount(400));
            VentureRepository ventureRepository = new VentureRepository(session);
            ventureRepository.Save(venture);

            Offer offer = new Offer(investor, new Amount(500), venture);
            OfferRepository offerRepository = new OfferRepository(session);
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