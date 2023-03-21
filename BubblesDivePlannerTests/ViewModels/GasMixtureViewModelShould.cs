using System.Collections.Generic;
using Xunit;
using BubblesDivePlanner.Cylinders.CylinderSetup.GasMixture;

namespace BubblesDivePlannerTests.ViewModels
{
    public class GasMixtureViewModelShould
    {
        private readonly GasMixtureViewModel _gasMixture = new();

        [Fact]
        public void AllowModelToBeSet()
        {
            //Arrange
            const int oxygen = 21;
            const int helium = 10;
            const int nitrogen = 69;
            const double maximumOperatingDepth = 56.67;

            //Act
            _gasMixture.Oxygen = oxygen;
            _gasMixture.Helium = helium;

            //Assert
            Assert.Equal(oxygen, _gasMixture.Oxygen);
            Assert.Equal(helium, _gasMixture.Helium);
            Assert.Equal(nitrogen, _gasMixture.Nitrogen);
            Assert.Equal(maximumOperatingDepth, _gasMixture.MaximumOperatingDepth);
            Assert.True(_gasMixture.IsVisible);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            const int oxygen = 32;
            const int helium = 15;
            List<string> viewModelEvents = new();
            _gasMixture.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            _gasMixture.Oxygen = oxygen;
            _gasMixture.Helium = helium;
            _gasMixture.IsVisible = false;

            //Assert
            Assert.NotEmpty(viewModelEvents);
            Assert.Contains(nameof(_gasMixture.Oxygen), viewModelEvents);
            Assert.Contains(nameof(_gasMixture.Helium), viewModelEvents);
            Assert.Contains(nameof(_gasMixture.Nitrogen), viewModelEvents);
            Assert.Contains(nameof(_gasMixture.IsVisible), viewModelEvents);
        }
    }
}