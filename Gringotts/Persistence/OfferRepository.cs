using System.Collections.Generic;
using Gringotts.Domain;
using NHibernate;

namespace Gringotts.Persistence{
    public class OfferRepository{

        private ISession Session;

        public OfferRepository(ISession session)
        {
            Session = session;
        }

        public void Save(Offer offer)
        {
            Session.Save(offer);
        }

        public IList<Offer> FetchAll()
        {
            IQuery query = Session.CreateQuery("from Offer");            
            return query.List<Offer>();
        }
    }
}