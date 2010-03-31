namespace Gringotts.Domain
{
	public class Investor
	{
		private int id;
		private readonly Name name;
		private readonly GringottsDate date;

		public Investor() { } // For NHibernate

		public Investor(Name name, GringottsDate date, Amount amount)
		{
			this.name = name;
			this.date = date;
			Corpus = amount;
		}

		public virtual Amount Corpus { get; private set; }

		public virtual void Pay(Amount amount)
		{
			Corpus -= amount;
		}

		public virtual Name Name
		{
			get { return name; }
		}

		public virtual int Id
		{
			get
			{
				return id;
			}
		}
	}
}