using System;
using System.Linq;

namespace Gringotts.Domain
{
    public class Venture
    {
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
        }

        public Venture()
        {
        }

        internal virtual string Id { get; set; }
        internal virtual Name Name { get; set; }
        internal virtual Amount Outlay { get; set; }
        internal virtual Amount MinInvestment { get; set; }
        public virtual Subscription Subscription { get; set; }

        public virtual Investment AddOffer(Investor investor, Amount investedAmount)
        {
            Investment investment = new Investment(investedAmount);
            investor.Pay(investedAmount);
            Subscription.Add(investment);
            return investment;
        }

        public virtual bool Equals(Venture other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Id, Id) && Equals(other.Name, Name) && Equals(other.Outlay, Outlay) && Equals(other.MinInvestment, MinInvestment);
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
                return result;
            }
        }

        public virtual Amount SubscribedAmount()
        {
            return Subscription.Value;
        }
    }
}
