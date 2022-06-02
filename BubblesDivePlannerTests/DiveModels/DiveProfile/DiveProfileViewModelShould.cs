using Xunit;
using System.Collections.Generic;
using BubblesDivePlannerTests.TestFixtures;

namespace BubblesDivePlannerTests.DiveModels.DiveProfile
{
    public class DiveProfileViewModelShould
    {
        private readonly double[] expectedArray = { 3.0, 6.0 };
        private readonly double expectedValue = 10;
        private DiveStagesTextFixture diveStagesTextFixture = new DiveStagesTextFixture();

        [Fact]
        public void AllowModelToBeSet()
        {
            //Arrange
            var diveProfile = diveStagesTextFixture.GetDiveProfileResultFromFirstRun;

            //Act
            diveProfile.MaxSurfacePressures = expectedArray;
            diveProfile.TissuePressuresNitrogen = expectedArray;
            diveProfile.TissuePressuresHelium = expectedArray;
            diveProfile.TissuePressuresTotal = expectedArray;
            diveProfile.ToleratedAmbientPressures = expectedArray;
            diveProfile.AValues = expectedArray;
            diveProfile.BValues = expectedArray;
            diveProfile.CompartmentLoad = expectedArray;
            diveProfile.PressureOxygen = expectedValue;
            diveProfile.PressureHelium = expectedValue;
            diveProfile.PressureNitrogen = expectedValue;

            //Assert
            Assert.NotEmpty(diveProfile.MaxSurfacePressures);
            Assert.NotEmpty(diveProfile.TissuePressuresNitrogen);
            Assert.NotEmpty(diveProfile.TissuePressuresHelium);
            Assert.NotEmpty(diveProfile.TissuePressuresTotal);
            Assert.NotEmpty(diveProfile.ToleratedAmbientPressures);
            Assert.NotEmpty(diveProfile.AValues);
            Assert.NotEmpty(diveProfile.BValues);
            Assert.NotEmpty(diveProfile.CompartmentLoad);
            Assert.Equal(expectedValue, diveProfile.PressureOxygen);
            Assert.Equal(expectedValue, diveProfile.PressureNitrogen);
            Assert.Equal(expectedValue, diveProfile.PressureHelium);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            var diveProfile = diveStagesTextFixture.GetDiveProfileResultFromFirstRun;
            var viewModelEvents = new List<string>();
            diveProfile.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            diveProfile.MaxSurfacePressures = expectedArray;
            diveProfile.TissuePressuresNitrogen = expectedArray;
            diveProfile.TissuePressuresHelium = expectedArray;
            diveProfile.TissuePressuresTotal = expectedArray;
            diveProfile.ToleratedAmbientPressures = expectedArray;
            diveProfile.AValues = expectedArray;
            diveProfile.BValues = expectedArray;
            diveProfile.CompartmentLoad = expectedArray;
            diveProfile.PressureOxygen = expectedValue;
            diveProfile.PressureHelium = expectedValue;
            diveProfile.PressureNitrogen = expectedValue;


            //Assert
            Assert.NotEmpty(viewModelEvents);
            Assert.Contains(nameof(diveProfile.MaxSurfacePressures), viewModelEvents);
            Assert.Contains(nameof(diveProfile.TissuePressuresNitrogen), viewModelEvents);
            Assert.Contains(nameof(diveProfile.TissuePressuresHelium), viewModelEvents);
            Assert.Contains(nameof(diveProfile.TissuePressuresHelium), viewModelEvents);
            Assert.Contains(nameof(diveProfile.ToleratedAmbientPressures), viewModelEvents);
            Assert.Contains(nameof(diveProfile.AValues), viewModelEvents);
            Assert.Contains(nameof(diveProfile.BValues), viewModelEvents);
            Assert.Contains(nameof(diveProfile.CompartmentLoad), viewModelEvents);
            Assert.Contains(nameof(diveProfile.PressureOxygen), viewModelEvents);
            Assert.Contains(nameof(diveProfile.PressureHelium), viewModelEvents);
            Assert.Contains(nameof(diveProfile.PressureNitrogen), viewModelEvents);
        }

        [Fact]
        public void Clone()
        {
            //Act
            var diveProfile = diveStagesTextFixture.GetDiveProfileResultFromFirstRun;
            var newDiveProfileModel = diveProfile.DeepClone();

            //Assert
            Assert.NotSame(diveProfile, newDiveProfileModel);
            Assert.Equal(diveProfile.PressureOxygen, newDiveProfileModel.PressureOxygen);
            Assert.Equal(diveProfile.PressureNitrogen, newDiveProfileModel.PressureNitrogen);
            Assert.Equal(diveProfile.PressureHelium, newDiveProfileModel.PressureHelium);
            Assert.NotSame(diveProfile.AValues, newDiveProfileModel.AValues);
            Assert.NotSame(diveProfile.BValues, newDiveProfileModel.BValues);
            Assert.NotSame(diveProfile.TissuePressuresNitrogen, newDiveProfileModel.TissuePressuresNitrogen);
            Assert.NotSame(diveProfile.TissuePressuresHelium, newDiveProfileModel.TissuePressuresHelium);
            Assert.NotSame(diveProfile.TissuePressuresTotal, newDiveProfileModel.TissuePressuresTotal);
            Assert.NotSame(diveProfile.ToleratedAmbientPressures, newDiveProfileModel.ToleratedAmbientPressures);
            Assert.NotSame(diveProfile.CompartmentLoad, newDiveProfileModel.CompartmentLoad);
            Assert.NotSame(diveProfile.MaxSurfacePressures, newDiveProfileModel.MaxSurfacePressures);
        }
    }
}