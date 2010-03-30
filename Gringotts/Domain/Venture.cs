using System;
namespace Gringotts.Domain
{
    public class Venture
    {
        public Venture(Name	name, Amount outlay, Amount minInvestment)
        {            
            if(minInvestment <= new Amount(0))
                throw new Exception("Minimum investment must be greater than 0");
            if(outlay < minInvestment)
                throw new Exception("Outlay must be greater than minimum investment");
            Name = name;
            Outlay = outlay;
            MinInvestment = minInvestment;
        }

        public Venture()
        {
            
        }

        internal virtual string Id { get; set; }
        internal virtual Name Name { get; set; }
        internal virtual Amount Outlay { get; set; }
        internal virtual Amount MinInvestment { get; set; }

        public virtual Investment OfferToInvest(Investor investor, Amount amount)
        {
            investor.Pay(amount);
            return new Investment();
        }
    }
}
