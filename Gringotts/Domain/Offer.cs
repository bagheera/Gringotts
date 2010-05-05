using System;

namespace Gringotts.Domain
{
    public class Offer
    {
        private string id;

        public virtual string Id
        {
            get
            {
                return id;
            }
        }

        public Offer(){
            // for hibernate
        }

        private readonly Investor investor;
        private readonly Venture venture;

        public Offer(Investor investor, Amount amount, Venture venture)
        {
            this.investor = investor;
            this.venture = venture;
            Value = amount;
        }

        public virtual Investor Investor
        {
            get { return investor; }
        }

        public virtual Venture Venture
        {
            get { return venture; }
        }

        public virtual Amount Value { get; set; }

        public virtual string VentureName{
            get { return Venture.Name.GetValue(); }
        }

        public virtual bool HasInvestor(Investor investor)
        {
            return this.investor.Equals(investor);
        }

        public virtual Investment ToInvestment()
        {
            return ToInvestment(new Amount(0));
        }

        public virtual Investment ToInvestment(Amount difference)
        {
            return new Investment(investor, venture, Value - difference);
        }
    }
}