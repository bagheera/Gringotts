using System;

namespace Gringotts.Domain
{
    public class Investment
    {
        public Investment(Amount amount)
        {
            Value = amount;
        }

        public Amount Value { get; set; }
    }
}