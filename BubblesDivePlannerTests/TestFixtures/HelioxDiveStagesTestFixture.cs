using System.Collections.Generic;
using BubblesDivePlanner.Cylinders.CylinderSetup;
using BubblesDivePlanner.Cylinders.CylinderSetup.GasMixture;
using BubblesDivePlanner.Cylinders.CylinderSetup.GasUsage;
using BubblesDivePlanner.DiveModels;
using BubblesDivePlanner.DiveModels.DiveProfile;
using BubblesDivePlanner.DiveStep;

namespace BubblesDivePlannerTests.TestFixtures
{
    public class HelioxDiveStagesTextFixture
    {
        public Zhl16BuhlmannModel GetDiveModel => new Zhl16BuhlmannModel();

        public DiveStepViewModel GetDiveStep => new DiveStepViewModel()
        {
            Depth = 100,
            Time = 20
        };

        public CylinderSetupViewModel GetSelectedCylinder => new CylinderSetupViewModel()
        {
            CylinderName = "Heliox",
            CylinderPressure = 200,
            CylinderVolume = 24,
            GasMixture = new GasMixtureViewModel()
            {
                Oxygen = 10,
                Helium = 10
            },
            GasUsage = new GasUsageViewModel()
            {
                GasRemaining = 1680,
                GasUsed = 720,
                InitialPressurisedCylinderVolume = 2400,
                SurfaceAirConsumptionRate = 12
            }
        };

        //TODO in progress need to put in the expected results here
        public DiveProfileViewModel GetDiveProfileResult => new DiveProfileViewModel() {
            PressureOxygen = 0,
            PressureNitrogen = 0,
            PressureHelium = 0,
            AValues = new List<double> {},
            BValues = new List<double> {},
            MaxSurfacePressures = new List<double> {},
            TissuePressuresNitrogen = new List<double> {},
            TissuePressuresHelium = new List<double> {},
            TissuePressuresTotal = new List<double> {},
            ToleratedAmbientPressures = new List<double> {},
            CompartmentLoad = new List<double> {}
        };
    }
}
