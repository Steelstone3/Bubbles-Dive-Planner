namespace BubblesDivePlanner.Models.DiveModels
{
    public class DiveModel : IDiveModel
    {
        public DiveModel(IDiveProfile diveProfile)
        {
            DiveProfile = diveProfile;
        }

        public string Name { get; protected set; }
        public byte CompartmentCount { get; protected set; }
        public double[] NitrogenHalfTimes { get; protected set; }
        public double[] HeliumHalfTimes { get; protected set; }
        public double[] AValuesNitrogen { get; protected set; }
        public double[] BValuesNitrogen { get; protected set; }
        public double[] AValuesHelium { get; protected set; }
        public double[] BValuesHelium { get; protected set; }
        public IDiveProfile DiveProfile { get; protected set; }

        protected void AssignDiveProfile()
        {
            switch (DiveProfile)
            {
                case null:
                    DiveProfile = new DiveProfile(CompartmentCount);
                    break;
                default:
                    DiveProfile = DiveProfile;
                    break;
            }
        }
    }
}