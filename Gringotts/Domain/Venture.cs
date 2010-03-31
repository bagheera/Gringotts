using System;
using System.Linq;

namespace Gringotts.Domain
{
    public class Venture
    {
        private Holding holding;
        public const string PROPOSED_STATE = "Proposed";
        public const string STARTED_STATE = "Started";
        public const string CANCELLED_STATE = "Cancelled";
        public const string CLOSED_STATE = "Closed";
        //public static readonly string[] STATES = new string[] { PROPOSED_STATE, STARTED_STATE, CANCELLED_STATE, CLOSED_STATE };

        public Venture(Name name, Amount outlay, Amount minInvestment)
        {
            if (minInvestment <= new Amount(0))
                throw new Exception("Minimum investment must be greater than 0");
            if (outlay < minInvestment)
                throw new Exception("Outlay must be greater than minimum investment");
            Name = name;
            Outlay = outlay;
            MinInvestment = minInvestment;
            Subscription = new Subscription();
            State = PROPOSED_STATE;
            holding = new Holding();
        }

        public Venture()
        {
        }

        internal virtual string Id { get; set; }
        internal virtual Name Name { get; set; }
        internal virtual Amount Outlay { get; set; }
        internal virtual Amount MinInvestment { get; set; }
        public virtual Subscription Subscription { get; set; }
        public virtual String State { get; set; }

        public virtual Holding Holding
        {
            get {
                return holding;
            }
        }

        public virtual Investment AddOffer(Investor investor, Amount investedAmount)
        {
            if (MinimumInvestment(investedAmount))
            {
                Investment investment = new Investment(investedAmount);
                investor.Pay(investedAmount);
                Subscription.Add(investment);
                return investment;
            }
            throw new InvalidOfferException("Investment amount less than the required minimum amount.");
        }

        private bool MinimumInvestment(Amount investedAmount)
        {
            return investedAmount >= MinInvestment;
        }

        public virtual bool Equals(Venture other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Id, Id) && Equals(other.Name, Name) && Equals(other.Outlay, Outlay) && Equals(other.MinInvestment, MinInvestment) && Equals(other.State, State);
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
                int result = (Id != null ? Id.GetHashCode() : 0);
                result = (result * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                result = (result * 397) ^ (Outlay != null ? Outlay.GetHashCode() : 0);
                result = (result * 397) ^ (MinInvestment != null ? MinInvestment.GetHashCode() : 0);
                result = (result * 397) ^ (State != null ? State.GetHashCode() : 0);
                return result;
            }
        }
        public virtual Amount SubscribedAmount()
        {
            return Subscription.Value;
        }

        public virtual void ChangeStateToCancelled()
        {
            State = CANCELLED_STATE;
        }

        public virtual void ChangeStateToStarted()
        {
            State = STARTED_STATE;
        }

        public virtual void HandOutDividends()
        {
            if (!IsStarted())
                throw new Exception("Cannot hand out dividends for an un-started venture");
            Amount profits = GenerateProfits();
            //Send the profit generated to holding
            //Hope that Holding distributes the profits properly
        }

        private Amount GenerateProfits()
        {
            return new Amount(1000);
        }

        private bool IsStarted()
        {
            return State == STARTED_STATE;
        }

        public virtual void Start()
        {
            if (!IsProposed())
                throw new Exception("Venture can only start from Proposed State");
            if (Subscription.Value < Outlay)
                throw new Exception("Venture cannot start with Total Subscription less than Outlay");
        }

        public bool IsProposed()
        {
            return State == PROPOSED_STATE;
        }
    }
}