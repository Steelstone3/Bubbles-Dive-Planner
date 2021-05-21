using Xunit;
using DivePlannerMk3.ViewModels.DivePlan;
using DivePlannerMk3.Controllers;
using System.Collections.Generic;
using DivePlannerMk3.ViewModels.DiveInformation;

namespace DivePlannerTests
{
    public class DiveBoundariesUserInterfaceShould
    {
        private GasMixtureSelectorViewModel _gasMixtureViewModel = new GasMixtureSelectorViewModel();

        [Fact]
        public void AllowDiveBoundariesModelToBeSet()
        {
            //Act
            _gasMixtureViewModel.MaximumOperatingDepth = 55;

            //Assert
            Assert.Equal(55, _gasMixtureViewModel.MaximumOperatingDepth);
        }

        [Theory]
        [InlineData(4.1, 1.41, 0.99)]
        [InlineData(6.0, 1.6, 1.5)]
        public void PerformDiveCeilingCalculation(double ceilingExpected, double toleratedAmbientPressure1, double toleratedAmbientPressure2)
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
        public void RaisePropertyChangedOnMaxOperatingDepthPropertyIsSet()
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
        public void MaximumOperatingDepthUpdatesWhenGasMixtureIsSet()
        {
            //Arrange
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
    }
}