using System.Collections.Generic;
using Gringotts.Domain;
using NHibernate;
using NUnit.Framework;

namespace Gringotts.Persistence{
    [TestFixture]
    public class VentureEventPersistenceTest : NHibernateInMemoryTestFixtureBase {

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            InitalizeSessionFactory();
        }

        [Test]
        public void ShouldPersistandRetrieveVentureEvent(){

            VentureEvent ventureEvent = new VentureEvent(VentureEvent.STARTED, new Amount(1000));
            string id = (string) session.Save(ventureEvent);
            session.Evict(ventureEvent);

            IQuery query = session.CreateQuery("from VentureEvent");
            IList<VentureEvent> events = query.List<VentureEvent>();
            Assert.AreEqual(1, events.Count);
            Assert.AreEqual(ventureEvent, events[0]);
        }

        [Test]
        public void VerifyCascadeSaveOfVentureEventViaVenture()
        {
            Venture venture = new Venture(new Name("Ventura"), new Amount(100), new Amount(1));
            VentureHistory ventureHistory = venture.GetVentureHistory();
            VentureEvent ventureEventStarted = new VentureEvent(VentureEvent.STARTED, new Amount(1000));
            ventureHistory.AddEvent(ventureEventStarted);

            VentureRepository ventureRepository = new VentureRepository(session);

            ventureRepository.Save(venture);
            session.Flush();
            session.Evict(venture);

            IQuery query = session.CreateQuery("from VentureEvent");
            IList<VentureEvent> ventureEvents = query.List<VentureEvent>();
            Assert.IsTrue(ventureEvents.Contains(ventureEventStarted));
        }
    }
}