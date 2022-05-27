using Xunit;
using BubblesDivePlanner.DiveModels.DiveProfile;
using System.Collections.Generic;

namespace BubblesDivePlannerTests.DiveModels.DiveProfile
{
    public class DiveProfileViewModelShould
    {
        private readonly double[] expectedArray = { 3.0, 6.0 };
        private readonly double expectedValue = 10;

        private DiveProfileViewModel _diveProfileViewModel = new DiveProfileViewModel();

        [Fact]
        public void AllowModelToBeSet()
        {
            //Act
            _diveProfileViewModel.MaxSurfacePressures = expectedArray;
            _diveProfileViewModel.TissuePressuresNitrogen = expectedArray;
            _diveProfileViewModel.TissuePressuresHelium = expectedArray;
            _diveProfileViewModel.TissuePressuresTotal = expectedArray;
            _diveProfileViewModel.ToleratedAmbientPressures = expectedArray;
            _diveProfileViewModel.AValues = expectedArray;
            _diveProfileViewModel.BValues = expectedArray;
            _diveProfileViewModel.CompartmentLoad = expectedArray;
            _diveProfileViewModel.PressureOxygen = expectedValue;
            _diveProfileViewModel.PressureHelium = expectedValue;
            _diveProfileViewModel.PressureNitrogen = expectedValue;

            //Assert
            Assert.NotEmpty(_diveProfileViewModel.MaxSurfacePressures);
            Assert.NotEmpty(_diveProfileViewModel.TissuePressuresNitrogen);
            Assert.NotEmpty(_diveProfileViewModel.TissuePressuresHelium);
            Assert.NotEmpty(_diveProfileViewModel.TissuePressuresTotal);
            Assert.NotEmpty(_diveProfileViewModel.ToleratedAmbientPressures);
            Assert.NotEmpty(_diveProfileViewModel.AValues);
            Assert.NotEmpty(_diveProfileViewModel.BValues);
            Assert.NotEmpty(_diveProfileViewModel.CompartmentLoad);
            Assert.Equal(expectedValue, _diveProfileViewModel.PressureOxygen);
            Assert.Equal(expectedValue, _diveProfileViewModel.PressureNitrogen);
            Assert.Equal(expectedValue, _diveProfileViewModel.PressureHelium);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            var viewModelEvents = new List<string>();
            _diveProfileViewModel.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            _diveProfileViewModel.MaxSurfacePressures = expectedArray;
            _diveProfileViewModel.TissuePressuresNitrogen = expectedArray;
            _diveProfileViewModel.TissuePressuresHelium = expectedArray;
            _diveProfileViewModel.TissuePressuresTotal = expectedArray;
            _diveProfileViewModel.ToleratedAmbientPressures = expectedArray;
            _diveProfileViewModel.AValues = expectedArray;
            _diveProfileViewModel.BValues = expectedArray;
            _diveProfileViewModel.CompartmentLoad = expectedArray;
            _diveProfileViewModel.PressureOxygen = expectedValue;
            _diveProfileViewModel.PressureHelium = expectedValue;
            _diveProfileViewModel.PressureNitrogen = expectedValue;


            //Assert
            Assert.NotEmpty(viewModelEvents);
            Assert.Contains(nameof(_diveProfileViewModel.MaxSurfacePressures), viewModelEvents);
            Assert.Contains(nameof(_diveProfileViewModel.TissuePressuresNitrogen), viewModelEvents);
            Assert.Contains(nameof(_diveProfileViewModel.TissuePressuresHelium), viewModelEvents);
            Assert.Contains(nameof(_diveProfileViewModel.TissuePressuresHelium), viewModelEvents);
            Assert.Contains(nameof(_diveProfileViewModel.ToleratedAmbientPressures), viewModelEvents);
            Assert.Contains(nameof(_diveProfileViewModel.AValues), viewModelEvents);
            Assert.Contains(nameof(_diveProfileViewModel.BValues), viewModelEvents);
            Assert.Contains(nameof(_diveProfileViewModel.CompartmentLoad), viewModelEvents);
            Assert.Contains(nameof(_diveProfileViewModel.PressureOxygen), viewModelEvents);
            Assert.Contains(nameof(_diveProfileViewModel.PressureHelium), viewModelEvents);
            Assert.Contains(nameof(_diveProfileViewModel.PressureNitrogen), viewModelEvents);
        }

        [Fact]
        public void Clone()
        {
            //Act
            var newDiveProfileModel = _diveProfileViewModel.DeepClone();

            //Assert
            Assert.NotSame(_diveProfileViewModel, newDiveProfileModel);
            Assert.NotSame(_diveProfileViewModel.PressureOxygen, newDiveProfileModel.PressureOxygen);
            Assert.NotSame(_diveProfileViewModel.PressureNitrogen, newDiveProfileModel.PressureNitrogen);
            Assert.NotSame(_diveProfileViewModel.PressureHelium, newDiveProfileModel.PressureHelium);
            Assert.NotSame(_diveProfileViewModel.AValues, newDiveProfileModel.AValues);
            Assert.NotSame(_diveProfileViewModel.BValues, newDiveProfileModel.BValues);
            Assert.NotSame(_diveProfileViewModel.TissuePressuresNitrogen, newDiveProfileModel.TissuePressuresNitrogen);
            Assert.NotSame(_diveProfileViewModel.TissuePressuresHelium, newDiveProfileModel.TissuePressuresHelium);
            Assert.NotSame(_diveProfileViewModel.TissuePressuresTotal, newDiveProfileModel.TissuePressuresTotal);
            Assert.NotSame(_diveProfileViewModel.ToleratedAmbientPressures, newDiveProfileModel.ToleratedAmbientPressures);
            Assert.NotSame(_diveProfileViewModel.CompartmentLoad, newDiveProfileModel.CompartmentLoad);
            Assert.NotSame(_diveProfileViewModel.MaxSurfacePressures, newDiveProfileModel.MaxSurfacePressures);
        }
    }
}