using System;
using System.Linq;
using System.Collections.Generic;

namespace Gringotts.Domain
{
    public class Subscription
    {
        private readonly List<Offer> subscription = new List<Offer>();

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

        public void Add(Offer investment)
        {
            subscription.Add(investment);
        }

        public List<Investment> Confirm(Amount outlay)
        {
            var sortedInvestments = subscription.OrderBy(inv => inv.Value);
            Amount difference = outlay;
            List<Investment> finalSubscription = new List<Investment>();
            Amount zero = new Amount(0);
            foreach(Offer offer in sortedInvestments)
            {
                Investment investment = offer.ToInvestment();
                finalSubscription.Add(investment);
                difference -= offer.Value;
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