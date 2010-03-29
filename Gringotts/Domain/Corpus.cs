namespace Gringotts.Domain
{
	public class Corpus
	{
		private readonly int amount;

		public Corpus(int amount)
		{
			this.amount = amount;
		}

		public int Amount
		{
			get {
				return amount;
			}
		}
	}
}