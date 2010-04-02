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
    }
}