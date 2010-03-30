using System;
using System.Collections.Generic;
using Gringotts.Domain;
using NHibernate;

namespace Gringotts.Persistence
{
    class VentureRepository
    {
        private ISession Session;

        public VentureRepository(ISession session)
        {
            Session = session;
        }

        public void Save(Venture venture)
        {
            Session.Save(venture);
        }

        public IList<Venture> FetchAll()
        {
            IQuery query = Session.CreateQuery("from Venture");
            return query.List<Venture>();
        }
    }
}
