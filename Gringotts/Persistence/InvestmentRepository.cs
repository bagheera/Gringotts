using System.Collections.Generic;
using Gringotts.Domain;
using NHibernate;

namespace Gringotts.Persistence
{
    public class InvestmentRepository
    {
        private ISession session;

        public InvestmentRepository(ISession session)
        {
            this.session = session;
        }

        public void Save(Investment investment)
        {
            session.Save(investment);
        }

        public Investment GetInvestmentById(int id)
        {
            return session.Load<Investment>(id);
        }

        public IList<Investment> FetchAll()
        {
            IQuery query = session.CreateQuery("from Investment");
            return query.List<Investment>();
        }
    }
}