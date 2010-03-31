namespace Gringotts.Domain
{
    public class Investment
    {
        public Investor investor;//HACK

        public Investment(Amount amount)
        {
            Value = amount;
        }

        public Amount Value { get; set; }
    }
}