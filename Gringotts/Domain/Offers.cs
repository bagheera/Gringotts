using System.Collections.Generic;
using System.Linq;

namespace Gringotts.Domain
{
    public class Offers
    {
        private readonly List<Offer> offers = new List<Offer>();

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