using System;

namespace Gringotts.Domain
{
    public class Investment
    {        
        private readonly Investor investor;
        
        public Investment(Investor investor  , Amount amount)
        {
            this.investor = investor;
            this.Value = amount;
        }

        public Amount Value { get; set; }

        public bool HasInvestor(Investor investor)
        {
            return this.investor.Equals(investor);
        }

        public void GiveReturn(Amount dividend)
        {
            investor.AcceptReturn(dividend);
        }
        public void CreditSurplus(Amount creditSurplus)
        {
            investor.AcceptSurplus(creditSurplus);
        }
    }
}