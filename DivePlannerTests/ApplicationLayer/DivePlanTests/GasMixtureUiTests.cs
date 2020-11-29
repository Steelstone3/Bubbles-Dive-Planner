using System.Reflection;
using DivePlannerMk3.Models;
using DivePlannerMk3.ViewModels.DivePlan;
using Xunit;

namespace DivePlannerTests
{
    public class GasMixtureUiTests
    {
        //Select gas mixture, add gas mixture, check limits so that no gas mixture can have more than 100% total gas!
 
        //TODO AH Additional functionality added that also needs doing
 
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

        [Fact]
        public void GasMixtureCanBeSetTest()
        {
           //Arrange
            var gasMix = new GasMixtureModel()
            {
                GasName = "Bob",
                Oxygen = 50,
                Helium = 30,
                Nitrogen = 20,
            };

            //Act
            _gasMixtureViewModel.SelectedGasMixture = gasMix;

            //Assert
            Assert.Equal(gasMix, _gasMixtureViewModel.SelectedGasMixture);
        }

        [Fact(Skip = "Test needs implementing")]
        public void GasMixtureCanNotBeOver100PercentTest()
        {
            //Arrange

            //Act

            //Assert
        }

        [Fact(Skip = "Test needs implementing")]
        public void GasMixtureLimitsTest()
        {
            //Arrange

            //Act

            //Assert
        }

        [Theory]
        [InlineData(21,79,0,"Air")]
        [InlineData(50,20,30,"Crazy")]
        public void GasMixtureCanBeAddedTest(int oxygen, int nitrogen, int helium, string gasName)
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

        [Fact(Skip = "Logic needs implementing (with a pop out), Test needs implementing")]
        public void AddedGasMixtureLimitsTest()
        {
            //Arrange

            //Act

            //Assert
        }

        [Fact(Skip = "Logic needs implementing (with a pop out), Test needs implementing")]
        public void AddedGasMixtureCanNotBeOver100PercentTest()
        {
            //Arrange

            //Act

            //Assert
        }
    }
}