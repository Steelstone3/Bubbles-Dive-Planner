using Xunit;
using BubblesDivePlanner.DiveModels.DiveProfile;

namespace BubblesDivePlannerTests.DiveModels.DiveProfile
{
    public class DiveProfileModelShould
    {
        private IDiveProfileModel _diveProfileModel = new DiveProfileModel();

        [Fact]
        public void AllowModelToBeSet()
        {
            //Arrange
            double[] expectedArray = { 3.0, 6.0 };
            double expectedValue = 10;

            //Act
            _diveProfileModel.MaxSurfacePressures = expectedArray;
            _diveProfileModel.TissuePressuresNitrogen = expectedArray;
            _diveProfileModel.TissuePressuresHelium = expectedArray;
            _diveProfileModel.TissuePressuresTotal = expectedArray;
            _diveProfileModel.ToleratedAmbientPressures = expectedArray;
            _diveProfileModel.AValues = expectedArray;
            _diveProfileModel.BValues = expectedArray;
            _diveProfileModel.CompartmentLoad = expectedArray;
            _diveProfileModel.PressureOxygen = expectedValue;
            _diveProfileModel.PressureHelium = expectedValue;
            _diveProfileModel.PressureNitrogen = expectedValue;

            //Assert
            Assert.NotEmpty(_diveProfileModel.MaxSurfacePressures);
            Assert.NotEmpty(_diveProfileModel.TissuePressuresNitrogen);
            Assert.NotEmpty(_diveProfileModel.TissuePressuresHelium);
            Assert.NotEmpty(_diveProfileModel.TissuePressuresTotal);
            Assert.NotEmpty(_diveProfileModel.ToleratedAmbientPressures);
            Assert.NotEmpty(_diveProfileModel.AValues);
            Assert.NotEmpty(_diveProfileModel.BValues);
            Assert.NotEmpty(_diveProfileModel.CompartmentLoad);
            Assert.Equal(expectedValue, _diveProfileModel.PressureOxygen);
            Assert.Equal(expectedValue, _diveProfileModel.PressureNitrogen);
            Assert.Equal(expectedValue, _diveProfileModel.PressureHelium);
        }

        [Fact]
        public void Clone()
        {
            //Act
            var newDiveProfileModel = _diveProfileModel.DeepClone();

            //Assert
            Assert.NotSame(_diveProfileModel, newDiveProfileModel);
        }
    }
}