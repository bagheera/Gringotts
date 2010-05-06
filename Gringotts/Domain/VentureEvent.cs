using System;

namespace Gringotts.Domain{

    public class VentureEvent{

        public const String CANCELLED = "Venture Cancelled";
        public const String STARTED = "Venture Started";
        public const String PROPOSED = "Venture Proposed";
        public const string BANKRUPT = "Venture Bankrupt";

        private string Id;
        
        public virtual string EventType { get; private set; }
        public virtual Amount Outlay { get; private set; }

        public VentureEvent(){
            //for hibernate
        }

        public VentureEvent(string eventType, Amount outlay){
            EventType = eventType;
            Outlay = outlay;
        }

        public virtual bool Equals(VentureEvent other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.EventType, EventType) && Equals(other.Outlay, Outlay);
        }

        public override bool Equals(object obj){
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (VentureEvent)) return false;
            return Equals((VentureEvent) obj);
        }

        public override int GetHashCode(){
            unchecked{
                return ((EventType != null ? EventType.GetHashCode() : 0)*397) ^ (Outlay != null ? Outlay.GetHashCode() : 0);
            }
        }
    }
}