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
        private readonly float myRatio;

        public Percentage(float ratio)
        {
            if (!IsValid(ratio))
                throw new ArgumentException("Ratio can be between 0.0 and 1.0 value");
            myRatio = ratio;
        }

        private bool IsValid(float ratio)
        {
            return ratio <= 1.0 && ratio >= 0.0;
        }

        public Amount Apply(Amount amount)
        {
            return new Amount(amount.Denomination * myRatio);
        }

        public Amount ApplyRemaining(Amount amount)
        {
            return new Amount(amount.Denomination * (1.0f - myRatio));
        }
    }
}