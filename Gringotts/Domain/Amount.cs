using System;

namespace Gringotts.Domain
{
    public class Amount
    {
        private decimal value;

        public Amount(int amount)
        {
            value = amount;
        }

        public Amount(decimal amount)
        {
            value = amount;
        }

        public Amount()
        {

        }

        public static Amount operator -(Amount self, Amount another)
        {
            return new Amount(self.value - another.value);
        }

        public static Amount operator +(Amount self, Amount another)
        {
            return new Amount(self.value + another.value);
        }

        public bool Equals(Amount other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.value == value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Amount)) return false;
            return Equals((Amount)obj);
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }        

        public static bool operator <=(Amount left, Amount right)
        {
            return left.value <= right.value;
        }

        public static bool operator >=(Amount left, Amount right)
        {
            return left.value >= right.value;
        }

        public static bool operator >(Amount left, Amount right)
        {
            return left.value > right.value;
        }

        public static bool operator <(Amount left, Amount right)
        {
            return left.value < right.value;
        }
    }
}