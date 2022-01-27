using System.Collections.Generic;
using Moq;
using Xunit;
using BubblesDivePlanner.GasManagement.GasMixture;

namespace BubblesDivePlannerTests.GasManagement.GasMixture
{
    public class GasMixtureViewModelShould
    {
        private GasMixtureViewModel _gasMixture = new();

        [Fact]
        public void AllowModelToBeSet()
        {
            //Arrange
            int oxygen = 21;
            int helium = 10;
            int nitrogen = 69;

            //Act
            _gasMixture.Oxygen = oxygen;
            _gasMixture.Helium = helium;

            //Assert
            Assert.Equal(oxygen, _gasMixture.Oxygen);
            Assert.Equal(helium, _gasMixture.Helium);
            Assert.Equal(nitrogen, _gasMixture.Nitrogen);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            int oxygen = 32;
            int helium = 15;
            var viewModelEvents = new List<string>();
            _gasMixture.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            _gasMixture.Oxygen = oxygen;
            _gasMixture.Helium = helium;

            //Assert
            Assert.Contains(nameof(_gasMixture.Oxygen), viewModelEvents);
            Assert.Contains(nameof(_gasMixture.Helium), viewModelEvents);

            //Check count is 2
            Assert.Contains(nameof(_gasMixture.Nitrogen), viewModelEvents);
        }
    }
}