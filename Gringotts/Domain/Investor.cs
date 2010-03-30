using System;

namespace Gringotts.Domain
{
	public class Investor
	{
		private int id;
		private readonly Name name;
		private readonly GringottsDate date;
		private Amount corpus;

		public Investor() { } // For NHibernate

		public Investor(Name name, GringottsDate date, Amount amount)
		{
			this.name = name;
			this.date = date;
			corpus = amount;
		}

		public virtual Amount Corpus
		{
			get { return corpus; }
		}

		public virtual void Pay(Amount amount)
		{
			corpus -= amount;
		}

		public virtual string Name
		{
			get { return name.GetValue(); }
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