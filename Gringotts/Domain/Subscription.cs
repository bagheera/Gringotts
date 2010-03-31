using System;
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
            Amount total = new Amount();
            return total;
        }
    }

    public void Add(Investment investment)
    {
        subscription.Add(investment);
    }
}