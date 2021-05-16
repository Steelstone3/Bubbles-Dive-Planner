using System.Reactive.Linq;
using DivePlannerMk3.Controllers;
using DivePlannerMk3.Models;
using DivePlannerMk3.ViewModels.DivePlan;
using Xunit;

namespace DivePlannerTests
{
    public class GasMixtureUserInterfaceShould
    {
        //TODO AH validate this view model's ranges
        //TODO AH test for raise property changed event on view model
        //TODO AH test Nitrogen is calculated correctly for new gas mixtures on view model

        private GasMixtureSelectorViewModel _gasMixtureViewModel = new GasMixtureSelectorViewModel();

        [Fact]
        public void HaveAtLeastOneGasMixture()
        {
            //Arrange

            //Act

            //Assert
            Assert.NotNull(_gasMixtureViewModel.GasMixtures);
            Assert.NotEmpty(_gasMixtureViewModel.GasMixtures);
        }

        [Theory]
        [InlineData(21, 79, 0, "Air")]
        [InlineData(10, 70, 20, "Heliox")]
        [InlineData(32, 68, 0, "EAN32")]
        public void AllowGasMixturesToBeAdded(double oxygen, double nitrogen, double helium, string gasName)
        {
            //Arrange
            var gasMix = new GasMixtureModel()
            {
                GasName = gasName,
                Oxygen = oxygen,
                Helium = helium,
                Nitrogen = nitrogen,
            };

            //Act
            _gasMixtureViewModel.GasMixtures.Add(gasMix);

            //Assert
            Assert.Equal(2, _gasMixtureViewModel.GasMixtures.Count);
        }

        //TODO AH change to a view model and check correct nitrogen
        [Fact]
        public void AllowGasMixtureToBeSet()
        {
            //Arrange
            var gasMix = new GasMixtureModel()
            {
                GasName = "Bob",
                Oxygen = 50,
                Helium = 30,
                //TODO AH reconsider the use of set for nitrogen
                Nitrogen = 20,
            };

            //Act
            _gasMixtureViewModel.SelectedGasMixture = gasMix;

            //Assert
            Assert.Equal(gasMix, _gasMixtureViewModel.SelectedGasMixture);
        }

        //TODO AH here put a test relating to raise property changed
    }
}