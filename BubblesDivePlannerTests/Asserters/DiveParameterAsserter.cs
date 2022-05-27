using BubblesDivePlanner.Cylinders.CylinderSetup;
using BubblesDivePlanner.DiveModels.DiveProfile;
using BubblesDivePlanner.DiveStep;
using Xunit;

namespace BubblesDivePlannerTests.Asserters
{
    public class DiveParameterAsserter
    {
        public void AssertDiveStepValuesEquality(DiveStepViewModel expectedDiveStepViewModel, DiveStepViewModel actualDiveStepViewModel)
        {
            Assert.Equal(expectedDiveStepViewModel.Depth, actualDiveStepViewModel.Depth);
            Assert.Equal(expectedDiveStepViewModel.Time, actualDiveStepViewModel.Time);
        }

        public void AssertSelectedCylinderValuesEquality(CylinderSetupViewModel expectedSelectedCylinderViewModel, CylinderSetupViewModel actualSelectedCylinderViewModel)
        {
            Assert.NotNull(actualSelectedCylinderViewModel);
            Assert.Equal(expectedSelectedCylinderViewModel.CylinderName, actualSelectedCylinderViewModel.CylinderName);
            Assert.Equal(expectedSelectedCylinderViewModel.CylinderPressure, actualSelectedCylinderViewModel.CylinderPressure);
            Assert.Equal(expectedSelectedCylinderViewModel.CylinderVolume, actualSelectedCylinderViewModel.CylinderVolume);
            Assert.NotNull(actualSelectedCylinderViewModel.GasMixture);
            Assert.Equal(expectedSelectedCylinderViewModel.GasMixture.Helium, actualSelectedCylinderViewModel.GasMixture.Helium);
            Assert.Equal(expectedSelectedCylinderViewModel.GasMixture.Oxygen, actualSelectedCylinderViewModel.GasMixture.Oxygen);
            Assert.Equal(expectedSelectedCylinderViewModel.GasMixture.Nitrogen, actualSelectedCylinderViewModel.GasMixture.Nitrogen);
            Assert.NotNull(actualSelectedCylinderViewModel.GasUsage);
            Assert.Equal(expectedSelectedCylinderViewModel.GasUsage.SurfaceAirConsumptionRate, actualSelectedCylinderViewModel.GasUsage.SurfaceAirConsumptionRate);
            Assert.Equal(expectedSelectedCylinderViewModel.GasUsage.InitialPressurisedCylinderVolume, actualSelectedCylinderViewModel.GasUsage.InitialPressurisedCylinderVolume);
            Assert.Equal(expectedSelectedCylinderViewModel.GasUsage.GasRemaining, actualSelectedCylinderViewModel.GasUsage.GasRemaining);
            Assert.Equal(expectedSelectedCylinderViewModel.GasUsage.GasUsed, actualSelectedCylinderViewModel.GasUsage.GasUsed);
        }

          public void AssertDiveProfileValuesEquality(DiveProfileViewModel expectedDiveStepViewModel, DiveProfileViewModel actualDiveStepViewModel)
        {
            Assert.NotNull(actualDiveStepViewModel);
            Assert.Equal(expectedDiveStepViewModel.PressureOxygen, actualDiveStepViewModel.PressureOxygen);
            Assert.Equal(expectedDiveStepViewModel.PressureNitrogen, actualDiveStepViewModel.PressureNitrogen);
            Assert.Equal(expectedDiveStepViewModel.PressureHelium, actualDiveStepViewModel.PressureHelium);
            Assert.Equal(expectedDiveStepViewModel.AValues, actualDiveStepViewModel.AValues);
            Assert.Equal(expectedDiveStepViewModel.BValues, actualDiveStepViewModel.BValues);
            Assert.Equal(expectedDiveStepViewModel.MaxSurfacePressures, actualDiveStepViewModel.MaxSurfacePressures);
            Assert.Equal(expectedDiveStepViewModel.TissuePressuresNitrogen, actualDiveStepViewModel.TissuePressuresNitrogen);
            Assert.Equal(expectedDiveStepViewModel.TissuePressuresHelium, actualDiveStepViewModel.TissuePressuresHelium);
            Assert.Equal(expectedDiveStepViewModel.TissuePressuresTotal, actualDiveStepViewModel.TissuePressuresTotal);
            Assert.Equal(expectedDiveStepViewModel.ToleratedAmbientPressures, actualDiveStepViewModel.ToleratedAmbientPressures);
            Assert.Equal(expectedDiveStepViewModel.CompartmentLoad, actualDiveStepViewModel.CompartmentLoad);
        }
    }
}