using System;

namespace Gringotts.Domain
{
    public class Investor
    {
        private readonly Name name;
        private readonly GringottsDate date;
        private Amount corpus;

        public Investor(Name name, GringottsDate date, Amount amount)
        {
            this.name = name;
            this.date = date;
            this.corpus = amount;
        }

        public Amount Corpus
        {
            get { return corpus; }
        }

        public void Pay(Amount amount)
        {
            this.corpus -= amount;
        }
    }
}