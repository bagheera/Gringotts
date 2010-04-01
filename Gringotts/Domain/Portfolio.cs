using System.Collections.Generic;
using System.Linq;

namespace Gringotts.Domain
{
    public class Portfolio
    {
        private List<Investment> investments = new List<Investment>();

        public Amount Value
        {
            get
            {
                return investments.Aggregate(new Amount(0), (amount, investment) => amount + investment.Value);
            }

        }

        public void AddInvestment(Investment investment)
        {
            investments.Add(investment);
        }
    }
}