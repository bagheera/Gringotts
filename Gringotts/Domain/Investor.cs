namespace Gringotts.Domain
{
	public class Investor
	{
		private readonly Name name;
		private readonly GringottsDate date;
		private readonly Amount _amount;

		public Investor(Name name, GringottsDate date, Amount _amount)
		{
			this.name = name;
			this.date = date;
			this._amount = _amount;
		}

		public int Corpus
		{
			get { return _amount.Value; }
		}
	}
}