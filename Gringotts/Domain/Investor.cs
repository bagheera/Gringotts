using System;

namespace Gringotts.Domain
{
    public class Investor
    {
        private string id;
        private readonly Name name;
        private readonly Portfolio portfolio = new Portfolio();
        private Offers offers = new Offers();
        private BalanceHistory balanceHistory = new BalanceHistory();
        public virtual Amount Balance { get; private set; }

        public virtual Name Name
        {
            get { return name; }
        }

        public virtual string Id
        {
            get
            {
                return id;
            }
        }

        public virtual Amount PortfolioValue
        {
            get { return portfolio.Value; }
        }

        public virtual Amount OfferValue
        {
            get { return offers.Value; }
        }

        public Investor() { } // For NHibernate

        public Investor(Name name, Amount amount)
        {
            this.name = name;
            Balance = amount;
            balanceHistory.AddEvent(new BalanceEvent(BalanceEvent.INVESTOR_CREATED, amount));
        }

        public virtual bool Equals(Investor other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.id, id) && Equals(other.name, name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Investor)) return false;
            return Equals((Investor) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((id != null ? id.GetHashCode() : 0)*397) ^ (name != null ? name.GetHashCode() : 0);
            }
        }


        public virtual void AcceptSurplus(Amount surplus)
        {
            Balance += surplus;
        }

        public virtual void AddInvestmentToPortfolio(Investment investment)
        {
            portfolio.AddInvestment(investment);
        }

        public virtual void AcceptOffer(Offer offer)
        {
            Pay(offer.Value);
            offers.AddOffer(offer);
            string offerEvent = string.Format(BalanceEvent.OFFER_ACCEPTED, offer.VentureName);
            balanceHistory.AddEvent(new BalanceEvent(offerEvent, Balance));
        }

        public virtual void AcceptReturn(Amount dividend)
        {
            Balance += dividend;
        }

        public virtual BalanceHistory GetBalanceHistory(){
            return balanceHistory;
        }

        private void Pay(Amount amount)
        {
            Balance -= amount;
        }
    }
}