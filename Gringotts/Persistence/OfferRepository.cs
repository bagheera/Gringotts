using System.Collections.Generic;
using Gringotts.Domain;
using NHibernate;

namespace Gringotts.Persistence{
    public class OfferRepository{

        private ISession session;

        public OfferRepository(ISession session)
        {
            this.session = session;
        }

        public void Save(Offer offer)
        {
            session.Save(offer);
        }

        public IList<Offer> FetchAll()
        {
            IQuery query = session.CreateQuery("from Offer");            
            return query.List<Offer>();
        }

        public Offer GetOfferById(int id)
        {
            return session.Load<Offer>(id);
        }
    }
}