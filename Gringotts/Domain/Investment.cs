namespace Gringotts.Domain
{
    public class Investment
    {        
        private readonly Investor investor;
        private readonly Venture venture;
        private string id;

        public virtual string Id
        {
            get
            {
                return id;
            }
        }

        public Investment(Investor investor, Amount amount) : this (investor, null, amount)
        {
        }

        public Investment(Investor investor, Venture venture, Amount amount)
        {
            this.investor = investor;
            this.venture = venture;
            this.Value = amount;
            this.venture = venture;
        }

        public Investment() {}

        public virtual Amount Value { get; set; }

        public virtual bool HasInvestor(Investor investor)
        {
            return this.investor.Equals(investor);
        }

        public virtual void GiveReturn(Amount dividend)
        {
            investor.AcceptReturn(dividend);
        }

        public virtual void CreditSurplus(Amount creditSurplus)
        {
            investor.AcceptSurplus(creditSurplus);
        }

        public virtual bool Equals(Investment other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.investor, investor) && Equals(other.venture, venture) && Equals(other.Value, Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Investment)) return false;
            return Equals((Investment) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (investor != null ? investor.GetHashCode() : 0);
                result = (result*397) ^ (venture != null ? venture.GetHashCode() : 0);
                result = (result*397) ^ (Value != null ? Value.GetHashCode() : 0);
                return result;
            }
        }
    }
}