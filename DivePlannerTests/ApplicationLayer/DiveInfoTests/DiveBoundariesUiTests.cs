using Xunit;
using DivePlannerMk3.ViewModels.DiveInfo;
using DivePlannerMk3.ViewModels.DivePlan;
using DivePlannerMk3.Controllers;
using System.Collections.Generic;

namespace DivePlannerTests
{
    public class DiveBoundariesUiTests
    {
        private DiveBoundariesViewModel _diveBoundaries = new DiveBoundariesViewModel();
        private GasMixtureSelectorViewModel _gasMixtureViewModel = new GasMixtureSelectorViewModel();

        [Fact]
        public void DiveBoundariesModelCanBeSetTest()
        {
            //Act
            _diveBoundaries.DiveCeiling = 10;

            //Assert
            Assert.Equal(10, _diveBoundaries.DiveCeiling);
        }

        [Fact]
        public void DiveCeilingRaisePropertyChangedTest()
        {
            //Arrange
            string diveCeilingEvent = "Not Fired";
            _diveBoundaries.PropertyChanged += ((sender, e) => diveCeilingEvent = e.PropertyName);

            //Act
            _diveBoundaries.DiveCeiling = 15;

            //Assert
            Assert.Equal(nameof(_diveBoundaries.DiveCeiling), diveCeilingEvent);
        }

        [Fact]
        public void MaxOperatingDepthRaisePropertyChangedTest()
        {
            //Arrange
            string maxOperatingDepthEvent = "Not Fired";
            _gasMixtureViewModel.PropertyChanged += ((sender, e) => maxOperatingDepthEvent = e.PropertyName);

            //Act
            _gasMixtureViewModel.MaximumOperatingDepth = 200;

            //Assert
            Assert.Equal(nameof(_gasMixtureViewModel.MaximumOperatingDepth), maxOperatingDepthEvent);
        }

        [Fact]
        public void MaxDepthChangesOnGasChangeTest()
        {
            //Arrange
            _gasMixtureViewModel = new GasMixtureSelectorViewModel();

            var newGasMixture = new GasMixtureViewModel()
            {
                Oxygen = 34,
                Helium = 0,
            };

            List<string> viewModelEvents = new List<string>();

            _gasMixtureViewModel.PropertyChanged += ((sender, e) => viewModelEvents.Add(e.PropertyName));

            //Act
            _gasMixtureViewModel.SelectedGasMixture = newGasMixture;

            //Assert
            Assert.Contains(nameof(_gasMixtureViewModel.SelectedGasMixture), viewModelEvents);
            Assert.Contains(nameof(_gasMixtureViewModel.MaximumOperatingDepth), viewModelEvents);
        }

        [Theory]
        [InlineData(21, 56.67)]
        [InlineData(100, 4)]
        public void MaxOperatingDepthCalculatedCorrectlyTest(double oxygenPercentage, double expectedDepth)
        {
            //Arrange
            var diveBoundariesController = new DiveBounderiesController();

            //Act
            _gasMixtureViewModel.MaximumOperatingDepth = diveBoundariesController.CalculateMaximumOperatingDepth(oxygenPercentage);

            //Assert
            Assert.Equal(expectedDepth, _gasMixtureViewModel.MaximumOperatingDepth);
        }
    }
}