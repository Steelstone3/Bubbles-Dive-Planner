namespace BubblesDivePlanner.Models.DiveModels.Types
{
    public class Zhl12Buhlmann : DiveModel
    {
        private const byte NUMBER_OF_COMPARTMENTS = 16;

        public Zhl12Buhlmann(IDiveProfile diveProfile) : base(diveProfile)
        {
            Name = DiveModelNames.ZHL12.ToString();
            CompartmentCount = NUMBER_OF_COMPARTMENTS;
            NitrogenHalfTimes = new double[NUMBER_OF_COMPARTMENTS] { 2.65, 7.94, 12.2, 18.5, 26.5, 37.0, 53.0, 79.0, 114.0, 146.0, 185.0, 238.0, 304.0, 397.0, 503.0, 635.0 };
            HeliumHalfTimes = new double[NUMBER_OF_COMPARTMENTS] { 1.0, 3.0, 4.6, 7.0, 10.0, 14.0, 20.0, 30.0, 43.0, 55.0, 70.0, 90.0, 115.0, 150.0, 190.0, 240.0 };
            // TODO Double check a and b values are correct for both Nitrogen and Helium
            AValuesNitrogen = new double[NUMBER_OF_COMPARTMENTS] { 2.2005, 1.5005, 1.0779, 0.9024, 0.7466, 0.5772, 0.4706, 0.4564, 0.4564, 0.4593, 0.4593, 0.3807, 0.2505, 0.2505, 0.2505, 0.2505 };
            BValuesNitrogen = new double[NUMBER_OF_COMPARTMENTS] { 0.82, 0.82, 0.825, 0.835, 0.845, 0.86, 0.87, 0.89, 0.89, 0.934, 0.934, 0.944, 0.962, 0.962, 0.962, 0.962 };
            AValuesHelium = new double[NUMBER_OF_COMPARTMENTS] { 2.2005, 1.5079, 1.0924, 0.9166, 0.7672, 0.5906, 0.4964, 0.4564, 0.5001, 0.5001, 0.5001, 0.5001, 0.5001, 0.5001, 0.5001, 0.5001 };
            BValuesHelium = new double[NUMBER_OF_COMPARTMENTS] { 0.82, 0.825, 0.835, 0.845, 0.86, 0.87, 0.89, 0.89, 0.926, 0.926, 0.926, 0.926, 0.926, 0.926, 0.926, 0.926 };
            AssignDiveProfile();
        }
    }
}