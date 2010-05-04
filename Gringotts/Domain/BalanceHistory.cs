using System.Collections.Generic;
using System.Linq;
using Iesi.Collections.Generic;

namespace Gringotts.Domain{

    public class BalanceHistory{
        private ISet<BalanceEvent> events = new HashedSet<BalanceEvent>();

        public List<BalanceEvent> GetEvents(){
            return events.ToList();
        }

        public void AddEvent(BalanceEvent balanceEvent){
            events.Add(balanceEvent);
        }
    }
}