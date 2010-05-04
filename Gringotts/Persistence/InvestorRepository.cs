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


        public string Save(Investor newInvestor)
		{
            return (string)session.Save(newInvestor);
		}

		public Investor GetInvestorById(string id)
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