using System.Reactive.Linq;
using DivePlannerMk3.ViewModels.DivePlan;
using Xunit;

namespace DivePlannerTests
{
    public class CalculateDiveStepUserInterfaceShould
    {
        private GasMixtureSelectorViewModel _gasMixtureViewModel = new GasMixtureSelectorViewModel();

        //TODO Setup cannot execute in the view model command and test that
        [Fact(Skip = "Test needs implementing - is can execute dive step false when selected dive model or gas mixture is null")]
        public void NotAllowDivesProfilesToBeRunWithoutASelectedDiveModel()
        {
            //Arrange

            //Act

            //Assert
        }

        [Fact(Skip = "Depth should not be allowed to exceed max operating depth, time and depth should both have reasonable ranges")]
        public void NotAllowDiveStepExecutionIfADepthIsOutOfRange()
        {
            //Arrange           

            //Act

            //Assert
        }

        [Fact(Skip = "Depth should not exceed maximum operating depth (maybe +5 for planning purposes)")]
        public void NotAllowDiveStepExecutionIfTimeIsOutOfRange()
        {
            //Arrange

            //Act

            //Assert
        }

        [Fact(Skip = "Depth should not be allowed to exceed max operating depth, time and depth should both have reasonable ranges")]
        public void NotAllowDiveStepExecutionIfADepthIsGreaterThanMaximumOperatingDepth()
        {
            //Arrange           

            //Act

            //Assert
        }

        [Theory(Skip = "Cylinder Volume Should Be Between 3 and 30")]
        [InlineData(2, false)]
        [InlineData(31, false)]
        [InlineData(3, true)]
        [InlineData(30, true)]
        public void NotAllowDiveStepExecutionIfCylinderVolumeIsOutOfRange(int cylinderVolume, bool expectedResult)
        {

        }

        [Theory(Skip = "Cylinder Pressure Should Be Between 50 and 300")]
        [InlineData(49, false)]
        [InlineData(301, false)]
        [InlineData(300, true)]
        [InlineData(50, true)]
        public void NotAllowDiveStepExecutionIfCylinderPressureIsOutOfRange(int cylinderPressure, bool expectedResult)
        {

        }

        [Theory(Skip = "SAC Rate Should Be Between 5 and 30")]
        [InlineData(4, false)]
        [InlineData(31, false)]
        [InlineData(5, true)]
        [InlineData(30, true)]
        public void NotAllowDiveStepExecutionIfSurfaceAirConsumptionRateIsOutOfRange(int surfaceAirComsumptionRate, bool expectedResult)
        {

        }

        [Theory]
        [InlineData(100, -1, "Negative Helium")]
        [InlineData(-1, 100, "Negative Oxygen")]
        public async void NotAllowDiveStepExecutionIfGasMixtureHasNegativeValues(double oxygen, double helium, string gasName)
        {
            //Arrange
            var gasMix = new GasMixtureViewModel()
            {
                GasName = gasName,
                Oxygen = oxygen,
                Helium = helium,
            };

            //Act
            _gasMixtureViewModel.NewGasMixture = gasMix;

            var canExecute = await _gasMixtureViewModel.CanAddGasMixture.FirstAsync();

            //Assert
            Assert.False(canExecute);
        }

        [Theory]
        [InlineData(25, 80, "Loads of Helium")]
        [InlineData(80, 25, "Loads of Oxygen")]
        [InlineData(0, 101, "Helium")]
        [InlineData(101, 0, "O2")]
        public async void NotAllowDiveStepExecutionIfGasMixtureIsOver100Percent(double oxygen, double helium, string gasName)
        {
            //Arrange
            var gasMix = new GasMixtureViewModel()
            {
                GasName = gasName,
                Oxygen = oxygen,
                Helium = helium,
            };

            //Act
            _gasMixtureViewModel.NewGasMixture = gasMix;

            var canExecute = await _gasMixtureViewModel.CanAddGasMixture.FirstAsync();

            //Assert
            Assert.False(canExecute);
        }

        [Theory]
        [InlineData(1, 0, "Oxygen Starved")]
        [InlineData(4, 0, "Oxygen Starved 2")]
        public async void NotAllowDiveStepExecutionIfGasMixtureOxygenLevelsAreTooLow(double oxygen, double helium, string gasName)
        {
            //Arrange
            var gasMix = new GasMixtureViewModel()
            {
                GasName = gasName,
                Oxygen = oxygen,
                Helium = helium,
            };

            //Act
            _gasMixtureViewModel.NewGasMixture = gasMix;

            var canExecute = await _gasMixtureViewModel.CanAddGasMixture.FirstAsync();

            //Assert
            Assert.False(canExecute);
        }
    }
}