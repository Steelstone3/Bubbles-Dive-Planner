namespace BubblesDivePlanner.Models.DiveModels.Types
{
    public class DcapMf11f6 : DiveModel
    {
        private const byte NUMBER_OF_COMPARTMENTS = 11;

        public DcapMf11f6(IDiveProfile diveProfile) : base(diveProfile)
        {
            Name = DiveModelNames.DCAP_MF11F6.ToString();
            CompartmentCount = NUMBER_OF_COMPARTMENTS;
            NitrogenHalfTimes = new double[NUMBER_OF_COMPARTMENTS] { 5.0, 10.0, 25.0, 55.0, 95.0, 145.0, 200.0, 285.0, 385.0, 520.0, 670.0 };
            HeliumHalfTimes = new double[NUMBER_OF_COMPARTMENTS];
            AValuesNitrogen = new double[NUMBER_OF_COMPARTMENTS] { 1.89, 1.415, 0.824, 0.418, 0.352, 0.346, 0.343, 0.35, 0.35, 0.34, 0.33 };
            BValuesNitrogen = new double[NUMBER_OF_COMPARTMENTS] { 0.769, 0.952, 0.926, 0.943, 0.962, 0.98, 0.99, 1.0, 1.0, 1.0, 1.0 };
            AValuesHelium = new double[NUMBER_OF_COMPARTMENTS];
            BValuesHelium = new double[NUMBER_OF_COMPARTMENTS];
            AssignDiveProfile();
        }
    }
}