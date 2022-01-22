using BubblesDivePlanner.GasManagement;
using System.Collections.Generic;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.ApplicationLayerTests.ViewModels.Plan
{
    public class GasMixtureViewModelShould
    {
        private GasMixtureViewModel _gasMixture;
        private Mock<IGasMixtureController> _gasMixtureController;

        public GasMixtureViewModelShould()
        {
            _gasMixtureController = new Mock<IGasMixtureController>();
            _gasMixture = new(_gasMixtureController.Object);
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
        public void RaisePropertyChanged() {
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
            _gasMixtureController.Setup(x => x.CalculateNitrogenMixture(oxygen, 0));

            //Act
            _gasMixture.Oxygen = oxygen;

            //Assert
            _gasMixtureController.Verify(x => x.CalculateNitrogenMixture(oxygen, 0));
        }

        [Fact]
        public void CalculateNitrogenOnHeliumSet()
        {
           //Arrange
            int helium = 10;
            _gasMixtureController.Setup(x => x.CalculateNitrogenMixture(0, helium));

            //Act
            _gasMixture.Helium = helium;

            //Assert
            _gasMixtureController.Verify(x => x.CalculateNitrogenMixture(0, helium));
       
        }
    }
}