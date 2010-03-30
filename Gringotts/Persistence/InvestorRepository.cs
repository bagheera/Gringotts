using System;
using System.Collections.Generic;
using Gringotts.Domain;
using NHibernate;

namespace Gringotts.Persistence
{
	public class InvestorRepository
	{
		private ISession nhibernateSession;

		public ISession NHibernateSession
		{
			get
			{
				if (nhibernateSession == null)
					throw new InvalidOperationException("The NHibernateSession property is null");
				return nhibernateSession;
			}
			set
			{
				nhibernateSession = value;
			}
		}

		public int Save(Investor newInvestor)
		{
			return (int)NHibernateSession.Save(newInvestor);
		}

		public Investor GetInvestorById(int id)
		{
			return NHibernateSession.Load<Investor>(id);
		}
	}
}