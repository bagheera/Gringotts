﻿using System;
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
        public void Should_Be_Able_To_Save_And_Load_A_Venture()
        {
            Name nameOfVenture = new Name("Ventura");
            Amount outlay = new Amount(100);
            Amount minInvestment = new Amount(1);
            Venture venture = new Venture(nameOfVenture, outlay, minInvestment);
            VentureRepository ventureRepository = new VentureRepository(session);

            Assert.IsNull(venture.Id);
            ventureRepository.Save(venture);            
            IList<Venture> ventures = ventureRepository.FetchAll();

            ventures.ToList().ForEach(v => Console.WriteLine("{0} {1} {2}", v.Id, v.Name.GetValue(), v.Outlay.Value));
            Assert.AreEqual(1, ventures.Count);
            Assert.AreEqual(venture.Name, ventures.First().Name);
            Assert.AreEqual(venture.MinInvestment, ventures.First().MinInvestment);
            Assert.AreEqual(venture.Outlay, ventures.First().Outlay);
            Assert.IsNotNull(ventures.First().Id);
        }
    }
}
