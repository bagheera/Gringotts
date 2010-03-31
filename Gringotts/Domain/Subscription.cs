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
                return subscription.Aggregate(new Amount(0), (amt, inv) => amt + inv.Value);
            }
        }

        public void Add(Investment investment)
        {
            subscription.Add(investment);
        }
        public List<Investment> Confirm(Amount outlay)
        {
            var sortedInvestments = subscription.OrderBy(inv => inv.Value.Value);
            int currentSubscriptionTotal = 0;
            List<Investment> finalSubscription = new List<Investment>();
            foreach(Investment investment in sortedInvestments)
            {
                if(outlay.Value <= currentSubscriptionTotal) break;
                finalSubscription.Add(investment);
                currentSubscriptionTotal += investment.Value.Value;
            }

            return finalSubscription;
        }
    }
}