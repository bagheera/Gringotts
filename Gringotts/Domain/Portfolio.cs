using System;
using System.Collections.Generic;
using System.Linq;
using Iesi.Collections.Generic;

namespace Gringotts.Domain
{
    public class Portfolio
    {
        private ISet<Investment> investments { get; set; }
        
        public Portfolio()
        {
            investments = new HashedSet<Investment>();
        }
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

        public void RemoveInvestment(Investment investment){
            investments.Remove(investment);
        }

        public bool	HasInvestmentIn(Venture venture){
            IEnumerable<Investment> commonInvestments = investments.Intersect(venture.Holding.Investments);
            if (commonInvestments.Count() > 0) return true;
            return false;
        }
    }
}