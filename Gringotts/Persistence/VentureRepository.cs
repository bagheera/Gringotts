using System;
using System.Collections.Generic;
using Gringotts.Domain;
using NHibernate;

namespace Gringotts.Persistence
{
    public class VentureRepository
    {
        private ISession session;

        public VentureRepository(ISession session)
        {
            this.session = session;
        }

        public void Save(Venture venture)
        {
            session.Save(venture);
        }

        public IList<Venture> FetchAll()
        {
            IQuery query = session.CreateQuery("from Venture");            
            return query.List<Venture>();
        }

        public Venture GetVentureById(string id)
        {
            return session.Load<Venture>(id);
        }
    }
}
