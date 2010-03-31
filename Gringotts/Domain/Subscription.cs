using System.Linq;
using System.Collections.Generic;
using Gringotts.Domain;

public class Subscription
{
    private readonly List<Investment> subscription = new List<Investment>();

    public int Count
    {
        get { return subscription.Count; }
    }

    public Amount Value
    {
        get
        {
            Amount total = new Amount(0);
            foreach (var investment in subscription)
            {
                total += investment.Value;
            }
            return total;
        }
    }

    public void Add(Investment investment)
    {
        subscription.Add(investment);
    }
}