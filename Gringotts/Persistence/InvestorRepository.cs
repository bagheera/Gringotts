using System;
using System.Collections.Generic;
using Gringotts.Domain;
using NHibernate;

namespace Gringotts.Persistence
{
	public class InvestorRepository
	{
		private ISession session;

	    public InvestorRepository(ISession session)
	    {
	        this.session = session;
	    }
	   

		public int Save(Investor newInvestor)
		{
            return (int)session.Save(newInvestor);
		}

		public Investor GetInvestorById(int id)
		{
            return session.Load<Investor>(id);
		}

        public IList<Investor> FetchAll()
        {
            IQuery query = session.CreateQuery("from Investor");
            return query.List<Investor>();
        }
	}
}