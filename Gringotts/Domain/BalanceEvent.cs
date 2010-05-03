using System;

namespace Gringotts.Domain{
    public class BalanceEvent{
        public string EventType { get; private set; }
        public Amount Amount { get; private set; }
        public static String CREATE_INVESTOR = "Created Investor with a balance of $ ";
//        public static String OFFER_ACCEPTED = "Created Investor with a balance of $ ";
//        public static String OFFER_REJECTED = "Created Investor with a balance of $ ";
//        public static String DIVIDEND_RECIEVED = "Created Investor with a balance of $ ";

        public BalanceEvent(String eventType, Amount amount){
            EventType = eventType;
            Amount = amount;
        }

        public bool Equals(BalanceEvent other){
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.EventType, EventType) && Equals(other.Amount, Amount);
        }

        public override bool Equals(object obj){
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (BalanceEvent)) return false;
            return Equals((BalanceEvent) obj);
        }

        public override int GetHashCode(){
            unchecked{
                return (EventType.GetHashCode()*397) ^ Amount.GetHashCode();
            }
        }
    }
   
}