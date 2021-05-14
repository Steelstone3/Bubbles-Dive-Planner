using Xunit;
using DivePlannerMk3.ViewModels.DiveInfo;
using DivePlannerMk3.ViewModels.DivePlan;
using DivePlannerMk3.Controllers;
using System.Collections.Generic;

namespace DivePlannerTests
{
    public class DiveBoundariesUserInterfaceShould
    {
        private DiveCeilingViewModel _diveCeilingViewModel = new DiveCeilingViewModel();
        private GasMixtureSelectorViewModel _gasMixtureViewModel = new GasMixtureSelectorViewModel();

        [Fact]
        public void DiveBoundariesModelCanBeSetTest()
        {
            //Act
            _diveCeilingViewModel.DiveCeiling = 10;

            //Assert
            Assert.Equal(10, _diveCeilingViewModel.DiveCeiling);
        }

        [Fact]
        public void DiveCeilingRaisePropertyChangedTest()
        {
            //Arrange
            string diveCeilingEvent = "Not Fired";
            _diveCeilingViewModel.PropertyChanged += ((sender, e) => diveCeilingEvent = e.PropertyName);

            //Act
            _diveCeilingViewModel.DiveCeiling = 15;

            //Assert
            Assert.Equal(nameof(_diveCeilingViewModel.DiveCeiling), diveCeilingEvent);
        }

        [Theory]
        [InlineData(4.1, 1.41, 0.99)]
        [InlineData(6.0, 1.6, 1.5)]
        public void DiveCeilingCalculation(double ceilingExpected, double toleratedAmbientPressure1, double toleratedAmbientPressure2)
        {
            //Arrange
            var diveCeilingController = new DiveCeilingController();
            var toleratedAmbientPressure = new List<double>()
            {
                toleratedAmbientPressure1, toleratedAmbientPressure2,
            };

            //Act
            var diveCeiling = diveCeilingController.CalculateDiveCeiling(toleratedAmbientPressure);

            //Assert
            Assert.Equal(ceilingExpected, diveCeiling, 2);
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
            var _maxOperatingDepthController = new MaxOperatingDepthController();

            //Act
            _gasMixtureViewModel.MaximumOperatingDepth = _maxOperatingDepthController.CalculateMaximumOperatingDepth(oxygenPercentage);

            //Assert
            Assert.Equal(expectedDepth, _gasMixtureViewModel.MaximumOperatingDepth);
        }
    }
}