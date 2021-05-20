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
        public void AllowDiveBoundariesModelToBeSet()
        {
            //Act
            _diveCeilingViewModel.DiveCeiling = 10;
            _gasMixtureViewModel.MaximumOperatingDepth = 55;

            //Assert
            Assert.Equal(10, _diveCeilingViewModel.DiveCeiling);
            Assert.Equal(55, _gasMixtureViewModel.MaximumOperatingDepth);
        }

        [Fact]
        public void RaisePropertyChangedWhenDiveCeilingPropertyIsSet()
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