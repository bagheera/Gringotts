using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gringotts.Domain
{
    public class Venture
    {
        public virtual string Id
        {
            get; set;
        }

        public virtual string Name
        {
            get;
            set;
        }

        public virtual int Outlay
        {
            get;
            set;
        }
    }
}
