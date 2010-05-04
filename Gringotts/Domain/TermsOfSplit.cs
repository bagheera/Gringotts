namespace Gringotts.Domain{
    public class TermsOfSplit{

        public int PartRatio { get; private set; }
        public string FirstVentureName { get; private set; }
        public string SecondVentureName { get; private set; }

        public TermsOfSplit(int partRatio, string firstVentureName, string secondVentureName){
            PartRatio = partRatio;
            FirstVentureName = firstVentureName;
            SecondVentureName = secondVentureName;
        }
    }
}