using BubblesDivePlanner.ViewModels.DiveStages;
using BubblesDivePlanner.ViewModels.Models;

namespace BubblesDivePlanner.DiveModels
{
    public class UsnRevision6Model : IDiveModel
    {
        private const byte COMPARTMENT_COUNT = 9;

        public UsnRevision6Model()
        {
            DiveProfile = new DiveProfileViewModel(CompartmentCount);
        }

        public string DiveModelName => "USN Revision 6 Model";
        public int CompartmentCount => COMPARTMENT_COUNT;
        public double[] NitrogenHalfTime => new double[COMPARTMENT_COUNT] { 5.0, 10.0, 20.0, 40.0, 80.0, 120.0, 160.0, 200.0, 240.0 };
        public double[] HeliumHalfTime => new double[COMPARTMENT_COUNT] { 5.0, 10.0, 20.0, 40.0, 80.0, 120.0, 160.0, 200.0, 240.0 };
        public double[] AValuesNitrogen => new double[COMPARTMENT_COUNT] { 1.37, 1.08, 0.69, 0.3, 0.34, 0.38, 0.4, 0.45, 0.42 };
        public double[] BValuesNitrogen => new double[COMPARTMENT_COUNT] { 0.555, 0.625, 0.666, 0.714, 0.769, 0.833, 0.870, 0.909, 0.909 };
        public double[] AValuesHelium => new double[COMPARTMENT_COUNT] { 1.12, 0.85, 0.71, 0.63, 0.5, 0.44, 0.54, 0.61, 0.61 };
        public double[] BValuesHelium => new double[COMPARTMENT_COUNT] { 0.67, 0.714, 0.769, 0.83, 0.83, 0.91, 1.0, 1.0, 1.0 };

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