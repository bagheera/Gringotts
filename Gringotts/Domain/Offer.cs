namespace Gringotts.Domain
{
    public class Offer
    {        
        private readonly Investor investor;
        private readonly Venture venture;

        public Offer(Investor investor, Amount amount, Venture venture)
        {
            this.investor = investor;
            this.venture = venture;
            this.Value = amount;
        }

        public Amount Value { get; set; }

        public bool HasInvestor(Investor investor)
        {
            return this.investor.Equals(investor);
        }
    }
}