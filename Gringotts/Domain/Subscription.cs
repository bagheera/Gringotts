using System;
using System.Linq;
using System.Collections.Generic;

namespace Gringotts.Domain
{
    public class Subscription
    {
        private readonly List<Investment> subscription = new List<Investment>();

        public int Count
        {
            get { return subscription.Count; }
        }

        public Amount Value
        {
            get
            {
                return subscription.Aggregate(new Amount(0), (amount, investment) => amount + investment.Value);
            }
        }

        public void Add(Investment investment)
        {
            subscription.Add(investment);
        }

        public List<Investment> Confirm(Amount outlay)
        {
            var sortedInvestments = subscription.OrderBy(inv => inv.Value.Value);
            Amount difference = outlay;
            List<Investment> finalSubscription = new List<Investment>();
            Amount zero = new Amount(0);
            foreach(Investment investment in sortedInvestments)
            {
                finalSubscription.Add(investment);
                difference -= investment.Value;
                if (difference <= zero)
                {
                    investment.Value += difference;
                    investment.CreditSurplus(difference.Abs());
                    break;
                }
            }

            return finalSubscription;
        }

        public bool AlreadyInvested(Investor investor)
        {
            return subscription.Any(investment => investment.HasInvestor(investor));
        }
    }
}