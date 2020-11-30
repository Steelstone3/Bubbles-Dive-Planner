using Xunit;
using DivePlannerMk3.ViewModels.DivePlan;
using DivePlannerMk3.Models;

namespace DivePlannerTests
{
    public class GasManagementSetupUiTests
    {
        private PlanGasManagementViewModel _gasManagementSetup = new PlanGasManagementViewModel()
        {
            GasManagementModel = new GasManagementSetupModel(),
        };

        [Theory]
        [InlineData(12, 12, 50)]
        [InlineData(1, 12, 200)]
        [InlineData(15, 30, 300)]
        public void GasManagementModelCanBeSetTest(int cylinderVolume, int sacRate, int cylinderPressure)
        {
            //Arrange

            //Act
            _gasManagementSetup.GasManagementModel.CylinderVolume = cylinderVolume;
            _gasManagementSetup.GasManagementModel.SacRate = sacRate;
            _gasManagementSetup.GasManagementModel.CylinderPressure = cylinderPressure;

            //Assert
            Assert.Equal(cylinderPressure, _gasManagementSetup.GasManagementModel.CylinderPressure);
            Assert.Equal(sacRate, _gasManagementSetup.GasManagementModel.SacRate);
            Assert.Equal(cylinderVolume, _gasManagementSetup.GasManagementModel.CylinderVolume);
        }

        [Theory(Skip="Composite can execute command needs to be created, Can execute command needs to be created")]
        [InlineData(0, 11, 49)]
        [InlineData(16, 31, 301)]
        public void GasManagementModelLimitsTests(int cylinderVolume, int sacRate, int cylinderPressure)
        {
            //TODO AH may need to look into a composite command for CanExecute using reactiveUI i.e can execute create combined() and https://www.reactiveui.net/docs/handbook/commands/
            //TODO AH Range Validations for Gas Management View Model and CanExecute for the calculate
            //TODO AH Consider a combo box of 3, 5, 7, 10, 12, 15, 20, 24, 30
            //TODO AH Consider a list of gas supplies that you can add and take away from each of which you can assign a gas and supply to and switch out dyamically on the dive with each gas showing remaining and a history of gas usage

            //TODO AH use range attribute on sac rate

            //TODO AH provide a range between 50 and 300 for Cylinder Pressure
            
            //Arrange
            _gasManagementSetup.GasManagementModel.CylinderPressure = cylinderPressure;
            _gasManagementSetup.GasManagementModel.CylinderVolume = cylinderVolume;
            _gasManagementSetup.GasManagementModel.SacRate = sacRate;

            //Act
            //var canExecute = await = _gasManagementSetup.CanUseGasManagementSetup().FirstAsync();

            //Assert can calculate command as expected false
            //Assert
            //Assert.Equal(false, canExecute);
        }

        [Fact]
        public void IsVisible()
        {
            Assert.True(_gasManagementSetup.UiEnabled);
        }
    }
}