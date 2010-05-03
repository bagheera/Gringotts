using System.Collections.Generic;

namespace Gringotts.Domain{

    public class BalanceHistory{
        private readonly List<BalanceEvent> events = new List<BalanceEvent>();

        public List<BalanceEvent> GetEvents(){
            return events;
        }

        public void AddEvent(BalanceEvent balanceEvent){
            events.Add(balanceEvent);
        }
    }
}