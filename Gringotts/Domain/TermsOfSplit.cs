using System;

namespace Gringotts.Domain{
    public class TermsOfSplit{

        public Percentage Ratio { get; private set; }
        public Name FirstVentureName { get; private set; }
        public Name SecondVentureName { get; private set; }

        public TermsOfSplit(Percentage ratio, Name firstVentureName, Name secondVentureName){
            Ratio = ratio;
            FirstVentureName = firstVentureName;
            SecondVentureName = secondVentureName;
        }
    }

    public class Percentage
    {
        public float Ratio { get; private set; }

        public Percentage(float ratio)
        {
            if (!IsValid(ratio))
                throw new ArgumentException("Ratio can be between 0.0 and 1.0 value");
            Ratio = ratio;
        }

        public Percentage RemainingPercentage
        {
            get { return new Percentage(1.0f - Ratio); }
        }

        private bool IsValid(float ratio)
        {
            return ratio <= 1.0 && ratio >= 0.0;
        }

        public Amount Apply(Amount amount)
        {
            return new Amount(amount.Denomination * Ratio);
        }

        public Amount ApplyRemaining(Amount amount)
        {
            return new Amount(amount.Denomination * RemainingPercentage.Ratio);
        }

        public bool Equals(Percentage other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Ratio == Ratio;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Percentage)) return false;
            return Equals((Percentage) obj);
        }

        public override int GetHashCode()
        {
            return Ratio.GetHashCode();
        }

        public static bool operator ==(Percentage left, Percentage right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Percentage left, Percentage right)
        {
            return !Equals(left, right);
        }
    }
}