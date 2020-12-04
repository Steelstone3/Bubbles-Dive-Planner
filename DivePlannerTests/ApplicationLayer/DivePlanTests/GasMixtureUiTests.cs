using System.Reactive.Linq;
using DivePlannerMk3.Controllers.ModelConverters;
using DivePlannerMk3.Models;
using DivePlannerMk3.ViewModels.DivePlan;
using Xunit;

namespace DivePlannerTests
{
    public class GasMixtureUiTests
    {
        //TODO AH validate this view model's ranges
        //TODO AH test for raise property changed event on view model
        //TODO AH test Nitrogen is calculated correctly for new gas mixtures on view model

        private PlanGasMixtureViewModel _gasMixtureViewModel = new PlanGasMixtureViewModel();

        [Fact]
        public void HasAtLeastOneGasMixtureTest()
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
        public void GasMixtureCanBeAddedTest(double oxygen, double nitrogen, double helium, string gasName)
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
        public void GasMixtureCanBeSetTest()
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

        [Theory]
        [InlineData(25, -5, 80, "Loads of Helium")]
        [InlineData(80, -5, 25, "Loads of Oxygen")]
        [InlineData(0, 0, 101, "Helium")]
        [InlineData(100, 0, -1, "Negative Helium")]
        [InlineData(101, 0, 0, "O2")]
        [InlineData(1,99,0,"Oxygen Starved")]
        [InlineData(4,96,0,"Oxygen Starved 2")]
        [InlineData(-1, 0, 100, "Negative Oxygen")]
        public async void GasMixtureLimitsTest(double oxygen, double nitrogen, double helium, string gasName)
        {
            //Arrange
            var gasConverter = new GasMixtureModelConverter();

            var gasMix = new GasMixtureModel()
            {
                GasName = gasName,
                Oxygen = oxygen,
                Helium = helium,
                Nitrogen = nitrogen,
            };

            //Act
            _gasMixtureViewModel.NewGasMixture = gasConverter.ConvertToViewModel(gasMix);

            var canExecute = await _gasMixtureViewModel.CanAddGasMixture.FirstAsync();

            //Assert
            Assert.False(canExecute);
        }
    }
}