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

        public bool AlreadyInvested(Investor investor)
        {
            return subscription.Any(investment => investment.HasInvestor(investor));
        }
    }
}