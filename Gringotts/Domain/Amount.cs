namespace Gringotts.Domain
{
	public class Amount
    {
	    public Amount(int amount)
		{
			this.Value = amount;
		}

        public Amount()
        {
            
        }

	    public int Value { get; private set; }
	}
}