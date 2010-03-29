using NUnit.Framework;

namespace Gringotts.Domain
{
	[TestFixture]
	public class InvestorTest
	{
		[Test]
		public void Can_Create_Investor()
		{
			Name name = new Name("Investor 1");
			GringottsDate date = new GringottsDate(System.DateTime.Now);
			Corpus corpus = new Corpus(10);
			Investor investor = new Investor(name, date, corpus);
			Assert.AreEqual(10, investor.Corpus);
		}
	}
}