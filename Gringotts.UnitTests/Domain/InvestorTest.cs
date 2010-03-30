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
			Amount _amount = new Amount(10);
			Investor investor = new Investor(name, date, _amount);
			Assert.AreEqual(10, investor.Corpus);
		}
	}
}