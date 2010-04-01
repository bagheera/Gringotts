using System;
using System.Collections.Generic;

namespace Gringotts.Domain
{
    public class Holding
    {
        private readonly List<Investment> investments = new List<Investment>();

        public void Add(Investment investment)
        {
            investments.Add(investment);
        }
        public void	 DistributeDividends(Amount amount)
        {
            throw new NotImplementedException();
        }
    }
}