using System.Collections.Generic;
using System.Linq;
using Iesi.Collections.Generic;

namespace Gringotts.Domain
{
    public class Offers
    {
        private ISet<Offer> offers = new HashedSet<Offer>();

        public Amount Value
        {
            get
            {
                return offers.Aggregate(new Amount(0), (amount, investment) => amount + investment.Value);
            }

        }

        public void AddOffer(Offer offer)
        {
            offers.Add(offer);
        }
    }
}