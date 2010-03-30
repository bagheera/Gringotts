using System;

namespace Gringotts.Domain
{
    public class Venture
    {
        internal virtual string Id { get; set; }
        internal virtual Name Name { get; set; }
        internal virtual Amount Outlay { get; set; }
        internal virtual Amount MinInvestment { get; set; }
    }
}
