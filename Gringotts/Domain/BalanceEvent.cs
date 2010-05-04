using System;

namespace Gringotts.Domain
{
    public class BalanceEvent
    {
        private string Id;
        public virtual string EventType { get; private set; }
        public virtual Amount Amount { get; private set; }

        public const String CREATE_INVESTOR = "Created Investor with a balance of $ ";
        public const String OFFER_ACCEPTED = "Offer Accepted for $ ";
        public const String OFFER_PARTIALLY_ACCEPTED = "Offer Partially Accepted for $ ";
        public const String OFFER_REJECTED = "Offer Rejected for $ ";
        public const String DIVIDEND_RECIEVED = "Divident Recieved for $ ";

        public BalanceEvent()
        {
            
        }

        public BalanceEvent(String eventType, Amount amount)
        {
            EventType = eventType;
            Amount = amount;
        }

        public virtual bool Equals(BalanceEvent other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.EventType, EventType) && Equals(other.Amount, Amount);
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
                return (EventType.GetHashCode() * 397) ^ Amount.GetHashCode();
            }
        }
    }

}