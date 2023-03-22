using BubblesDivePlanner.DiveModels;
using BubblesDivePlanner.ViewModels.Cylinders;
using BubblesDivePlanner.ViewModels.DiveStages;

namespace BubblesDivePlannerTests.TestFixtures
{
    public static class DivePlannerApplicationTestFixture
    {
        private const int COMPARTMENT_SIZE = 16;

        public static Zhl16BuhlmannModel GetDiveModel => new();

        public static DiveStepViewModel GetDiveStep => new()
        {
            Depth = 50,
            Time = 10
        };

        public static CylinderSetupViewModel GetSelectedCylinder => new()
        {
            CylinderName = "Air",
            CylinderPressure = 200,
            CylinderVolume = 12,
            InitialPressurisedCylinderVolume = 2400,
            GasMixture = new GasMixtureViewModel()
            {
                Oxygen = 21,
                Helium = 0
            },
            GasUsage = new GasUsageViewModel()
            {
                GasRemaining = 2400,
                GasUsed = 720,
                SurfaceAirConsumptionRate = 12
            }
        };

        public static DiveProfileViewModel GetDiveProfile => new(COMPARTMENT_SIZE)
        {
            PressureOxygen = 0,
            PressureNitrogen = 0,
            PressureHelium = 0,
            AValues = new double[COMPARTMENT_SIZE] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            BValues = new double[COMPARTMENT_SIZE] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            MaxSurfacePressures = new double[COMPARTMENT_SIZE] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            TissuePressuresNitrogen = new double[COMPARTMENT_SIZE] { 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79 },
            TissuePressuresHelium = new double[COMPARTMENT_SIZE] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            TissuePressuresTotal = new double[COMPARTMENT_SIZE] { 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79 },
            ToleratedAmbientPressures = new double[COMPARTMENT_SIZE] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            CompartmentLoad = new double[COMPARTMENT_SIZE] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
        };

        public static DiveProfileViewModel GetDiveProfileResultFromFirstRun => new(16)
        {
            PressureOxygen = 1.26,
            PressureNitrogen = 4.74,
            PressureHelium = 0,
            AValues = new double[COMPARTMENT_SIZE] { 1.2559, 1.0000, 0.8618, 0.7562, 0.6667, 0.5600, 0.4947, 0.4500, 0.4187, 0.3798, 0.3497, 0.3223, 0.2850, 0.2737, 0.2523, 0.2327 },
            BValues = new double[COMPARTMENT_SIZE] { 0.5050, 0.6514, 0.7222, 0.7825, 0.8126, 0.8434, 0.8693, 0.8910, 0.9092, 0.9222, 0.9319, 0.9403, 0.9477, 0.9544, 0.9602, 0.9653 },
            MaxSurfacePressures = new double[COMPARTMENT_SIZE] { 3.2361, 2.5352, 2.2465, 2.0342, 1.8973, 1.7457, 1.6451, 1.5723, 1.5186, 1.4642, 1.4228, 1.3858, 1.3402, 1.3215, 1.2937, 1.2686 },
            TissuePressuresNitrogen = new double[COMPARTMENT_SIZE] { 4.0417, 3.0792, 2.4713, 2.0243, 1.6843, 1.4439, 1.2634, 1.13, 1.0334, 0.9731, 0.9337, 0.9029, 0.8788, 0.8596, 0.8446, 0.8329 },
            TissuePressuresHelium = new double[COMPARTMENT_SIZE] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            TissuePressuresTotal = new double[COMPARTMENT_SIZE] { 4.0417, 3.0792, 2.4713, 2.0243, 1.6843, 1.4439, 1.2634, 1.13, 1.0334, 0.9731, 0.9337, 0.9029, 0.8788, 0.8596, 0.8446, 0.8329 },
            ToleratedAmbientPressures = new double[COMPARTMENT_SIZE] { 1.4068, 1.3544, 1.1624, 0.9923, 0.8269, 0.7455, 0.6682, 0.6059, 0.5589, 0.5471, 0.5442, 0.5459, 0.5627, 0.5592, 0.5687, 0.5794 },
            CompartmentLoad = new double[COMPARTMENT_SIZE] { 124.89, 121.46, 110.01, 99.51, 88.77, 82.71, 76.8, 71.87, 68.05, 66.46, 65.62, 65.15, 65.57, 65.05, 65.29, 65.66 }
        };

        public static DiveProfileViewModel GetDiveProfileResultFromSecondRun => new(COMPARTMENT_SIZE)
        {
            PressureOxygen = 1.26,
            PressureNitrogen = 4.74,
            PressureHelium = 0,
            AValues = new double[16] { 1.2559, 1, 0.8618, 0.7562, 0.6667, 0.56, 0.4947, 0.45, 0.4187, 0.3798, 0.3497, 0.3223, 0.285, 0.2737, 0.2523, 0.2327 },
            BValues = new double[16] { 0.505, 0.6514, 0.7222, 0.7825, 0.8126, 0.8434, 0.8693, 0.891, 0.9092, 0.9222, 0.9319, 0.9403, 0.9477, 0.9544, 0.9602, 0.9653 },
            MaxSurfacePressures = new double[16] { 3.2361, 2.5352, 2.2465, 2.0342, 1.8973, 1.7457, 1.6451, 1.5723, 1.5186, 1.4642, 1.4228, 1.3858, 1.3402, 1.3215, 1.2937, 1.2686 },
            TissuePressuresNitrogen = new double[16] { 4.6166, 4.0417, 3.437, 2.8729, 2.3762, 1.9896, 1.68, 1.4408, 1.2618, 1.1478, 1.0722, 1.0126, 0.9656, 0.928, 0.8984, 0.8753 },
            TissuePressuresHelium = new double[16] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            TissuePressuresTotal = new double[16] { 4.6166, 4.0417, 3.437, 2.8729, 2.3762, 1.9896, 1.68, 1.4408, 1.2618, 1.1478, 1.0722, 1.0126, 0.9656, 0.928, 0.8984, 0.8753 },
            ToleratedAmbientPressures = new double[16] { 1.6972, 1.9814, 1.8598, 1.6563, 1.3891, 1.2057, 1.0304, 0.8828, 0.7665, 0.7082, 0.6733, 0.6491, 0.645, 0.6245, 0.6204, 0.6203 },
            CompartmentLoad = new double[16] { 142.66, 159.42, 152.99, 141.23, 125.24, 113.97, 102.12, 91.64, 83.09, 78.39, 75.36, 73.07, 72.05, 70.22, 69.44, 69 }
        };
    }
}
