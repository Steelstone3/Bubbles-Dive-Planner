using System.Collections.Generic;
using Moq;
using Xunit;
using BubblesDivePlanner.GasManagement.GasMixture;

namespace BubblesDivePlannerTests.GasManagement.GasMixture
{
    public class GasMixtureViewModelShould
    {
        private GasMixtureViewModel _gasMixture;
        private Mock<IGasMixtureController> _gasMixtureControllerMock;

        public GasMixtureViewModelShould()
        {
            _gasMixtureControllerMock = new Mock<IGasMixtureController>();
            _gasMixture = new(_gasMixtureControllerMock.Object);
        }

        [Fact]
        public void AllowModelToBeSet()
        {
            //Arrange
            int oxygen = 21;
            int helium = 10;

            //Act
            _gasMixture.Oxygen = oxygen;
            _gasMixture.Helium = helium;

            //Assert
            Assert.Equal(oxygen, _gasMixture.Oxygen);
            Assert.Equal(helium, _gasMixture.Helium);
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
        }

        [Fact]
        public void CalculateNitrogenOnOxygenSet()
        {
            //Arrange
            int oxygen = 21;
            _gasMixtureControllerMock.Setup(x => x.CalculateNitrogenMixture(oxygen, 0));

            //Act
            _gasMixture.Oxygen = oxygen;

            //Assert
            _gasMixtureControllerMock.Verify(x => x.CalculateNitrogenMixture(oxygen, 0));
        }

        [Fact]
        public void CalculateNitrogenOnHeliumSet()
        {
            //Arrange
            int helium = 10;
            _gasMixtureControllerMock.Setup(x => x.CalculateNitrogenMixture(0, helium));

            //Act
            _gasMixture.Helium = helium;

            //Assert
            _gasMixtureControllerMock.Verify(x => x.CalculateNitrogenMixture(0, helium));

        }
    }
}