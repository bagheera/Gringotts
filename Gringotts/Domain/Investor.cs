namespace Gringotts.Domain
{
    public class Investor
    {
        private int id;
        private readonly Name name;
        private readonly GringottsDate date;
        private readonly Portfolio portfolio = new Portfolio();

        public Investor() { } // For NHibernate

        public Investor(Name name, GringottsDate date, Amount amount)
        {
            this.name = name;
            this.date = date;
            Corpus = amount;
        }

        public virtual Amount Corpus { get; private set; }

        private void Pay(Amount amount)
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

        public virtual Amount PortfolioValue
        {
            get { return portfolio.Value;}
        }

        public virtual bool Equals(Investor other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.id == id && Equals(other.name, name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Investor)) return false;
            return Equals((Investor)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (id * 397) ^ (name != null ? name.GetHashCode() : 0);
            }
        }

        public virtual void AcceptSurplus(Amount surplus)
        {
            Corpus += surplus;
        }

        private void AddInvestmentToPortfolio(Investment investment)
        {
            portfolio.AddInvestment(investment);
        }

        public virtual void AcceptInvestment(Investment investment)
        {
            Pay(investment.Value);
            AddInvestmentToPortfolio(investment);
        }

        public virtual void AcceptReturn(Amount dividend)
        {
            Corpus += dividend;
        }
    }
}