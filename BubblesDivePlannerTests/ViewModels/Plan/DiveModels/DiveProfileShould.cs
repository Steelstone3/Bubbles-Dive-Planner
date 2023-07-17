using BubblesDivePlanner.ViewModels.Model.Plan.DiveModels;
using BubblesDivePlanner.ViewModels.Plan.DiveModels;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels.Plan.DiveModels
{
    public class DiveProfileShould
    {
        [Theory]
        [InlineData(16)]
        [InlineData(12)]
        [InlineData(8)]
        public void ConstructModelWithCorrectCollectionSizes(int compartmentSize)
        {
            //Arrange
            IDiveProfile diveProfile = new DiveProfile(compartmentSize);

            //Assert
            Assert.Equal(compartmentSize, diveProfile.MaxSurfacePressures.Length);
            Assert.Equal(compartmentSize, diveProfile.NitrogenTissuePressures.Length);
            Assert.Equal(compartmentSize, diveProfile.HeliumTissuePressures.Length);
            Assert.Equal(compartmentSize, diveProfile.TotalTissuePressures.Length);
            Assert.Equal(compartmentSize, diveProfile.ToleratedAmbientPressures.Length);
            Assert.Equal(compartmentSize, diveProfile.AValues.Length);
            Assert.Equal(compartmentSize, diveProfile.BValues.Length);
            Assert.Equal(compartmentSize, diveProfile.CompartmentLoads.Length);
            Assert.Equal(0, diveProfile.OxygenAtPressure);
            Assert.Equal(0, diveProfile.NitrogenAtPressure);
            Assert.Equal(0, diveProfile.HeliumAtPressure);
        }

        // [Fact]
        // public void ConstructModelWithCorrectInitialValues()
        // {
        //     //Arrange
        //     var expectedDiveStepViewModel = new DiveProfile(16)
        //     {
        //         PressureOxygen = 0,
        //         PressureNitrogen = 0,
        //         PressureHelium = 0,
        //         AValues = new double[COMPARTMENT_SIZE] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        //         BValues = new double[COMPARTMENT_SIZE] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        //         MaxSurfacePressures = new double[COMPARTMENT_SIZE] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        //         TissuePressuresNitrogen = new double[COMPARTMENT_SIZE] { 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79 },
        //         TissuePressuresHelium = new double[COMPARTMENT_SIZE] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        //         TissuePressuresTotal = new double[COMPARTMENT_SIZE] { 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79 },
        //         ToleratedAmbientPressures = new double[COMPARTMENT_SIZE] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        //         CompartmentLoad = new double[COMPARTMENT_SIZE] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
        //     };

        //     var actualDiveStepViewModel = new DiveProfile(16);

        //     //Assert
        //     Assert.NotNull(actualDiveStepViewModel);
        //     Assert.Equal(expectedDiveStepViewModel.PressureOxygen, actualDiveStepViewModel.PressureOxygen);
        //     Assert.Equal(expectedDiveStepViewModel.PressureNitrogen, actualDiveStepViewModel.PressureNitrogen);
        //     Assert.Equal(expectedDiveStepViewModel.PressureHelium, actualDiveStepViewModel.PressureHelium);
        //     Assert.Equal(expectedDiveStepViewModel.DiveCeiling, actualDiveStepViewModel.DiveCeiling);
        //     Assert.Equal(expectedDiveStepViewModel.AValues, actualDiveStepViewModel.AValues);
        //     Assert.Equal(expectedDiveStepViewModel.BValues, actualDiveStepViewModel.BValues);
        //     Assert.Equal(expectedDiveStepViewModel.MaxSurfacePressures, actualDiveStepViewModel.MaxSurfacePressures);
        //     Assert.Equal(expectedDiveStepViewModel.TissuePressuresNitrogen, actualDiveStepViewModel.TissuePressuresNitrogen);
        //     Assert.Equal(expectedDiveStepViewModel.TissuePressuresHelium, actualDiveStepViewModel.TissuePressuresHelium);
        //     Assert.Equal(expectedDiveStepViewModel.TissuePressuresTotal, actualDiveStepViewModel.TissuePressuresTotal);
        //     Assert.Equal(expectedDiveStepViewModel.ToleratedAmbientPressures, actualDiveStepViewModel.ToleratedAmbientPressures);
        //     Assert.Equal(expectedDiveStepViewModel.CompartmentLoad, actualDiveStepViewModel.CompartmentLoad);
        // }

        // [Fact]
        // public void AllowModelToBeSet()
        // {
        //     //Arrange
        //     var diveProfile = DivePlannerApplicationTestFixture.GetDiveProfileResultFromFirstRun;

        //     //Act
        //     diveProfile.MaxSurfacePressures = expectedArray;
        //     diveProfile.TissuePressuresNitrogen = expectedArray;
        //     diveProfile.TissuePressuresHelium = expectedArray;
        //     diveProfile.TissuePressuresTotal = expectedArray;
        //     diveProfile.ToleratedAmbientPressures = expectedArray;
        //     diveProfile.AValues = expectedArray;
        //     diveProfile.BValues = expectedArray;
        //     diveProfile.CompartmentLoad = expectedArray;
        //     diveProfile.PressureOxygen = expectedValue;
        //     diveProfile.PressureHelium = expectedValue;
        //     diveProfile.PressureNitrogen = expectedValue;

        //     //Assert
        //     Assert.NotEmpty(diveProfile.MaxSurfacePressures);
        //     Assert.NotEmpty(diveProfile.TissuePressuresNitrogen);
        //     Assert.NotEmpty(diveProfile.TissuePressuresHelium);
        //     Assert.NotEmpty(diveProfile.TissuePressuresTotal);
        //     Assert.NotEmpty(diveProfile.ToleratedAmbientPressures);
        //     Assert.NotEmpty(diveProfile.AValues);
        //     Assert.NotEmpty(diveProfile.BValues);
        //     Assert.NotEmpty(diveProfile.CompartmentLoad);
        //     Assert.Equal(expectedValue, diveProfile.PressureOxygen);
        //     Assert.Equal(expectedValue, diveProfile.PressureNitrogen);
        //     Assert.Equal(expectedValue, diveProfile.PressureHelium);
        //     Assert.Equal(50, diveProfile.DiveCeiling);
        // }

        // [Fact]
        // public void RaisePropertyChanged()
        // {
        //     //Arrange
        //     var diveProfile = DivePlannerApplicationTestFixture.GetDiveProfileResultFromFirstRun;
        //     var viewModelEvents = new List<string>();
        //     diveProfile.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

        //     //Act
        //     diveProfile.MaxSurfacePressures = expectedArray;
        //     diveProfile.TissuePressuresNitrogen = expectedArray;
        //     diveProfile.TissuePressuresHelium = expectedArray;
        //     diveProfile.TissuePressuresTotal = expectedArray;
        //     diveProfile.ToleratedAmbientPressures = expectedArray;
        //     diveProfile.AValues = expectedArray;
        //     diveProfile.BValues = expectedArray;
        //     diveProfile.CompartmentLoad = expectedArray;
        //     diveProfile.PressureOxygen = expectedValue;
        //     diveProfile.PressureHelium = expectedValue;
        //     diveProfile.PressureNitrogen = expectedValue;

        //     //Assert
        //     Assert.NotEmpty(viewModelEvents);
        //     Assert.Contains(nameof(diveProfile.MaxSurfacePressures), viewModelEvents);
        //     Assert.Contains(nameof(diveProfile.TissuePressuresNitrogen), viewModelEvents);
        //     Assert.Contains(nameof(diveProfile.TissuePressuresHelium), viewModelEvents);
        //     Assert.Contains(nameof(diveProfile.TissuePressuresHelium), viewModelEvents);
        //     Assert.Contains(nameof(diveProfile.ToleratedAmbientPressures), viewModelEvents);
        //     Assert.Contains(nameof(diveProfile.AValues), viewModelEvents);
        //     Assert.Contains(nameof(diveProfile.BValues), viewModelEvents);
        //     Assert.Contains(nameof(diveProfile.CompartmentLoad), viewModelEvents);
        //     Assert.Contains(nameof(diveProfile.PressureOxygen), viewModelEvents);
        //     Assert.Contains(nameof(diveProfile.PressureHelium), viewModelEvents);
        //     Assert.Contains(nameof(diveProfile.PressureNitrogen), viewModelEvents);
        // }

        // [Fact]
        // public void Clone()
        // {
        //     //Act
        //     var diveProfile = DivePlannerApplicationTestFixture.GetDiveProfileResultFromFirstRun;
        //     var newDiveProfileModel = diveProfile.DeepClone();

        //     //Assert
        //     Assert.NotSame(diveProfile, newDiveProfileModel);
        //     Assert.Equal(diveProfile.PressureOxygen, newDiveProfileModel.PressureOxygen);
        //     Assert.Equal(diveProfile.PressureNitrogen, newDiveProfileModel.PressureNitrogen);
        //     Assert.Equal(diveProfile.PressureHelium, newDiveProfileModel.PressureHelium);
        //     Assert.Equal(diveProfile.DiveCeiling, newDiveProfileModel.DiveCeiling);
        //     Assert.NotSame(diveProfile.AValues, newDiveProfileModel.AValues);
        //     Assert.NotSame(diveProfile.BValues, newDiveProfileModel.BValues);
        //     Assert.NotSame(diveProfile.TissuePressuresNitrogen, newDiveProfileModel.TissuePressuresNitrogen);
        //     Assert.NotSame(diveProfile.TissuePressuresHelium, newDiveProfileModel.TissuePressuresHelium);
        //     Assert.NotSame(diveProfile.TissuePressuresTotal, newDiveProfileModel.TissuePressuresTotal);
        //     Assert.NotSame(diveProfile.ToleratedAmbientPressures, newDiveProfileModel.ToleratedAmbientPressures);
        //     Assert.NotSame(diveProfile.CompartmentLoad, newDiveProfileModel.CompartmentLoad);
        //     Assert.NotSame(diveProfile.MaxSurfacePressures, newDiveProfileModel.MaxSurfacePressures);
        // }

    }
}