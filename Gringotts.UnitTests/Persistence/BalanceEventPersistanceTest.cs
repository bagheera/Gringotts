using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gringotts.Domain;
using NHibernate;
using NUnit.Framework;

namespace Gringotts.Persistence
{
    [TestFixture]
    public class BalanceEventPersistanceTest : NHibernateInMemoryTestFixtureBase{

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            InitalizeSessionFactory();
        }

        [Test]
        public void ShouldVerifySaveAndLoadOfBalanceEvents()
        {
            BalanceEvent balanceEvent = new BalanceEvent(BalanceEvent.OFFER_ACCEPTED, new Amount(600));
            String id = (String)session.Save(balanceEvent);
            session.Evict(balanceEvent);

            IQuery query = session.CreateQuery("from BalanceEvent");
            IList<BalanceEvent> events = query.List<BalanceEvent>();
            Assert.AreEqual(1,events.Count);
            Assert.AreEqual(balanceEvent, events[0]);
        }

        [Test]
        public void VerifyCascadeSaveOfBalanceEventViaInvestor(){
            Investor investor = new Investor(new Name("dude"), new Amount(1000));
            BalanceHistory balanceHistory = investor.GetBalanceHistory();

            var venture = new Venture(new Name("Hacker's Venture"), new Amount(500), new Amount(500));
            var offerAmount = new Amount(500);
            venture.AddOffer(investor, offerAmount);
            var testBalanceEvent = new BalanceEvent(string.Format(BalanceEvent.OFFER_ACCEPTED,venture.Name.GetValue()), offerAmount);

            InvestorRepository investorRepository = new InvestorRepository(session);
            investorRepository.Save(investor);
            session.Evict(investor);

            IQuery query = session.CreateQuery("from BalanceEvent");
            IList<BalanceEvent> savedBalanceEvents = query.List<BalanceEvent>();
            Assert.IsTrue(savedBalanceEvents.Contains(testBalanceEvent));
        }
        
    }
}
