using System.Collections.Generic;
using Gringotts.Domain;
using NHibernate;

namespace Gringotts.Persistence
{
    public class InvestmentRepository
    {
        private ISession Session;

        public InvestmentRepository(ISession session)
        {
            Session = session;
        }

        public void Save(Investment investment)
        {
            Session.Save(investment);
        }

        public IList<Venture> FetchAll()
        {
            //IQuery query = Session.CreateQuery("from In");            
            //return query.List<Venture>();
            return null;
        }
    }
}