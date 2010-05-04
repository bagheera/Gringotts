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

        private ISession session;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            InitalizeSessionFactory();
        }

        #region Setup/Teardown

        [SetUp]
        public void SetUp()
        {
            session = CreateSession();
            session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            session.Transaction.Rollback();
            session.Dispose();
        }
        #endregion

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
        
    }
}
