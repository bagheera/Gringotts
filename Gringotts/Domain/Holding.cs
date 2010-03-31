using System;
using System.Collections.Generic;

namespace Gringotts.Domain
{
    public class Holding
    {
        private readonly List<Investment> investments = new List<Investment>();
        internal List<Investment> Investments
        {
            get { return investments; }
        }

        public void Add(Investment investment)
        {
            investments.Add(investment);
        }

        public void AddRange(List<Investment> confirmedInvestments)
        {
            investments.AddRange(confirmedInvestments);
        }
    }
}