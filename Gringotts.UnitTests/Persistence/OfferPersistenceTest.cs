using System.Collections.Generic;
using Gringotts.Domain;
using NHibernate;
using NUnit.Framework;

namespace Gringotts.Persistence{
    [TestFixture]
    public class OfferRepositoryPersistentTest : NHibernateInMemoryTestFixtureBase{
        
        [TestFixtureSetUp]
        public void TestFixtureSetUp(){
            InitalizeSessionFactory();
        }

        [Test]
        public void ShouldFetchInvestorAndVentureForOffer(){
            Investor investor = CreateInvestor("Jagan", 4000);
            var investorRepository = new InvestorRepository(session);

            investorRepository.Save(investor);

            Venture venture = CreateVenture(2000, 400, "Ram Capitalists");
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

        private Venture CreateVenture(int outlay, int minInvestment, string name){
            return new Venture(new Name(name), new Amount(outlay), new Amount(minInvestment));
        }

        [Test]
        public void ShouldFetchOffersForInvestorAndVenture(){
            Investor investor = CreateInvestor("Jagan", 4000);
            InvestorRepository investorRepository = new InvestorRepository(session);
            investorRepository.Save(investor);

            Venture venture = CreateVenture(2000, 400, "Ram Capitalists");
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

        [Test]
        public void ShouldFetchMultipleOffersForInvestor(){
            Investor investor = CreateInvestor("Jagan1", 5000);
            InvestorRepository investorRepository = new InvestorRepository(session);
            investorRepository.Save(investor);

            Venture venture1 = CreateVenture(3000, 500, "Ram1");
            Venture venture2 = CreateVenture(6000, 1000, "Ram2");
            VentureRepository ventureRepository = new VentureRepository(session);
            ventureRepository.Save(venture1);
            ventureRepository.Save(venture2);

            Amount amount1 = new Amount(700);
            Amount amount2 = new Amount(800);

            Offer offer1 = new Offer(investor, amount1, venture1);
            Offer offer2 = new Offer(investor, amount2, venture2);
            OfferRepository offerRepository = new OfferRepository(session);
            offerRepository.Save(offer1);
            offerRepository.Save(offer2);

            session.Flush();
            session.Clear();

            Investor fetchedInvestor = investorRepository.GetInvestorById(investor.Id);
            Assert.AreEqual(new Amount(1500), fetchedInvestor.OfferValue);
        }

        [Test]
        public void ShouldFetchMultipleSubscriptionsForVenture(){
            Investor investor1 = CreateInvestor("Joy", 9000);
            Investor investor2 = CreateInvestor("Roy", 6000);
            InvestorRepository investorRepository = new InvestorRepository(session);
            investorRepository.Save(investor1);
            investorRepository.Save(investor2);

            Venture venture = CreateVenture(8000, 1920, "Ace Ventura");
            VentureRepository ventureRepository = new VentureRepository(session);
            ventureRepository.Save(venture);

            Amount amount1 = new Amount(712);
            Amount amount2 = new Amount(423);

            Offer offer1 = new Offer(investor1, amount1, venture);
            Offer offer2 = new Offer(investor2, amount2, venture);
            OfferRepository offerRepository = new OfferRepository(session);
            offerRepository.Save(offer1);
            offerRepository.Save(offer2);

            session.Flush();
            session.Clear();

            Venture fetchedVenture = ventureRepository.GetVentureById(venture.Id);
            Assert.AreEqual(new Amount(1135), fetchedVenture.SubscribedAmount());
          
            
        }
        
        private Investor CreateInvestor(string name, int corpus){
            return new Investor(new Name(name), new Amount(corpus));
        }

    }
}