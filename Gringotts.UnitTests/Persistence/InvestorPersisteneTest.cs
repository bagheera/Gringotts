﻿using System;
using Gringotts.Domain;
using NHibernate;
using NUnit.Framework;

namespace Gringotts.Persistence
{
	[TestFixture]
	public class InvsetorPersistenceTest : NHibernateInMemoryTestFixtureBase
	{
		[TestFixtureSetUp]
		public void TestFixtureSetUp()
		{
			InitalizeSessionFactory();
		}

	    [Test]
		public void ShouldBeAbleToSaveInvestor()
		{
			string name = "Investor 1";
			DateTime date = DateTime.Today;
			int amount = 100;
			
			Investor investor = new Investor(new Name(name),  new Amount(amount));

            InvestorRepository investorRepository = new InvestorRepository(session);
            string newId = investorRepository.Save(investor);
			
			Investor newInvestor = investorRepository.GetInvestorById(newId);
			Assert.AreEqual(new Name(name), newInvestor.Name);
		}

	}
}
