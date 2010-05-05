using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Iesi.Collections.Generic;

namespace Gringotts.Domain
{
    public class Holding
    {
        private readonly ISet<Investment> investments = new HashedSet<Investment>();

        internal List<Investment> Investments
        {
            get { return investments.ToList(); }
        }

        public Amount Value
        {
            get
            {
                return investments.Aggregate(new Amount(0), (amount, investment) => amount + investment.Value);
            }

        }

        public void Add(Investment investment)
        {
            investments.Add(investment);
        }

        public void	DistributeDividends(Amount amount)
        {
            Dictionary<Investment, Amount> participation = CalculateParticipation();
            foreach (var participant in participation)
            {
                participant.Key.GiveReturn(participant.Key.Venture, participant.Value * amount);
            }
        }

        public void AddRange(List<Investment> confirmedInvestments)
        {
            foreach (Investment inv in confirmedInvestments){
                investments.Add(inv);
            }
        }

        private Dictionary<Investment, Amount> CalculateParticipation()
        {
            Amount totalInvestment = investments.Aggregate(new Amount(0), (total, investment) => total + investment.Value);
            Dictionary<Investment, Amount> participation = new Dictionary<Investment, Amount>();
            Investments.ForEach(investment => participation.Add(investment, totalInvestment/investment.Value));
            
            return participation;
        }
        
        public IEnumerable<Holding> Split(Percentage percentage)
        {
            IList<Holding> holdings = new List<Holding>();
            var aHolding = new Holding();
            foreach (var investment in Investments)
            {
                aHolding.Add(new Investment(investment.Investor, percentage.Apply(investment.Value)));
            }
            var aSecondHolding = new Holding();
            foreach (var investment in Investments)
            {
                aSecondHolding.Add(new Investment(investment.Investor, percentage.ApplyRemaining(investment.Value)));
            }
            
            holdings.Add(aHolding);
            holdings.Add(aSecondHolding);

            return holdings;
        }
    }
}