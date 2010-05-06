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

        public virtual void PartialRefundOnOffer(Amount surplus)
        {
            Balance += surplus;
        }

        public virtual void PartialRefundOnOffer(Offer offer, Amount refundAmount)
        {
            PartialRefundOnOffer(refundAmount);
            string offerEvent = string.Format(BalanceEvent.OFFER_PARTIALLY_ACCEPTED, offer.VentureName);
            balanceHistory.AddEvent(new BalanceEvent(offerEvent, Balance));
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

        public virtual void AcceptReturn(Venture venture, Amount dividend)
        {
            Balance += dividend;
            string offerEvent = string.Format(BalanceEvent.DIVIDEND_RECEIVED, venture.Name);
            balanceHistory.AddEvent(new BalanceEvent(offerEvent, Balance));
        }
        
        public virtual BalanceHistory GetBalanceHistory()
        {
            return new BalanceHistory(balanceHistory);
        }

        private void Pay(Amount amount)
        {
            Balance -= amount;
        }

        public virtual void	NotifyVentureBankruptcy(Investment investment){
            portfolio.RemoveInvestment(investment);
            String ventureBankruptEvent = String.Format(BalanceEvent.VENTURE_BANKRUPT,
                                                        investment.Venture.Name);
            balanceHistory.AddEvent(new BalanceEvent(ventureBankruptEvent, new Amount(Balance.Denomination)));
        }

        public virtual void OfferRejected(Offer offer){
            Balance += offer.Value;
            string offerEvent = string.Format(BalanceEvent.OFFER_REJECTED, offer.VentureName);
            balanceHistory.AddEvent(new BalanceEvent(offerEvent, Balance));
        }

        public virtual bool	HasInvestmentIn(Venture venture){
            return portfolio.HasInvestmentIn(venture);
        }

        public virtual void RemoveInvestmentFromPortfolio(Investment investment){
            portfolio.RemoveInvestment(investment);
        }
    }
}
