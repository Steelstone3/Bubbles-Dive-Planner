using Xunit;
using BubblesDivePlanner.DiveModels.DiveProfile;

namespace BubblesDivePlannerTests.DiveModels
{
    public class DiveProfileModelShould
    {
        private IDiveProfileModel diveProfileModel = new DiveProfileModel();

        [Fact]
        public void AllowModelToBeSet()
        {
            //Arrange
            double[] expectedArray = { 3.0, 6.0 };
            double expectedValue = 10;

            //Act
            diveProfileModel.MaxSurfacePressures = expectedArray;
            diveProfileModel.TissuePressuresNitrogen = expectedArray;
            diveProfileModel.TissuePressuresHelium = expectedArray;
            diveProfileModel.TissuePressuresTotal = expectedArray;
            diveProfileModel.ToleratedAmbientPressures = expectedArray;
            diveProfileModel.AValues = expectedArray;
            diveProfileModel.BValues = expectedArray;
            diveProfileModel.CompartmentLoad = expectedArray;
            diveProfileModel.PressureOxygen = expectedValue;
            diveProfileModel.PressureHelium = expectedValue;
            diveProfileModel.PressureNitrogen = expectedValue;

            //Assert
            Assert.NotEmpty(diveProfileModel.MaxSurfacePressures);
            Assert.NotEmpty(diveProfileModel.TissuePressuresNitrogen);
            Assert.NotEmpty(diveProfileModel.TissuePressuresHelium);
            Assert.NotEmpty(diveProfileModel.TissuePressuresTotal);
            Assert.NotEmpty(diveProfileModel.ToleratedAmbientPressures);
            Assert.NotEmpty(diveProfileModel.AValues);
            Assert.NotEmpty(diveProfileModel.BValues);
            Assert.NotEmpty(diveProfileModel.CompartmentLoad);
            Assert.Equal(expectedValue, diveProfileModel.PressureOxygen);
            Assert.Equal(expectedValue, diveProfileModel.PressureNitrogen);
            Assert.Equal(expectedValue, diveProfileModel.PressureHelium);
        }
    }
}