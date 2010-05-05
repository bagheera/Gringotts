using System.Collections.Generic;
using System.Linq;
using Iesi.Collections.Generic;

namespace Gringotts.Domain{

    public class VentureHistory{

        private ISet<VentureEvent> events = new HashedSet<VentureEvent>();

        public List<VentureEvent> GetEvents()
        {
            return events.ToList();
        }

        public void AddEvent(VentureEvent ventureEvent)
        {
            events.Add(ventureEvent);
        }
            
    }
}