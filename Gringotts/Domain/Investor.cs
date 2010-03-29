namespace Gringotts.Domain
{
	public class Investor
	{
		private readonly Name name;
		private readonly GringottsDate date;
		private readonly Corpus corpus;

		public Investor(Name name, GringottsDate date, Corpus corpus)
		{
			this.name = name;
			this.date = date;
			this.corpus = corpus;
		}

		public int Corpus
		{
			get { return corpus.Amount; }
		}
	}
}