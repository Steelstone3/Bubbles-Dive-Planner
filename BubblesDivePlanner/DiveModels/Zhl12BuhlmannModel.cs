using BubblesDivePlanner.DiveModels.DiveProfile;

namespace BubblesDivePlanner.DiveModels
{
    public class Zhl12BuhlmannModel : IDiveModel
    {
        private const byte COMPARTMENT_COUNT = 16;

        public Zhl12BuhlmannModel()
        {
            DiveProfile = new DiveProfileViewModel(CompartmentCount);
        }

        public string DiveModelName => "Zhl16-B Model";
        public int CompartmentCount => COMPARTMENT_COUNT;
        public double[] NitrogenHalfTime => new double[COMPARTMENT_COUNT] { 2.65, 7.94, 12.2, 18.5, 26.5, 37.0, 53.0, 79.0, 114.0, 146.0, 185.0, 238.0, 304.0, 397.0, 503.0, 635.0 };
        public double[] HeliumHalfTime => new double[COMPARTMENT_COUNT] { 1.0, 3.0, 4.6, 7.0, 10.0, 14.0, 20.0, 30.0, 43.0, 55.0, 70.0, 90.0, 115.0, 150.0, 190.0, 240.0 };
        public double[] AValuesNitrogen => new double[COMPARTMENT_COUNT] { 2.2005, 1.5005, 1.0779, 0.9024, 0.7466, 0.5772, 0.4706, 0.4564, 0.4564, 0.4593, 0.4593, 0.3807, 0.2505, 0.2505, 0.2505, 0.2505 };
        public double[] BValuesNitrogen => new double[COMPARTMENT_COUNT] { 0.82, 0.82, 0.825, 0.835, 0.845, 0.86, 0.87, 0.89, 0.89, 0.934, 0.934, 0.944, 0.962, 0.962, 0.962, 0.962 };
        public double[] AValuesHelium => new double[COMPARTMENT_COUNT] { 2.2005, 1.5079, 1.0924, 0.9166, 0.7672, 0.5906, 0.4964, 0.4564, 0.5001, 0.5001, 0.5001, 0.5001, 0.5001, 0.5001, 0.5001, 0.5001 };
        public double[] BValuesHelium => new double[COMPARTMENT_COUNT] { 0.82, 0.825, 0.835, 0.845, 0.86, 0.87, 0.89, 0.89, 0.926, 0.926, 0.926, 0.926, 0.926, 0.926, 0.926, 0.926 };

        public IDiveProfileModel DiveProfile { get; set; }

        public IDiveModel DeepClone()
        {
            return new Zhl16BuhlmannModel()
            {
                DiveProfile = DiveProfile.DeepClone()
            };
        }
    }
}