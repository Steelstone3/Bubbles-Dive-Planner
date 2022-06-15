using System.Collections.Generic;
using BubblesDivePlanner.Cylinders.CylinderSetup;
using BubblesDivePlanner.Cylinders.CylinderSetup.GasMixture;
using BubblesDivePlanner.Cylinders.CylinderSetup.GasUsage;
using BubblesDivePlanner.DiveModels;
using BubblesDivePlanner.DiveModels.DiveProfile;
using BubblesDivePlanner.DiveStep;

namespace BubblesDivePlannerTests.TestFixtures
{
    public static class HelioxDivePlannerApplicationTestFixture
    {
        public static Zhl16BuhlmannModel GetDiveModel => new Zhl16BuhlmannModel();

        public static DiveStepViewModel GetDiveStep()
        {
            return new DiveStepViewModel()
            {
                Depth = 100,
                Time = 20
            };
        }

        public static CylinderSetupViewModel GetSelectedCylinder => new CylinderSetupViewModel()
        {
            CylinderName = "Heliox",
            CylinderPressure = 200,
            CylinderVolume = 24,
            InitialPressurisedCylinderVolume = 2400,
            GasMixture = new GasMixtureViewModel()
            {
                Oxygen = 10,
                Helium = 10
            },
            GasUsage = new GasUsageViewModel()
            {
                GasRemaining = 1680,
                GasUsed = 720,
                SurfaceAirConsumptionRate = 12
            }
        };

        //TODO in progress need to put in the expected results here
        public static DiveProfileViewModel GetDiveProfileResult => new DiveProfileViewModel(16)
        {
            PressureOxygen = 0,
            PressureNitrogen = 0,
            PressureHelium = 0,
            // AValues = new double[] { },
            // BValues = new double[] { },
            // MaxSurfacePressures = new double[] { },
            // TissuePressuresNitrogen = new double[] { },
            // TissuePressuresHelium = new double[] { },
            // TissuePressuresTotal = new double[] { },
            // ToleratedAmbientPressures = new double[] { },
            // CompartmentLoad = new double[] { }
        };
    }
}
