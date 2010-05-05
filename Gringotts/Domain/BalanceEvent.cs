using System;

namespace Gringotts.Domain
{
    public class BalanceEvent
    {
        private string Id;
        public virtual string EventType { get; private set; }
        public virtual Amount Balance { get; private set; }

        public const String INVESTOR_CREATED = "Investor Created";
        public const String OFFER_ACCEPTED = "Offer Accepted for venture {0}";
        public const String OFFER_PARTIALLY_ACCEPTED = "Offer Partially Accepted for venture {0}";
        public const String OFFER_REJECTED = "Offer Rejected by venture {0}";
        public const String DIVIDEND_RECIEVED = "Divident Recieved from venture {0}";

        public BalanceEvent()
        {
            
        }

        public BalanceEvent(String eventType, Amount amount)
        {
            EventType = eventType;
            Balance = amount;
        }

        public virtual bool Equals(BalanceEvent other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.EventType, EventType) && Equals(other.Balance, Balance);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(BalanceEvent)) return false;
            return Equals((BalanceEvent)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (EventType.GetHashCode() * 397) ^ Balance.GetHashCode();
            }
        }
    }

}