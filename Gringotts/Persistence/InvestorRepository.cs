using System;
using System.Collections.Generic;
using Gringotts.Domain;
using NHibernate;

namespace Gringotts.Persistence
{
	public class InvestorRepository
	{
		private ISession nhibernateSession;

		public ISession Session
		{
			get
			{
				if (nhibernateSession == null)
					throw new InvalidOperationException("The Session property is null");
				return nhibernateSession;
			}
			set
			{
				nhibernateSession = value;
			}
		}

		public int Save(Investor newInvestor)
		{
			return (int)Session.Save(newInvestor);
		}

		public Investor GetInvestorById(int id)
		{
			return Session.Load<Investor>(id);
		}
	}
}