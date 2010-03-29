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
