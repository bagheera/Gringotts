using System;
using System.Collections.Generic;
using System.IO;
using Gringotts.Domain;
using NHibernate;
using NUnit.Framework;
using System.Linq;

namespace Gringotts.Persistence
{
    [TestFixture]
    public class VentureTest : NHibernateInMemoryTestFixtureBase
    {
        private ISession session;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            InitalizeSessionFactory(new FileInfo("Domain/Venture.hbm.xml"));
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
        public void Should_Be_Able_To_Save_And_Load_A_Venture()
        {
            String nameOfVenture = "Venture-1";
            int overlay = 100;
            Venture venture = new Venture { Name = nameOfVenture, Outlay = overlay };
            session.Save(venture);
            IQuery query = session.CreateQuery("from Venture");
            IList<Venture> ventures = query.List<Venture>();
            foreach (Venture loopVenture in ventures)
            {
                Console.WriteLine("{0} {1} {2}", loopVenture.Id, loopVenture.Name, loopVenture.Outlay);
            }

            Assert.AreEqual(1, ventures.Count);
            Assert.AreEqual(venture.Name, ventures.First().Name);
            Assert.IsNotNull(ventures.First().Id);
        }
    }
}
