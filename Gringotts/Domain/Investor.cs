using System;

namespace Gringotts.Domain
{
	public class Investor
	{
		private int id;

		private readonly Name name;
		private readonly GringottsDate date;
		private readonly Corpus corpus;

		public Investor() {} // For NHibernate

		public Investor(Name name, GringottsDate date, Corpus corpus)
		{
			this.name = name;
			this.date = date;
			this.corpus = corpus;
		}

		public virtual int Corpus
		{
			get { return corpus.Amount; }
		}

		public virtual int Id
		{
			get { return id; }
		}

		public virtual string Name
		{
			get { return name.GetValue(); }
		}
	}
}