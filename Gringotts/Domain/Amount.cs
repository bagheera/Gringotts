using System;

namespace Gringotts.Domain
{
    public class Amount : IComparable
    {
        private float value;

        public Amount(int amount)
        {
            value = amount;
        }

        public Amount(float amount)
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

        public static Amount operator /(Amount self, Amount another)
        {
            return new Amount(self.value / another.value);
        }

        public static Amount operator *(Amount self, Amount another)
        {
            return new Amount(self.value * another.value);
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
            return value.ToString();
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public int CompareTo(object obj)
        {
            Amount other = obj as Amount;
            if(other == null)
                throw new ArgumentException();
            return value.CompareTo(other.value);
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

        public Amount Abs()
        {
            return new Amount(Math.Abs(value));
        }
    }
}