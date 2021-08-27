using System.Collections.Generic;
using BubblesDivePlanner.Models.Plan;
using BubblesDivePlanner.ViewModels.DiveApplication.Plan;
using Xunit;

namespace BubblesDivePlannerTests.ApplicationLayerTests.ViewModels.Information
{
    public class DiveBoundariesViewModelShould
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
            var newGasMixture = new GasMixtureModel()
            {
                Oxygen = 34,
                Helium = 0,
                Nitrogen = 100 - 34,
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