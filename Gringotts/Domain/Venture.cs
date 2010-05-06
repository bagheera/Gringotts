using System;
using System.Collections.Generic;

namespace Gringotts.Domain
{
    public class Venture
    {
        private Holding holding;
        private VentureHistory ventureHistory = new VentureHistory();
        public const string PROPOSED_STATE = "Proposed";
        public const string STARTED_STATE = "Started";
        public const string CANCELLED_STATE = "Cancelled";
        public const string CLOSED_STATE = "Closed";
        public const string BANKRUPT_STATE = "Bankrupt";



        public virtual VentureHistory GetVentureHistory()
        {
            return ventureHistory;
        }

        public Venture(Name name, Amount outlay, Amount minInvestment)
            : this(name, outlay)
        {
            if (minInvestment <= new Amount(0))
                throw new Exception("Minimum investment must be greater than 0");
            if (outlay < minInvestment)
                throw new Exception("Outlay must be greater than minimum investment");
            MinInvestment = minInvestment;
            State = PROPOSED_STATE;
            AddEventToVentureHistory(VentureEvent.PROPOSED);
        }

        private Venture(Name name, Amount outlay)
        {
            Name = name;
            Outlay = outlay;
            MinInvestment = new Amount(0);
            Subscription = new Subscription();
            
            holding = new Holding();
            
        }

        public Venture()
        {
            holding = new Holding();
        }

        public virtual string Id { get; private set; }
        public virtual Name Name { get; private set; }
        public virtual Amount Outlay { get; private set; }
        public virtual Amount MinInvestment { get; private set; }
        public virtual Subscription Subscription { get; set; }
        public virtual String State { get; private set; }

        public virtual Holding Holding
        {
            get
            {
                return holding;
            }
        }

        public virtual Amount HoldingValue
        {
            get { return holding.Value; }
        }

        public virtual Offer AddOffer(Investor investor, Amount investedAmount)
        {
            if (Subscription.AlreadyInvested(investor))
                throw new InvalidOfferException("Cannot invest more than once.");
            if (!MinimumInvestment(investedAmount))
                throw new InvalidOfferException("Investment amount less than the required minimum amount.");
            Offer offer = new Offer(investor, investedAmount, this);
            investor.AcceptOffer(offer);
            Subscription.Add(offer);
            return offer;
        }

        private bool MinimumInvestment(Amount investedAmount)
        {
            return investedAmount >= MinInvestment;
        }

        public virtual bool Equals(Venture other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Id, Id) && Equals(other.Name, Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Venture)) return false;
            return Equals((Venture)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Id != null ? Id.GetHashCode() : 0) * 397) ^ (Name != null ? Name.GetHashCode() : 0);
            }
        }

        public virtual Amount SubscribedAmount()
        {
            return Subscription.Value;
        }

        public virtual void ChangeStateToCancelled()
        {
            State = CANCELLED_STATE;
            AddEventToVentureHistory(VentureEvent.CANCELLED);
        }

        public virtual void ChangeStateToStarted()
        {
            State = STARTED_STATE;
        }

        public virtual void HandOutDividends(Amount dividend)
        {
            if (!IsStarted())
                throw new InvalidOperationException("Cannot hand out dividends for an un-started venture");
            holding.DistributeDividends(dividend);
        }

        public virtual bool IsStarted()
        {
            return State == STARTED_STATE;
        }

        public virtual bool IsClosed()
        {
            return State == CLOSED_STATE;
        }

        public virtual void Start()
        {
            if (!IsProposed())
                throw new Exception("Venture can only start from Proposed State");
            if (Subscription.Value < Outlay)
            {
                State = CANCELLED_STATE;
                throw new Exception("Venture cannot start with Total Subscription less than Outlay");
            }
            AddInvestmentsToHolding(Subscription.Confirm(Outlay));

            AddInvestmentsToPortfolio(Holding.Investments);
            ChangeStateToStarted();
        }

        private void AddInvestmentsToPortfolio(IEnumerable<Investment> investments)
        {
            foreach (var investment in investments)
            {
                Investor investor = investment.Investor;
                investor.AddInvestmentToPortfolio(investment);
            }
        }

        private void AddInvestmentsToHolding(List<Investment> investments)
        {
            Holding.AddRange(investments);
            State = STARTED_STATE;
            AddEventToVentureHistory(VentureEvent.STARTED);
        }

        private void AddEventToVentureHistory(String eventType)
        {
            ventureHistory.AddEvent(new VentureEvent(eventType, new Amount(Outlay.Denomination)));
        }

        public virtual bool IsProposed()
        {
            return State == PROPOSED_STATE;
        }

        public virtual void GoBankrupt()
        {
            if (!IsStarted())
                throw new InvalidOperationException("Cannot Go Bankrupt if not started");
            State = BANKRUPT_STATE;
            foreach (var investment in holding.Investments)
            {
                investment.Investor.NotifyVentureBankruptcy(investment);
            }
            AddEventToVentureHistory(VentureEvent.BANKRUPT);
        }

        public virtual IEnumerable<Venture> Split(TermsOfSplit termsOfSplit)
        {
           // Splitting of Holding's Investments
           // Splitting of OutLay
            var aVentures = new List<Venture>();
            var aFirstVenture = new Venture(termsOfSplit.FirstVentureName,
                termsOfSplit.Ratio.Apply(Outlay));
            var aSecondVenture = new Venture(termsOfSplit.SecondVentureName,
                termsOfSplit.Ratio.ApplyRemaining(Outlay));

            // Splitting of Holding's Investments
            var holdings = Holding.Split(termsOfSplit.Ratio);
            aFirstVenture.holding = holdings[0];
            aSecondVenture.holding = holdings[1];

            CloseTheVenture();
            aFirstVenture.ChangeStateToStarted();
            aSecondVenture.ChangeStateToStarted();

            aVentures.Add(aFirstVenture);
            aVentures.Add(aSecondVenture);
            return aVentures;
        }

        private void CloseTheVenture(){
            State = CLOSED_STATE;
        }
    }
}