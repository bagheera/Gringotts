using System;
using System.Collections.Generic;
using System.Linq;

namespace Gringotts.Domain
{
    public class Holding
    {
        private readonly List<Investment> investments = new List<Investment>();

        internal List<Investment> Investments
        {
            get { return investments; }
        }

        public void Add(Investment investment)
        {
            investments.Add(investment);
        }

        public void	DistributeDividends(Amount amount)
        {
            CalculateParticipation();
        }

        public void AddRange(List<Investment> confirmedInvestments)
        {
            investments.AddRange(confirmedInvestments);
        }

        private Dictionary<Investment, Amount> CalculateParticipation()
        {
            Amount totalInvestment = investments.Aggregate(new Amount(0), (total, investment) => total + investment.Value);
            Dictionary<Investment, Amount> participation = new Dictionary<Investment, Amount>();
            //investments.ForEach(investment => participation.Add(investment, new Amount(totalInvestment / investment.Value)));
            
            return participation;
        }
    }
}