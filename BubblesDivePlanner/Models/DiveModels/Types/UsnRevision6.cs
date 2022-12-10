namespace BubblesDivePlanner.Models.DiveModels.Types
{
    public class UsnRevision6 : DiveModel
    {
        private const byte NUMBER_OF_COMPARTMENTS = 9;

        public UsnRevision6(IDiveProfile diveProfile) : base(diveProfile)
        {
            Name = DiveModelNames.USN_REVISION_6.ToString();
            CompartmentCount = NUMBER_OF_COMPARTMENTS;
            NitrogenHalfTimes = new double[NUMBER_OF_COMPARTMENTS] { 5.0, 10.0, 20.0, 40.0, 80.0, 120.0, 160.0, 200.0, 240.0 };
            HeliumHalfTimes = new double[NUMBER_OF_COMPARTMENTS] { 5.0, 10.0, 20.0, 40.0, 80.0, 120.0, 160.0, 200.0, 240.0 };
            AValuesNitrogen = new double[NUMBER_OF_COMPARTMENTS] { 1.37, 1.08, 0.69, 0.3, 0.34, 0.38, 0.4, 0.45, 0.42 };
            BValuesNitrogen = new double[NUMBER_OF_COMPARTMENTS] { 0.555, 0.625, 0.666, 0.714, 0.769, 0.833, 0.870, 0.909, 0.909 };
            AValuesHelium = new double[NUMBER_OF_COMPARTMENTS] { 1.12, 0.85, 0.71, 0.63, 0.5, 0.44, 0.54, 0.61, 0.61 };
            BValuesHelium = new double[NUMBER_OF_COMPARTMENTS] { 0.67, 0.714, 0.769, 0.83, 0.83, 0.91, 1.0, 1.0, 1.0 };
            AssignDiveProfile();
        }
    }
}