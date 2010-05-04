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
        public void ShouldFetchInvestorAndVentureForOffer(){
            Investor investor = CreateInvestor();
            var investorRepository = new InvestorRepository(session);

            investorRepository.Save(investor);

            Venture venture = CreateVenture();
            int minInvestment;
            int outlay;
            string ventureName;
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
            Assert.AreEqual(investor.Name.GetValue(), savedOffer.Investor.Name.GetValue());

            //venture props
            Assert.AreEqual(venture.Name.GetValue(), savedOffer.Venture.Name.GetValue());
            Assert.AreEqual(venture.Outlay, savedOffer.Venture.Outlay);
            Assert.AreEqual(venture.MinInvestment, savedOffer.Venture.MinInvestment);
        }

        private Venture CreateVenture(){
            const int outlay = 2000;
            const int minInvestment = 400;
            const string ventureName = "Ram Capitalists";
            return new Venture(new Name(ventureName), new Amount(outlay), new Amount(minInvestment));
        }

        [Test]
        public void ShouldFetchOffersForInvestorAndVenture(){
            Investor investor = CreateInvestor();
            InvestorRepository investorRepository = new InvestorRepository(session);
            investorRepository.Save(investor);

            Venture venture = CreateVenture();
            VentureRepository ventureRepository = new VentureRepository(session);
            ventureRepository.Save(venture);

            Amount amount = new Amount(600);
            Offer offer = new Offer(investor, amount, venture);
            OfferRepository offerRepository = new OfferRepository(session);
            offerRepository.Save(offer);

            session.Flush();
            session.Evict(investor);
            session.Evict(venture);
            session.Evict(offer);

            Investor fetchedInvestor = investorRepository.GetInvestorById(investor.Id);
            Assert.AreEqual(amount, fetchedInvestor.OfferValue);

            Venture fetchedVenture = ventureRepository.GetVentureById(venture.Id);
            Assert.AreEqual(amount, fetchedVenture.SubscribedAmount());
        }


        private Investor CreateInvestor(){
            const int corpus = 4000;
            const string investorName = "Jagan";
            return new Investor(new Name(investorName), new Amount(corpus));
        }

    }
}