using System;
using System.IO;
using Gringotts.Domain;
using NHibernate;
using NUnit.Framework;

namespace Gringotts.Persistence
{
	[TestFixture]
	public class InvsetorPersistenceTest : NHibernateInMemoryTestFixtureBase
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
		public void Should_Be_Able_To_Save_Investor()
		{
			string name = "Investor 1";
			DateTime date = DateTime.Today;
			int amount = 100;
			
			Investor investor = new Investor(new Name(name), new GringottsDate(date), new Amount(amount));
			
			InvestorRepository investorRepository = new InvestorRepository();
			investorRepository.Session = session;
			int newId = investorRepository.Save(investor);
			
			Investor newInvestor = investorRepository.GetInvestorById(newId);
			Assert.AreEqual(name, newInvestor.Name);
		}

	}
}
