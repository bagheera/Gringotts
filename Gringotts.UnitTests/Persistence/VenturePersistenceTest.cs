using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Gringotts.Domain;
using Gringotts.Persistence;
using NHibernate;
using NUnit.Framework;
using System.Linq;

namespace Gringotts.Persistence
{
    [TestFixture]
    public class VentureTest : NHibernateInMemoryTestFixtureBase
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            InitalizeSessionFactory();
        }

        [Test]
        public void ShouldBeAbleToSaveAndLoadAVenture()
        {
            Name nameOfVenture = new Name("Ventura");
            Amount outlay = new Amount(100);
            Amount minInvestment = new Amount(1);
            Venture venture = new Venture(nameOfVenture, outlay, minInvestment);
            VentureRepository ventureRepository = new VentureRepository(session);

            ventureRepository.Save(venture);
            session.Flush();
            session.Evict(venture);

            IList<Venture> ventures = ventureRepository.FetchAll();
            
            Assert.Contains(venture, ventures as ICollection);
        }
    }
}
