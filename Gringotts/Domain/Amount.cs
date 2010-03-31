using System;

namespace Gringotts.Domain
{
    public class Amount
    {
        public Amount(int amount)
        {
            Value = amount;
        }

        public Amount()
        {

        }

        public int Value { get; private set; }

        public static Amount operator -(Amount self, Amount another)
        {
            return new Amount(self.Value - another.Value);
        }

        public static Amount operator +(Amount self, Amount another)
        {
            return new Amount(self.Value + another.Value);
        }

        public bool Equals(Amount other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Value == Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Amount)) return false;
            return Equals((Amount)obj);
        }

        public override int GetHashCode()
        {
            return Value;
        }

        public static bool operator <=(Amount left, Amount right)
        {
            return left.Value <= right.Value;
        }

        public static bool operator >=(Amount left, Amount right)
        {
            return left.Value >= right.Value;
        }

        public static bool operator >(Amount left, Amount right)
        {
            return left.Value > right.Value;
        }

        public static bool operator <(Amount left, Amount right)
        {
            return left.Value < right.Value;
        }
    }
}