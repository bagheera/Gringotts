using System;

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
            Value = amount;
        }

        public Amount Value { get; set; }

        public bool HasInvestor(Investor investor)
        {
            return this.investor.Equals(investor);
        }

        public virtual Investment ToInvestment()
        {
            return ToInvestment(new Amount(0));
        }

        public virtual Investment ToInvestment(Amount difference)
        {
            return new Investment(investor, Value - difference, venture);
        }
    }
}