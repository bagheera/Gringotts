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
            return subscription.Aggregate(new Amount(0), (amt, inv) => amt + inv.Value);
        }
    }

    public void Add(Investment investment)
    {
        subscription.Add(investment);
    }
}